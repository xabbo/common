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
