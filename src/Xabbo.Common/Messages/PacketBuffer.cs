using System;
using System.Buffers;

namespace Xabbo.Messages;

/// <summary>
/// Represents a resizable buffer backed by a rented memory span
/// of <see cref="byte"/> from a <see cref="MemoryPool{T}"/> .
/// </summary>
/// <param name="minimumCapacity">The initial minimum capacity of the rented span of bytes.</param>
public sealed class PacketBuffer(int minimumCapacity = PacketBuffer.InitialCapacity) : IDisposable
{
    const int InitialCapacity = 32;

    private volatile bool _disposed;
    private IMemoryOwner<byte> _owner = MemoryPool<byte>.Shared.Rent(minimumCapacity);

    /// <summary>
    /// Gets the length of the buffer.
    /// </summary>
    public int Length { get; private set; }

    /// <summary>
    /// Gets the memory of the buffer as a <see cref="Span{T}"/>.
    /// </summary>
    public Span<byte> Span => _owner.Memory.Span[..Length];

    /// <summary>
    /// Constructs a new buffer and copies the specified <see cref="ReadOnlySpan{T}"/> into its internal memory.
    /// </summary>
    /// <param name="data">The data to copy into this buffer.</param>
    public PacketBuffer(ReadOnlySpan<byte> data)
        : this(data.Length)
    {
        Length = data.Length;
        data.CopyTo(Span);
    }

    /// <summary>
    /// Constructs a new buffer and copies the specified <see cref="ReadOnlySequence{T}"/> into its internal memory.
    /// </summary>
    /// <param name="data">The data to copy into this buffer.</param>
    public PacketBuffer(in ReadOnlySequence<byte> data)
        : this((int)data.Length)
    {
        Length = (int)data.Length;
        data.CopyTo(Span);
    }

    /// <summary>
    /// Grows the buffer to the specified length if the current length is insufficient.
    /// </summary>
    public void Grow(int min)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentOutOfRangeException.ThrowIfNegative(min);

        if (_owner.Memory.Length < min)
        {
            int capacity = _owner.Memory.Length;
            while (capacity < min)
                capacity <<= 1;

            using IMemoryOwner<byte> old = _owner;
            _owner = MemoryPool<byte>.Shared.Rent(capacity);
            old.Memory.CopyTo(_owner.Memory);
        }

        if (Length < min)
            Length = min;
    }

    /// <summary>
    /// Allocates <paramref name="length"/> bytes from the <paramref name="start"/> position
    /// and returns the allocated range as a <see cref="Span{T}"/> of bytes.
    /// <para/>
    /// If <paramref name="start"/>+<paramref name="length"/> is greater than the buffer length,
    /// the buffer is grown to support the new length.
    /// <para/>
    /// <paramref name="start"/> may not be greater than the current length.
    /// </summary>
    public Span<byte> Allocate(int start, int length)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentOutOfRangeException.ThrowIfNegative(start);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(start, Length);

        Grow(start + length);
        return Span[start..(start + length)];
    }

    /// <summary>
    /// Resizes the specified range of the buffer to the specified length and returns
    /// the resized range as a <see cref="Span{T}"/> of bytes.
    /// <para/>
    /// The tail portion of the buffer after the end of the range will be copied into
    /// the correct position.
    /// <para/>
    /// If the range grows in length, the expanded portion of the range is not cleared,
    /// so it is the caller's responsibility to write valid data to the resulting span.
    /// </summary>
    public Span<byte> Resize(Range range, int length)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentOutOfRangeException.ThrowIfNegative(length);

        var (start, preLen) = range.GetOffsetAndLength(Length);
        int diff = length - preLen;

        if (diff > 0)
        {
            Grow(Length + diff);
            Span[(start + preLen)..^diff].CopyTo(Span[(start + length)..]);
        }
        else if (diff < 0)
        {
            Span[(start + preLen)..].CopyTo(Span[(start + length)..^-diff]);
            Length += diff;
        }

        return Span[start..(start + length)];
    }

    /// <summary>
    /// Resets the buffer's length to zero.
    /// </summary>
    public void Clear()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        Length = 0;
    }

    /// <summary>
    /// Disposes of this buffer and releases its memory back to the memory pool.
    /// </summary>
    public void Dispose()
    {
        if (_disposed) return;

        _disposed = true;
        _owner.Dispose();
    }

    /// <summary>
    /// Creates a new copy of this buffer.
    /// </summary>
    public PacketBuffer Copy() => new(Span);
}
