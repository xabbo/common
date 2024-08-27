using System;
using System.Collections.Generic;

namespace Xabbo.Messages;

/// <summary>
/// Represents a packet of binary data with a message header.
/// </summary>
public interface IPacket : IDisposable
{
    /// <summary>
    /// Gets or sets the message header of the packet.
    /// </summary>
    Header Header { get; set; }

    /// <summary>
    /// Gets the client type of the packet's header.
    /// </summary>
    ClientType Client { get; }

    /// <summary>
    /// Gets the packet's buffer.
    /// </summary>
    PacketBuffer Buffer { get; }

    /// <summary>
    /// Gets a reference to the current position in the packet.
    /// </summary>
    ref int Position { get; }

    /// <summary>
    /// Gets the length of the packet.
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Reads a value from the current position in the packet.
    /// </summary>
    T Read<T>();

    /// <summary>
    /// Reads a value from the specified position in the packet.
    /// </summary>
    T ReadAt<T>(int pos);

    /// <summary>
    /// Reads an array of values from the current position in the packet.
    /// </summary>
    T[] ReadArray<T>();

    /// <summary>
    /// Reads an array of values from the specified position in the packet.
    /// </summary>
    T[] ReadArrayAt<T>(int pos);

    /// <summary>
    /// Parses an object from the current position in the packet.
    /// </summary>
    T Parse<T>() where T : IParser<T>;

    /// <summary>
    /// Parses an object from the specified position in the packet.
    /// </summary>
    T ParseAt<T>(int pos) where T : IParser<T>;

    /// <summary>
    /// Parses an array of objects implementing <see cref="IParser{T}"/> from the current position in the packet.
    /// </summary>
    T[] ParseArray<T>() where T : IParser<T>;

    /// <summary>
    /// Parses an array of objects implementing <see cref="IParser{T}"/> from the specified position in the packet.
    /// </summary>
    T[] ParseArrayAt<T>(int pos) where T : IParser<T>;

    /// <summary>
    /// Parses an array of objects implementing <see cref="IManyParser{T}"/> from the current position in the packet.
    /// </summary>
    T[] ParseAll<T>() where T : IParser<T>, IManyParser<T>;

    /// <summary>
    /// Parses an array of objects implementing <see cref="IManyParser{T}"/> from the specified position in the packet.
    /// </summary>
    T[] ParseAllAt<T>(int pos) where T : IParser<T>, IManyParser<T>;

    /// <summary>
    /// Writes a value at the current position in the packet.
    /// </summary>
    void Write<T>(T value);

    /// <summary>
    /// Writes a value at the specified position in the packet.
    /// </summary>
    void WriteAt<T>(int pos, T value);

    /// <summary>
    /// Composes a value at the current position in the packet.
    /// </summary>
    public void Compose<T>(T value) where T : IComposer;
    /// <summary>
    /// Composes a value at the specified position in the packet.
    /// </summary>
    public void ComposeAt<T>(int pos, T value) where T : IComposer;
    /// <summary>
    /// Composes the values at the current position in the packet.
    /// </summary>
    public void ComposeAll<T>(IEnumerable<T> values) where T : IComposer, IManyComposer<T>;
    /// <summary>
    /// Composes the values at the specified position in the packet.
    /// </summary>
    public void ComposeAllAt<T>(int pos, IEnumerable<T> values) where T : IComposer, IManyComposer<T>;

    /// <summary>
    /// Replaces a value at the current position in the packet.
    /// </summary>
    void Replace<T>(T value);

    /// <summary>
    /// Replaces a value at the specified position in the packet.
    /// </summary>
    void ReplaceAt<T>(int pos, T value);

    /// <summary>
    /// Modifies a value at the current position in the packet.
    /// </summary>
    void Modify<T>(Func<T, T> transform);

    /// <summary>
    /// Modifies a value at the specified position in the packet.
    /// </summary>
    void ModifyAt<T>(int pos, Func<T, T> transform);

    /// <summary>
    /// Clears the packet's buffer and resets its position.
    /// </summary>
    void Clear();

    /// <summary>
    /// Creates a copy of this packet.
    /// </summary>
    IPacket Copy();
}
