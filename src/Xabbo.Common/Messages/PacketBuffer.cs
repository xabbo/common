using System;
using System.Buffers;

namespace Xabbo.Messages;

public sealed class PacketBuffer(int minimumCapacity = PacketBuffer.InitialCapacity) : IDisposable
{
    const int InitialCapacity = 32;

    private volatile bool _disposed;
    private IMemoryOwner<byte> _owner = MemoryPool<byte>.Shared.Rent(minimumCapacity);

    public int Length { get; private set; }
    public Span<byte> Span => _owner.Memory.Span[..Length];

    public PacketBuffer(ReadOnlySpan<byte> data)
        : this(data.Length)
    {
        Length = data.Length;
        data.CopyTo(Span);
    }

    public PacketBuffer(in ReadOnlySequence<byte> data)
        : this((int)data.Length)
    {
        Length = (int)data.Length;
        data.CopyTo(Span);
    }

    /// <summary>
    /// Grows the buffer to the specified length, if the current length is insufficient.
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
    /// Returns a span of `<paramref name="length"/>` bytes from the specified `<paramref name="start"/>` position.
    /// If `<paramref name="start"/>` + `<paramref name="length"/>` is greater than the packet's length,
    /// the buffer is grown to support the new length.
    /// `<paramref name="start"/>` may not be greater than the current packet length.
    /// </summary>
    public Span<byte> Alloc(int start, int length)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentOutOfRangeException.ThrowIfNegative(start);
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(start, Length);

        Grow(start + length);
        return Span[start..(start+length)];
    }

    /// <summary>
    /// Resizes a range of the buffer to the specified length.
    /// The tail portion of the buffer after the end of the range will be copied into the correct position.
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
            Span[(start+preLen)..^diff].CopyTo(Span[(start+length)..]);
        }
        else if (diff < 0)
        {
            Span[(start+preLen)..].CopyTo(Span[(start+length)..^-diff]);
            Length += diff;
        }

        return Span[start..(start+length)];
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