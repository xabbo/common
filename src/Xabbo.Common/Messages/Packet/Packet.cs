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
