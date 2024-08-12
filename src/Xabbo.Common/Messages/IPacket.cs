using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents a writable packet of binary data with a message header.
/// </summary>
public interface IPacket : IReadOnlyPacket
{
    /// <summary>
    /// Gets or sets the message header of the packet.
    /// </summary>
    new Header Header { get; set; }

    /// <summary>
    /// Gets the underlying buffer of the packet's data as a <see cref="Span{T}"/>.
    /// </summary>
    new Span<byte> Buffer { get; }

    /// <summary>
    /// Gets the underlying buffer of the packet's data as <see cref="Memory{T}"/>.
    /// </summary>
    new Memory<byte> GetMemory();

    /// <summary>
    /// Allocates a byte <see cref="Span{T}" /> of the specified length from the current position in the packet.
    /// </summary>
    /// <param name="n">The number of bytes to allocate.</param>
    Span<byte> Allocate(int n);
    /// <summary>
    /// Allocates a byte <see cref="Span{T}" /> of the specified length
    /// from the specified position in the packet.
    /// </summary>
    /// <param name="n">The number of bytes to allocate.</param>
    /// <param name="pos">The position from which to allocate from.</param>
    Span<byte> Allocate(int n, int pos);
    /// <summary>
    /// Allocates a byte <see cref="Span{T}" /> of the specified length
    /// from the specified position in the packet and advances the position.
    /// </summary>
    /// <param name="n">The number of bytes to allocate.</param>
    /// <param name="pos">The position from which to allocate from.</param>
    Span<byte> Allocate(int n, ref int pos);

    /// <summary>
    /// Writes a value at the current position in the packet.
    /// </summary>
    void Write<T>(T value);
    /// <summary>
    /// Writes a value at the specified position in the packet.
    /// </summary>
    void Write<T>(T value, int pos);
    /// <summary>
    /// Writes a value at the specified position in the packet and advances the position.
    /// </summary>
    void Write<T>(T value, ref int pos);

    /// <summary>
    /// Replaces a value at the current position in the packet.
    /// </summary>
    void Replace<T>(T value);
    /// <summary>
    /// Replaces a value at the specified position in the packet.
    /// </summary>
    void Replace<T>(T value, int pos);
    /// <summary>
    /// Replaces a value at the specified position in the packet and advances the position.
    /// </summary>
    void Replace<T>(T value, ref int pos);

    /// <summary>
    /// Modifies a value at the current position in the packet.
    /// </summary>
    void Modify<T>(Func<T, T> transform);
    /// <summary>
    /// Modifies a value at the specified position in the packet.
    /// </summary>
    void Modify<T>(Func<T, T> transform, int pos);
    /// <summary>
    /// Modifies a value at the specified position in the packet and advances the position.
    /// </summary>
    void Modify<T>(Func<T, T> transform, ref int pos);

    /// <summary>
    /// Clears the packet's buffer and resets its position.
    /// </summary>
    void Clear();
}
