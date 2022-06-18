using System;
using System.IO;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections;
using System.Text;
using System.Globalization;

using Xabbo.Common;

namespace Xabbo.Messages;

/// <inheritdoc />
public partial class Packet : IPacket
{
    public static readonly Type
        Byte = typeof(byte),
        Bool = typeof(bool),
        Short = typeof(short),
        Int = typeof(int),
        Float = typeof(float),
        Long = typeof(long),
        String = typeof(string),
        ByteArray = typeof(byte[]);

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
            if (_disposed)
                throw new ObjectDisposedException(nameof(Packet));
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
    public int Length { get; private set; }

    /// <inheritdoc />
    public int Available => Length - Position;

    /// <summary>
    /// Constructs a new packet.
    /// </summary>
    public Packet()
        : this(ClientType.Unknown, Header.Unknown)
    { }

    /// <summary>
    /// Constructs a new packet with the specified message header.
    /// </summary>
    public Packet(Header header)
        : this(ClientType.Unknown, header)
    { }

    /// <summary>
    /// Constructs a new packet with the specified protocol and message header.
    /// </summary>
    /// <param name="protocol">The packet protocol hint.</param>
    /// <param name="header">The message header.</param>
    /// <param name="initialSize">The initial size in bytes of the packet data buffer.</param>
    public Packet(ClientType protocol, Header header, int initialSize = 8)
    {
        _memoryOwner = MemoryPool<byte>.Shared.Rent(initialSize);
        _buffer = _memoryOwner.Memory[..0];

        Protocol = protocol;
        Header = header;
    }

    /// <summary>
    /// Constructs a new packet with the specified protocol and header,
    /// copying the <see cref="ReadOnlySpan{T}"/> into its internal buffer.
    /// </summary>
    public Packet(ClientType protocol, Header header, ReadOnlySpan<byte> data)
        : this(protocol, header, data.Length)
    {
        Length = data.Length;
        _buffer = _memoryOwner.Memory[..Length];
        data.CopyTo(_buffer.Span);
    }

    /// <summary>
    /// Constructs a new packet with the specified header,
    /// copying the <see cref="ReadOnlySpan{T}"/> into its internal buffer.
    /// </summary>
    public Packet(Header header, ReadOnlySpan<byte> data)
        : this(ClientType.Unknown, header, data)
    { }

    /// <summary>
    /// Constructs a new packet with the specified protocol and header,
    /// copying the <see cref="ReadOnlySequence{T}"/> into its internal buffer.
    /// </summary>
    public Packet(ClientType protocol, Header header, in ReadOnlySequence<byte> data)
        : this(protocol, header, (int)data.Length)
    {
        Length = (int)data.Length;
        _buffer = _memoryOwner.Memory[..Length];
        data.CopyTo(_buffer.Span);
    }

    /// <summary>
    /// Constructs a new packet with the specified header,
    /// copying the <see cref="ReadOnlySequence{T}"/> into its internal buffer.
    /// </summary>
    public Packet(Header header, in ReadOnlySequence<byte> data)
        : this(ClientType.Unknown, header, data)
    { }

    /// <inheritdoc cref="IReadOnlyPacket.Copy" />
    public Packet Copy()
    {
        return new Packet(
            Protocol,
            Header,
            Buffer
        );
    }
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
        if (_disposed)
            throw new ObjectDisposedException(nameof(Packet));

        int size = _memoryOwner.Memory.Length;
        if (size < minSize)
        {
            while (size < minSize)
                size <<= 1;

            using IMemoryOwner<byte> oldMemoryOwner = _memoryOwner;
            _memoryOwner = MemoryPool<byte>.Shared.Rent(size);
            oldMemoryOwner.Memory.CopyTo(_memoryOwner.Memory);
        }
        
        if (Length < minSize)
        {
            Length = minSize;
            _buffer = _memoryOwner.Memory[..Length];
        }
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

    public string ReadString(int position)
    {
        Position = position;
        return ReadString();
    }

    public float ReadFloatAsString()
    {
        return float.Parse(ReadString(), CultureInfo.InvariantCulture);
    }

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
        return WriteString(value, position);
    }
    IPacket IPacket.WriteString(string value, int position) => WriteString(value, position);

    /*public Packet WriteValues(params object[] values)
    {
        foreach (object value in values)
        {
            switch (value)
            {
                case byte x: WriteByte(x); break;
                case bool x: WriteBool(x); break;
                case short x: WriteShort(x); break;
                case ushort x: WriteShort((short)x); break;
                case LegacyShort x: WriteLegacyShort(x); break;
                case int x: WriteInt(x); break;
                case long x: WriteLegacyLong(x); break;
                case LegacyLong x: WriteLegacyLong(x); break;
                case byte[] x:
                    WriteInt(x.Length);
                    WriteBytes(x);
                    break;
                case string x: WriteString(x); break;
                case float x: WriteFloat(x); break;
                case LegacyFloat x: WriteLegacyFloat(x); break;
                case IComposable x: x.Compose(this); break;
                case ICollection x:
                    {
                        WriteLegacyShort((short)x.Count);
                        foreach (object o in x)
                            WriteValues(o);
                    }
                    break;
                case IEnumerable x:
                    {
                        int count = 0, startPosition = Position;
                        WriteLegacyShort(-1);
                        foreach (object o in x)
                        {
                            WriteValues(o);
                            count++;
                        }
                        int endPosition = Position;
                        Position = startPosition;
                        WriteLegacyShort((short)count);
                        Position = endPosition;
                    }
                    break;
                default:
                    if (value == null)
                        throw new Exception("Null value");
                    else
                        throw new Exception($"Invalid value type: {value.GetType().Name}");
            }
        }

        return this;
    }*/

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
            Length += diff;
            _buffer = _memoryOwner.Memory[..Length];
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
    #endregion

    /// <summary>
    /// Disposes of this packet and its memory.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="Dispose()" />
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;

        if (disposing)
        {
            _memoryOwner.Dispose();
        }
    }
}
