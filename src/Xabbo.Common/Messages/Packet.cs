using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Xabbo.Messages;

public sealed partial class Packet : IPacket
{
    private static string FormatFloat(float value)
        => value.ToString("0.0##############", CultureInfo.InvariantCulture);

    const int InitialCapacity = 32;

    private bool _disposed;
    private IMemoryOwner<byte> _memOwner;
    private Memory<byte> _buf;
    private int _position;

    public Header Header { get; set; } = Header.Unknown;

    public ClientType Client => Header.Client;

    public Span<byte> Buffer
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
            return _buf.Span;
        }
    }
    ReadOnlySpan<byte> IReadOnlyPacket.Buffer => Buffer;

    public Memory<byte> GetMemory()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return _buf;
    }
    ReadOnlyMemory<byte> IReadOnlyPacket.GetMemory() => GetMemory();

    public ref int Position => ref _position;

    public int Length => _buf.Length;

    public int Available => Length - Position;

    /// <summary>
    /// Constructs a new packet with the specified protocol and message header.
    /// </summary>
    /// <param name="header">The message header.</param>
    /// <param name="capacity">The initial capacity in bytes.</param>
    public Packet(Header header, int capacity = InitialCapacity)
    {
        _memOwner = MemoryPool<byte>.Shared.Rent(capacity);
        _buf = _memOwner.Memory[..0];

        Header = header;
    }

    /// <summary>
    /// Constructs a new packet with the specified protocol and header,
    /// copying the <see cref="ReadOnlySpan{T}"/> into its internal buffer.
    /// </summary>
    public Packet(Header header, ReadOnlySpan<byte> data)
        : this(header, data.Length)
    {
        _buf = _memOwner.Memory[..data.Length];
        data.CopyTo(_buf.Span);
    }

    /// <summary>
    /// Constructs a new packet with the specified protocol and header,
    /// copying the <see cref="ReadOnlySequence{T}"/> into its internal buffer.
    /// </summary>
    public Packet(Header header, in ReadOnlySequence<byte> data)
        : this(header, (int)data.Length)
    {
        _buf = _memOwner.Memory[..(int)data.Length];
        data.CopyTo(_buf.Span);
    }

    /// <inheritdoc cref="IReadOnlyPacket.Copy" />
    public Packet Copy() => new(Header, Buffer);
    IPacket IReadOnlyPacket.Copy() => Copy();

    /// <summary>
    /// Grows the internal buffer to the specified minimum size, if its size is insufficient.
    /// </summary>
    private void EnsureLength(int len, bool shrink = false)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        int cap = _memOwner.Memory.Length;
        if (cap < len)
        {
            while (cap < len)
                cap <<= 1;

            using IMemoryOwner<byte> oldMemoryOwner = _memOwner;
            _memOwner = MemoryPool<byte>.Shared.Rent(cap);
            oldMemoryOwner.Memory.CopyTo(_memOwner.Memory);
        }

        if (_buf.Length < len || (shrink && _buf.Length != len))
            _buf = _memOwner.Memory[..len];
    }

    public void Clear()
    {
        _buf = _buf[0..0];
        Position = 0;
    }

    /// <summary>
    /// Disposes of this packet and its memory.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;

        if (disposing)
        {
            _memOwner.Dispose();
        }
    }

    #region Read
    private ReadOnlySpan<byte> ReadSpan(int n, ref int pos)
    {
        if (pos + n > Length)
            throw new Exception("Not enough bytes to read value.");
        pos += n;
        return _buf.Span[(pos - n)..pos];
    }

    public void Read(Span<byte> buffer) => Read(buffer, ref _position);
    public void Read(Span<byte> buffer, int position) => Read(buffer, ref position);
    public void Read(Span<byte> buffer, ref int pos)
    {
        Buffer[Position..(Position + buffer.Length)].CopyTo(buffer);
        pos += buffer.Length;
    }

    public T Read<T>() => Read<T>(ref _position);
    public T Read<T>(int position) => Read<T>(ref position);
    public T Read<T>(ref int pos)
    {
        switch (Type.GetTypeCode(typeof(T)))
        {
            case TypeCode.Boolean:
                return (T)(object)(_buf.Span[pos++] != 0);
            case TypeCode.Byte:
                return (T)(object)_buf.Span[pos++];
            case TypeCode.Int16:
                return Client switch
                {
                    ClientType.Shockwave => (T)(object)(short)Read<B64>(ref pos),
                    _ => (T)(object)BinaryPrimitives.ReadInt16BigEndian(ReadSpan(2, ref pos)),
                };
            case TypeCode.Int32:
                return Client switch
                {
                    ClientType.Shockwave => (T)(object)(int)Read<VL64>(ref pos),
                    _ => (T)(object)BinaryPrimitives.ReadInt32BigEndian(ReadSpan(4, ref pos)),
                };
            case TypeCode.Int64:
                if (Client == ClientType.Flash || Client == ClientType.Shockwave)
                    throw new Exception($"Cannot read long on a {Enum.GetName(Client)} session.");
                return (T)(object)BinaryPrimitives.ReadInt64BigEndian(ReadSpan(8, ref pos));
            case TypeCode.Single:
                return Client switch
                {
                    ClientType.Unity => (T)(object)BinaryPrimitives.ReadSingleBigEndian(ReadSpan(4, ref pos)),
                    _ => (T)(object)float.Parse(Read<string>(ref pos)),
                };
            case TypeCode.String:
                if (Client == ClientType.Shockwave)
                {
                    if (Header.Direction == Direction.In)
                    {
                        if (pos >= Length)
                            throw new Exception("Not enough bytes to read value.");
                        int start = pos, end;
                        int terminator = _buf.Span[pos..].IndexOf<byte>(2);
                        if (terminator == -1)
                        {
                            pos = _buf.Length;
                            end = pos;
                        }
                        else
                        {
                            pos = pos + terminator + 1;
                            end = pos - 1;
                        }
                        return (T)(object)Encoding.UTF8.GetString(_buf.Span[start..end]);
                    }
                    else if (Header.Direction != Direction.Out)
                    {
                        throw new Exception("Unknown direction when reading string on Shockwave.");
                    }
                }
                return (T)(object)Encoding.UTF8.GetString(ReadSpan(2 + Read<short>(pos), ref pos)[2..]);
            default:
                if (typeof(T) == typeof(B64))
                {
                    if (Client != ClientType.Shockwave)
                        throw new Exception($"Cannot read B64 on session: {Client}.");
                    return (T)(object)B64.Decode(ReadSpan(2, ref pos));
                }
                if (typeof(T) == typeof(VL64))
                {
                    if (Client != ClientType.Shockwave)
                        throw new Exception($"Cannot read VL64 on session: {Client}.");
                    return (T)(object)VL64.Decode(ReadSpan(VL64.DecodeLength(_buf.Span[pos]), ref pos));
                }
                if (typeof(T) == typeof(Id)) return Client switch
                {
                    ClientType.Unity => (T)(object)(Id)Read<long>(ref pos),
                    ClientType.Flash or ClientType.Shockwave => (T)(object)(Id)Read<int>(ref pos),
                    _ => throw new Exception($"Cannot read Id on session: {Client}."),
                };
                if (typeof(T) == typeof(Length)) return Client switch
                {
                    ClientType.Unity => (T)(object)(Length)Read<short>(ref pos),
                    _ => (T)(object)(Length)Read<int>(ref pos),
                };
                if (typeof(T) == typeof(FloatString))
                {
                    return (T)(object)(FloatString)float.Parse(Read<string>(ref pos));
                }
                throw new Exception($"Cannot read {typeof(T)} from packet.");
        }
    }

    public T[] ReadArray<T>() => ReadArray<T>(ref _position);
    public T[] ReadArray<T>(int pos) => ReadArray<T>(ref pos);
    public T[] ReadArray<T>(ref int pos)
    {
        T[] array = new T[Read<Length>(ref pos)];
        for (int i = 0; i < array.Length; i++)
            array[i] = Read<T>(ref pos);
        return array;
    }
    #endregion

    #region Write
    public Span<byte> Allocate(int n) => Allocate(n, ref _position);
    public Span<byte> Allocate(int n, int pos) => Allocate(n, ref pos);
    public Span<byte> Allocate(int n, scoped ref int pos)
    {
        EnsureLength(pos + n);
        Span<byte> span = _buf.Span[pos..(pos + n)];
        pos += n;
        return span;
    }

    public void Write(ReadOnlySpan<byte> buffer) => Write(buffer, ref _position);
    public void Write(ReadOnlySpan<byte> buffer, int position) => Write(buffer, ref position);
    public void Write(ReadOnlySpan<byte> buffer, ref int pos) => buffer.CopyTo(Allocate(buffer.Length, ref pos));

    public void Write<T>(T value) => Write(value, ref _position);
    public void Write<T>(T value, int position) => Write(value, ref position);
    public void Write<T>(T value, ref int pos)
    {
        ArgumentNullException.ThrowIfNull(value);

        switch (value)
        {
            case IComposable v:
                v.Compose(this, ref pos);
                break;
            case bool v:
                Allocate(1, ref pos)[0] = (byte)(v ? 1 : 0);
                break;
            case byte v:
                Allocate(1, ref pos)[0] = v;
                break;
            case short v:
                if (Client == ClientType.Shockwave)
                    Write<B64>(v, ref pos);
                else
                    BinaryPrimitives.WriteInt16BigEndian(Allocate(2, ref pos), v);
                break;
            case int v:
                if (Client == ClientType.Shockwave)
                    Write<VL64>(v, ref pos);
                else
                    BinaryPrimitives.WriteInt32BigEndian(Allocate(4, ref pos), v);
                break;
            case long v:
                if (Client == ClientType.Flash || Client == ClientType.Shockwave)
                    throw new Exception($"Cannot write long on session: {Client}.");
                BinaryPrimitives.WriteInt64BigEndian(Allocate(8, ref pos), v);
                break;
            case float v:
                if (Client == ClientType.Unity)
                    BinaryPrimitives.WriteSingleBigEndian(Allocate(4, ref pos), v);
                else
                    Write<string>(FormatFloat(v), ref pos);
                break;
            case string v:
                int len = Encoding.UTF8.GetByteCount(v);
                if (Client == ClientType.Shockwave)
                {
                    if (Header.Direction == Direction.In)
                    {
                        Span<byte> span = Allocate(len+1, ref pos);
                        Encoding.UTF8.GetBytes(v, span);
                        span[^1] = 2;
                        break;
                    }
                    else if (Header.Direction != Direction.Out)
                        throw new Exception("Unknown direction when writing string on Shockwave.");
                }
                Write((short)len, ref pos);
                Encoding.UTF8.GetBytes(v, Allocate(len, ref pos));
                break;
            case B64 v:
                if (Client != ClientType.Shockwave)
                    throw new Exception($"Cannot write B64 on session: {Client}.");
                B64.Encode(Allocate(2, ref pos), v);
                break;
            case VL64 v:
                if (Client != ClientType.Shockwave)
                    throw new Exception($"Cannot write VL64 on session: {Client}.");
                VL64.Encode(Allocate(VL64.EncodeLength(v), ref pos), v);
                break;
            case Id v:
                switch (Client)
                {
                    case ClientType.Unity:
                        Write<long>(v.Value, ref pos);
                        break;
                    case ClientType.Flash or ClientType.Shockwave:
                        Write((int)v.Value, ref pos);
                        break;
                    default:
                        throw new Exception($"Cannot write Id on session: {Client}.");
                }
                break;
            case Length v:
                switch (Client)
                {
                    case ClientType.Unity:
                        Write((short)v.Value, ref pos);
                        break;
                    default:
                        Write<int>(v.Value, ref pos);
                        break;
                }
                break;
            case FloatString v:
                Write<string>(FormatFloat(v.Value), ref pos);
                break;
            case IEnumerable v:
                int start = pos, n = 0;
                Write<Length>(0, ref pos);
                foreach (var item in v)
                {
                    Write<object>(item, ref pos);
                    n++;
                }
                Replace<Length>(n, start);
                break;
            default:
                throw new Exception($"Cannot write {typeof(T)} to packet.");
        }
    }
    #endregion

    #region Replace
    /// <summary>
    /// Stretches (expands or shrinks) a range of the buffer and returns the span of the new range.
    /// <paramref name="pos"/> is advanced to the index after the end of the new range.
    /// </summary>
    private Span<byte> Stretch(int preLen, int postLen, ref int pos)
    {
        int start = pos;
        int diff = postLen - preLen;
        int preTailStart = pos + preLen;
        int postTailStart = pos + postLen;
        if (diff > 0)
        {
            EnsureLength(_buf.Length + diff);
            _buf[preTailStart..^diff].CopyTo(_buf[postTailStart..]);
            pos += postLen;
            return _buf.Span[start..(start+postLen)];
        }
        else if (diff < 0)
        {
            _buf[(pos+preLen)..].CopyTo(_buf[postTailStart..^-diff]);
            EnsureLength(_buf.Length + diff, true);
            pos += postLen;
            return _buf.Span[start..(start+postLen)];
        }
        else
        {
            pos += preLen;
            return _buf.Span[start..(start+preLen)];
        }
    }

    public void Replace<T>(T value) => Replace(value, ref _position);
    public void Replace<T>(T value, int pos) => Replace(value, ref pos);
    public void Replace<T>(T value, ref int pos)
    {
        ArgumentNullException.ThrowIfNull(value);

        switch (value)
        {
            case int v when Client == ClientType.Shockwave:
                Replace<VL64>(v, ref pos);
                break;
            case float v when Client == ClientType.Flash || Client == ClientType.Shockwave:
                Replace<string>(FormatFloat(v), ref pos);
                break;
            case string v when Client == ClientType.Shockwave && Header.Direction == Direction.In:
                {
                    int end = _buf.Span[pos..].IndexOf<byte>(2);
                    int newLen = Encoding.UTF8.GetByteCount(v);
                    if (end < 0) {
                        Encoding.UTF8.GetBytes(v, Stretch(_buf.Length - pos, newLen, ref pos));
                    } else {
                        Encoding.UTF8.GetBytes(v, Stretch(end - pos, newLen + 1, ref pos)[..^1]);
                        _buf.Span[pos-1] = 2;
                    }
                }
                break;
            case string v:
                {
                    int preLen = Read<short>(pos);
                    int postLen = Encoding.UTF8.GetByteCount(v);
                    Write((short)postLen, ref pos);
                    Encoding.UTF8.GetBytes(v, Stretch(preLen, postLen, ref pos));
                }
                break;
            case VL64 v:
                VL64.Encode(Stretch(VL64.DecodeLength(_buf.Span[pos]), VL64.EncodeLength(v), ref pos), v);
                break;
            case Id v when Client == ClientType.Shockwave:
                Replace<VL64>((int)v, ref pos);
                break;
            case Length v when Client == ClientType.Shockwave:
                Replace<VL64>((int)v, ref pos);
                break;
            case IComposable:
                throw new Exception("Cannot replace IComposable.");
            default:
                Write<object>(value, ref pos);
                break;
        }
    }

    public void Modify<T>(Func<T, T> transform) => Modify(transform, ref _position);
    public void Modify<T>(Func<T, T> transform, int pos) => Modify(transform, ref pos);
    public void Modify<T>(Func<T, T> transform, ref int pos) => Replace(transform(Read<T>(pos)), ref pos);
    #endregion

    #region Parse
    public T Parse<T>() where T : IParsable<T> => T.Parse(this, ref _position);
    public T Parse<T>(int pos) where T : IParsable<T> => T.Parse(this, ref pos);
    public T Parse<T>(ref int pos) where T : IParsable<T> => T.Parse(this, ref pos);

    public T[] ParseAll<T>() where T : IParsable<T>, IManyParsable<T>
        => ParseAll<T>(ref _position);
    public T[] ParseAll<T>(int pos) where T : IParsable<T>, IManyParsable<T>
        => ParseAll<T>(ref _position);
    public T[] ParseAll<T>(ref int pos) where T : IParsable<T>, IManyParsable<T>
    {
        T[] array = new T[Read<Length>(ref pos)];
        for (int i = 0; i < array.Length; i++)
            array[i] = Parse<T>(ref pos);
        return array;
    }
    #endregion

    #region Compose
    public void Compose(IComposable value) => Compose(value, ref _position);
    public void Compose(IComposable value, int pos) => Compose(value, ref pos);
    public void Compose(IComposable value, ref int pos) => value.Compose(this, ref pos);
    #endregion
}
