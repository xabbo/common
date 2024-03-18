using System;
using System.IO;
using System.Buffers;
using System.Buffers.Binary;
using System.Text;
using System.Globalization;

namespace Xabbo.Messages;

/// <inheritdoc cref="IPacket" />
public sealed partial class Packet : IPacket
{
    /// <inheritdoc cref="byte" />
    public static readonly Type Byte = typeof(byte);

    /// <summary>
    /// Represents a boolean as an 8-bit unsigned integer.
    /// </summary>
    public static readonly Type Bool = typeof(bool);

    /// <inheritdoc cref="short" />
    public static readonly Type Short = typeof(short);

    /// <inheritdoc cref="int" />
    public static readonly Type Int = typeof(int);

    /// <inheritdoc cref="float" />
    public static readonly Type Float = typeof(float);

    /// <inheritdoc cref="long" />
    public static readonly Type Long = typeof(long);

    /// <summary>
    /// Represents a UTF-8 encoded string preceded by a 2-byte integer specifying its length.
    /// </summary>
    public static readonly Type String = typeof(string);

    private bool _disposed;
    private IMemoryOwner<byte> _memoryOwner;
    private Memory<byte> _buffer;
    private int _position;

    /// <inheritdoc />
    public ClientType Protocol { get; set; } = ClientType.Unknown;

    /// <inheritdoc />
    public Header Header { get; set; } = Header.Unknown;

    /// <inheritdoc />
    public Span<byte> Buffer
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _buffer.Span;
        }
    }
    ReadOnlySpan<byte> IReadOnlyPacket.Buffer => Buffer;

    /// <inheritdoc />
    public Memory<byte> GetMemory() => _buffer;
    ReadOnlyMemory<byte> IReadOnlyPacket.GetMemory() => GetMemory();

    /// <inheritdoc />
    public int Position
    {
        get => _position;
        set
        {
            if (value < 0 || value > Length)
                throw new IndexOutOfRangeException();
            _position = value;
        }
    }

    /// <inheritdoc />
    public int Length => _buffer.Length;

    /// <inheritdoc />
    public int Available => Length - Position;

    /// <summary>
    /// Constructs a new packet with the specified protocol and message header.
    /// </summary>
    /// <param name="protocol">The packet protocol hint.</param>
    /// <param name="header">The message header.</param>
    /// <param name="capacity">The initial capacity in bytes.</param>
    public Packet(Header header, ClientType protocol = ClientType.Unknown, int capacity = 8)
    {
        _memoryOwner = MemoryPool<byte>.Shared.Rent(capacity);
        _buffer = _memoryOwner.Memory[..0];

        Protocol = protocol;
        Header = header;
    }

    /// <summary>
    /// Constructs a new packet with the specified protocol and header,
    /// copying the <see cref="ReadOnlySpan{T}"/> into its internal buffer.
    /// </summary>
    public Packet(Header header, ReadOnlySpan<byte> data, ClientType protocol = ClientType.Unknown)
        : this(header, protocol, data.Length)
    {
        _buffer = _memoryOwner.Memory[..data.Length];
        data.CopyTo(_buffer.Span);
    }

    /// <summary>
    /// Constructs a new packet with the specified protocol and header,
    /// copying the <see cref="ReadOnlySequence{T}"/> into its internal buffer.
    /// </summary>
    public Packet(Header header, in ReadOnlySequence<byte> data, ClientType protocol = ClientType.Unknown)
        : this(header, protocol, (int)data.Length)
    {
        _buffer = _memoryOwner.Memory[..(int)data.Length];
        data.CopyTo(_buffer.Span);
    }

    /// <inheritdoc cref="IReadOnlyPacket.Copy" />
    public Packet Copy() => new(Header, Buffer, Protocol);
    IPacket IReadOnlyPacket.Copy() => Copy();

    /// <summary>
    /// Grows the internal buffer by the specified number of bytes from the current position in the packet, if its size is insufficient.
    /// </summary>
    private void Grow(int length) => GrowToSize(Position + length);

    /// <summary>
    /// Grows the internal buffer to the specified minimum size, if its size is insufficient.
    /// </summary>
    private void GrowToSize(int minSize)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        int size = _memoryOwner.Memory.Length;
        if (size < minSize)
        {
            while (size < minSize)
                size <<= 1;

            using IMemoryOwner<byte> oldMemoryOwner = _memoryOwner;
            _memoryOwner = MemoryPool<byte>.Shared.Rent(size);
            oldMemoryOwner.Memory.CopyTo(_memoryOwner.Memory);
        }
        
        if (_buffer.Length < minSize)
            _buffer = _memoryOwner.Memory[..minSize];
    }

    /// <summary>
    /// Returns whether a byte can be read from the current position in the packet.
    /// </summary>
    /// <returns><c>true</c> if the number of available bytes is &gt;= <c>1</c>.</returns>
    public bool CanReadByte() => Available >= 1;

    /// <summary>
    /// Returns whether a bool can be read from the current position in the packet.
    /// </summary>
    /// <returns><c>true</c> if a byte can be read from the current position in the packet and its value is either <c>0</c> or <c>1</c>.</returns>
    public bool CanReadBool()
    {
        if (!CanReadByte()) return false;
        byte b = ReadByte();
        Position -= 1;
        return b <= 1;
    }

    public bool CanReadShort() => Available >= 2;

    /// <summary>
    /// Returns whether an int can be read from the current position in the packet.
    /// </summary>
    /// <returns></returns>
    public bool CanReadInt() => Available >= 4;

    /// <summary>
    /// Returns whether a string can be read from the current position in the packet.
    /// </summary>
    public bool CanReadString()
    {
        return
            Available >= 2 &&
            Available >= 2 + BinaryPrimitives.ReadUInt16BigEndian(_buffer.Span[Position..]);
    }

    /// <summary>
    /// Returns whether a float can be read as a string from the current position in the packet.
    /// </summary>
    /// <returns></returns>
    public bool CanReadFloatAsString()
    {
        if (!CanReadString()) return false;

        int pos = Position;
        bool success = double.TryParse(ReadString(), NumberStyles.Float, CultureInfo.InvariantCulture, out _);
        Position = pos;

        return success;
    }

    /// <inheritdoc cref="IPacket.Skip(int)" />
    public Packet Skip(int bytes)
    {
        Position += bytes;
        return this;
    }
    IPacket IPacket.Skip(int bytes) => Skip(bytes);
    IReadOnlyPacket IReadOnlyPacket.Skip(int bytes) => Skip(bytes);

    /// <summary>
    /// Skips a value of the specified type.
    /// </summary>
    private void Skip(Type type)
    {
        Skip(Type.GetTypeCode(type) switch
        {
            TypeCode.Byte or TypeCode.Boolean => 1,
            TypeCode.Int16 or TypeCode.UInt16 => 2,
            TypeCode.Int32 or TypeCode.UInt32 => 4,
            TypeCode.Int64 or TypeCode.UInt64 => Protocol switch
            {
                ClientType.Flash => 4,
                ClientType.Unity => 8,
                _ => throw new InvalidOperationException("Cannot skip long: packet protocol is not specified.")
            },
            TypeCode.Single => Protocol switch
            {
                ClientType.Flash => ReadShort(),
                ClientType.Unity => 4,
                _ => throw new InvalidOperationException("Cannot skip float: packet protocol is not specified.")
            },
            TypeCode.String => ReadShort(),
            _ => throw new InvalidOperationException($"Cannot skip value of type \"{type.FullName}\".")
        });
    }

    /// <inheritdoc cref="IPacket.Skip(Type[])" />
    public Packet Skip(params Type[] types)
    {
        foreach (Type type in types)
            Skip(type);   
        return this;
    }
    IPacket IPacket.Skip(Type[] types) => Skip(types);
    IReadOnlyPacket IReadOnlyPacket.Skip(Type[] types) => Skip(types);

    /// <inheritdoc />
    public byte ReadByte()
    {
        byte value = Buffer[Position];
        Position++;
        return value;
    }

    /// <inheritdoc />
    public byte ReadByte(int position)
    {
        Position = position;
        return ReadByte();
    }

    /// <inheritdoc />
    public void Read(Span<byte> buffer)
    {
        Buffer[Position..(Position + buffer.Length)].CopyTo(buffer);
        Position += buffer.Length;
    }

    /// <inheritdoc />
    public void ReadBytes(Span<byte> buffer, int position)
    {
        Position = position;
        Read(buffer);
    }

    /// <inheritdoc />
    public bool ReadBool()
    {
        byte value = Buffer[Position];
        if (value != 0 && value != 1)
            throw new InvalidDataException($"Value {value} is outside the range of a boolean.");

        Position++;
        return value == 1;
    }

    /// <inheritdoc />
    public bool ReadBool(int position)
    {
        Position = position;
        return ReadBool();
    }

    /// <inheritdoc />
    public short ReadShort()
    {
        short value = BinaryPrimitives.ReadInt16BigEndian(Buffer[Position..]);
        Position += 2;
        return value;
    }

    /// <inheritdoc />
    public short ReadShort(int position)
    {
        Position = position;
        return ReadShort();
    }

    /// <inheritdoc />
    public int ReadInt()
    {
        int value = BinaryPrimitives.ReadInt32BigEndian(Buffer[Position..]);
        Position += 4;
        return value;
    }

    /// <inheritdoc />
    public int ReadInt(int position)
    {
        Position = position;
        return ReadInt();
    }

    /// <inheritdoc />
    public float ReadFloat()
    {
        float value = BinaryPrimitives.ReadSingleBigEndian(Buffer[Position..]);
        Position += 4;
        return value;
    }

    /// <inheritdoc />
    public float ReadFloat(int position)
    {
        Position = position;
        return ReadFloat();
    }

    /// <inheritdoc />
    public long ReadLong()
    {
        long value = BinaryPrimitives.ReadInt64BigEndian(Buffer[Position..]);
        Position += 8;
        return value;
    }

    /// <inheritdoc />
    public long ReadLong(int position)
    {
        Position = position;
        return ReadLong();
    }

    /// <inheritdoc />
    public string ReadString()
    {
        Span<byte> buffer = Buffer;
        int length = BinaryPrimitives.ReadUInt16BigEndian(buffer[Position..]);
        string value = Encoding.UTF8.GetString(buffer[(Position+2)..(Position+2+length)]);
        Position += (2 + length);
        return value;
    }

    /// <inheritdoc />
    public string ReadString(int position)
    {
        Position = position;
        return ReadString();
    }

    /// <inheritdoc />
    public float ReadFloatAsString()
    {
        return float.Parse(ReadString(), CultureInfo.InvariantCulture);
    }

    /// <inheritdoc />
    public float ReadFloatAsString(int position)
    {
        Position = position;
        return ReadFloatAsString();
    }

    /// <inheritdoc />
    public Span<byte> GetSpan(int length)
    {
        Grow(length);
        Span<byte> span = Buffer[Position..(Position + length)];
        Position += length;
        return span;
    }

    public Packet Write(IComposable composable)
    {
        composable.Compose(this);
        return this;
    }
    IPacket IPacket.Write(IComposable composable) => Write(composable);

    public Packet WriteByte(byte value)
    {
        Grow(1);
        _buffer.Span[Position++] = value;
        return this;
    }
    IPacket IPacket.WriteByte(byte value) => WriteByte(value);

    public Packet WriteByte(byte value, int position)
    {
        Position = position;
        return WriteByte(value);
    }
    IPacket IPacket.WriteByte(byte value, int position) => WriteByte(value, position);

    public Packet WriteBytes(ReadOnlySpan<byte> bytes)
    {
        Grow(bytes.Length);
        bytes.CopyTo(_buffer.Span[Position..]);
        Position += bytes.Length;
        return this;
    }
    IPacket IPacket.WriteBytes(ReadOnlySpan<byte> bytes) => WriteBytes(bytes);

    public Packet WriteBytes(ReadOnlySpan<byte> bytes, int position)
    {
        Position = position;
        return WriteBytes(bytes);
    }
    IPacket IPacket.WriteBytes(ReadOnlySpan<byte> bytes, int position) => WriteBytes(bytes, position);

    public Packet WriteBool(bool value) => WriteByte((byte)(value ? 1 : 0));
    IPacket IPacket.WriteBool(bool value) => WriteBool(value);

    public Packet WriteBool(bool value, int position)
    {
        Position = position;
        return WriteBool(value);
    }
    IPacket IPacket.WriteBool(bool value, int position) => WriteBool(value, position);

    public Packet WriteShort(short value)
    {
        Grow(2);
        BinaryPrimitives.WriteInt16BigEndian(_buffer.Span[Position..], value);
        Position += 2;
        return this;
    }
    IPacket IPacket.WriteShort(short value) => WriteShort(value);

    public Packet WriteShort(short value, int position)
    {
        Position = position;
        return WriteShort(value);
    }
    IPacket IPacket.WriteShort(short value, int position) => WriteShort(value, position);

    public Packet WriteInt(int value)
    {
        Grow(4);
        BinaryPrimitives.WriteInt32BigEndian(_buffer.Span[Position..], value);
        Position += 4;
        return this;
    }
    IPacket IPacket.WriteInt(int value) => WriteInt(value);

    public Packet WriteInt(int value, int position)
    {
        Position = position;
        return WriteInt(value);
    }
    IPacket IPacket.WriteInt(int value, int position) => WriteInt(value, position);

    public Packet WriteLong(long value)
    {
        if (Protocol == ClientType.Flash)
            throw new InvalidOperationException("Cannot write long when the packet protocol is Flash.");

        Grow(8);
        BinaryPrimitives.WriteInt64BigEndian(_buffer.Span[Position..], value);
        Position += 8;
        return this;
    }
    IPacket IPacket.WriteLong(long value) => WriteLong(value);

    public Packet WriteLong(long value, int position)
    {
        Position = position;
        return WriteLong(value);
    }
    IPacket IPacket.WriteLong(long value, int position) => WriteLong(value, position);

    public Packet WriteFloat(float value)
    {
        Grow(4);
        BinaryPrimitives.WriteSingleBigEndian(_buffer.Span[Position..], value);
        Position += 4;
        return this;
    }
    IPacket IPacket.WriteFloat(float value) => WriteFloat(value);

    /// <inheritdoc cref="IPacket.WriteFloat(float, int)" />
    public Packet WriteFloat(float value, int position)
    {
        Position = position;
        return WriteFloat(value);
    }
    IPacket IPacket.WriteFloat(float value, int position) => WriteFloat(value, position);

    /// <inheritdoc cref="IPacket.WriteFloatAsString(float)" />
    public Packet WriteFloatAsString(float value)
    {
        return WriteString(value.ToString("0.0##############", CultureInfo.InvariantCulture));
    }
    IPacket IPacket.WriteFloatAsString(float value) => WriteFloatAsString(value);

    /// <inheritdoc cref="IPacket.WriteFloatAsString(float, int)" />
    public Packet WriteFloatAsString(float value, int position)
    {
        Position = position;
        return WriteFloatAsString(value);
    }
    IPacket IPacket.WriteFloatAsString(float value, int position) => WriteFloatAsString(value, position);

    /// <inheritdoc cref="IPacket.WriteString(string)" />
    public Packet WriteString(string value)
    {
        int len = Encoding.UTF8.GetByteCount(value);
        WriteShort((short)len);

        Grow(len);
        Encoding.UTF8.GetBytes(value, _buffer.Span[Position..]);
        Position += len;
        return this;
    }
    IPacket IPacket.WriteString(string value) => WriteString(value);

    /// <inheritdoc cref="IPacket.WriteString(string, int)" />
    public Packet WriteString(string value, int position)
    {
        Position = position;
        return WriteString(value);
    }
    IPacket IPacket.WriteString(string value, int position) => WriteString(value, position);

    #region - Legacy -
    /// <inheritdoc cref="IReadOnlyPacket.ReadLegacyShort()" />
    public short ReadLegacyShort()
    {
        return Protocol switch
        {
            ClientType.Unity => ReadShort(),
            ClientType.Flash => (short)ReadInt(),
            _ => throw new InvalidOperationException("Cannot read legacy short, unknown protocol type.")
        };
    }

    /// <inheritdoc cref="IReadOnlyPacket.ReadLegacyFloat()" />
    public float ReadLegacyFloat()
    {
        return Protocol switch
        {
            ClientType.Unity => ReadFloat(),
            ClientType.Flash => ReadFloatAsString(),
            _ => throw new InvalidOperationException("Cannot read legacy float, unknown protocol type.")
        };
    }

    /// <inheritdoc cref="IReadOnlyPacket.ReadLegacyLong()" />
    public long ReadLegacyLong()
    {
        return Protocol switch
        {
            ClientType.Unity => ReadLong(),
            ClientType.Flash => ReadInt(),
            _ => throw new InvalidOperationException("Cannot read legacy long, unknown protocol type.")
        };
    }

    /// <inheritdoc cref="IPacket.WriteLegacyShort(short)" />
    public Packet WriteLegacyShort(short value)
    {
        return Protocol switch
        {
            ClientType.Unity => WriteShort(value),
            ClientType.Flash => WriteInt(value),
            _ => throw new InvalidOperationException("Cannot write legacy short, unknown protocol type.")
        };
    }
    IPacket IPacket.WriteLegacyShort(short value) => WriteLegacyShort(value);

    /// <inheritdoc cref="IPacket.WriteLegacyFloat(float)" />
    public Packet WriteLegacyFloat(float value)
    {
        return Protocol switch
        {
            ClientType.Unity => WriteFloat(value),
            ClientType.Flash => WriteFloatAsString(value),
            _ => throw new InvalidOperationException("Cannot write legacy float, unknown protocol type.")
        };
    }
    IPacket IPacket.WriteLegacyFloat(float value) => WriteLegacyFloat(value);

    /// <inheritdoc cref="IPacket.WriteLegacyLong(long)" />
    public Packet WriteLegacyLong(long value)
    {
        return Protocol switch
        {
            ClientType.Unity => WriteLong(value),
            ClientType.Flash => WriteInt((int)value),
            _ => throw new InvalidOperationException("Cannot write legacy long, unknown protocol type.")
        };
    }
    IPacket IPacket.WriteLegacyLong(long value) => WriteLegacyLong(value);
    #endregion

    #region - Replacement -
    private void ReplaceValue(object value)
    {
        ArgumentNullException.ThrowIfNull(value);

        switch (value)
        {
            case bool x: WriteBool(x); break;
            case byte x: WriteByte(x); break;
            case short x: WriteShort(x); break;
            case ushort x: WriteShort((short)x); break;
            case int x: WriteInt(x); break;
            case uint x: WriteInt((int)x); break;
            case float x: WriteLegacyFloat(x); break;
            case long x: WriteLegacyLong(x); break;
            case ulong x: WriteLegacyLong((long)x); break;
            case string x: ReplaceString(x); break;
            default: throw new ArgumentException($"Cannot replace value of type: {value.GetType().FullName}.");
        }
    }

    /// <inheritdoc cref="IPacket.ReplaceString(string)" />
    public Packet ReplaceString(string value)
    {
        int preStrLen = BinaryPrimitives.ReadInt16BigEndian(_buffer.Span[Position..]);
        if (Length < (Position + 2 + preStrLen))
            throw new InvalidOperationException($"Cannot replace string at position {Position}");

        int preLen = Length;
        int postStrLen = Encoding.UTF8.GetByteCount(value);

        int diff = postStrLen - preStrLen;
        if (diff > 0)
        {
            GrowToSize(Length + diff);
        }
        else if (diff < 0)
        {
            _buffer = _memoryOwner.Memory[..(Length + diff)];
        }
        else
        {
            Encoding.UTF8.GetBytes(value, _buffer.Span[(Position + 2)..]);
            Position += (2 + postStrLen);
            return this;
        }

        // offset bytes after string value
        _memoryOwner
            .Memory[(Position + 2 + preStrLen)..preLen]
            .CopyTo(_memoryOwner.Memory[(Position + 2 + postStrLen)..]);

        // write the new string length + bytes
        BinaryPrimitives.WriteInt16BigEndian(_buffer.Span[Position..], (short)postStrLen);
        Encoding.UTF8.GetBytes(value, _buffer.Span[(Position + 2)..]);

        Position += (2 + postStrLen);
        return this;
    }
    IPacket IPacket.ReplaceString(string value) => ReplaceString(value);

    /// <inheritdoc cref="IPacket.ReplaceString(string, int)" />
    public Packet ReplaceString(string value, int position)
    {
        Position = position;
        return ReplaceString(value);
    }
    IPacket IPacket.ReplaceString(string value, int position) => ReplaceString(value, position);

    /// <inheritdoc cref="IPacket.ModifyString(Func{string, string})" />
    public Packet ModifyString(Func<string, string> modifier)
    {
        int position = Position;
        string value = ReadString();
        ReplaceString(modifier(value), position);
        return this;
    }
    IPacket IPacket.ModifyString(Func<string, string> modifier) => ModifyString(modifier);

    /// <inheritdoc cref="IPacket.Replace(object[])" />
    public Packet Replace(params object[] values)
    {
        foreach (object value in values)
            ReplaceValue(value);

        return this;
    }
    IPacket IPacket.Replace(object[] values) => Replace(values);
    #endregion

    /// <inheritdoc cref="IPacket.Clear" />
    public Packet Clear()
    {
        _buffer = _buffer[0..0];
        Position = 0;
        return this;
    }

    /// <inheritdoc />
    IPacket IPacket.Clear() => Clear();

    /// <summary>
    /// Disposes of this packet and its memory.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="Dispose()" />
    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;

        if (disposing)
        {
            _memoryOwner.Dispose();
        }
    }
}
