using System;
using System.Collections;
using System.Collections.Generic;

namespace Xabbo.Messages;

/// <summary>
/// Represents a readable packet of binary data with a message header.
/// </summary>
public interface IReadOnlyPacket : IDisposable
{
    /// <summary>
    /// Gets the packet's message header.
    /// </summary>
    Header Header { get; }

    /// <summary>
    /// Gets the client type of the packet header.
    /// </summary>
    ClientType Client { get; }

    /// <summary>
    /// Gets or sets the current position in the packet.
    /// </summary>
    int Position { get; set; }

    /// <summary>
    /// Gets the length of the data in the packet.
    /// </summary>
    int Length { get; }

    /// <summary>
    /// Gets the number of available bytes left in the packet.
    /// </summary>
    int Available { get;  }

    /// <summary>
    /// Gets the underlying buffer of the packet's data as a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    ReadOnlySpan<byte> Buffer { get; }

    /// <summary>
    /// Gets the underlying buffer of the packet's data as <see cref="ReadOnlyMemory{T}"/>.
    /// </summary>
    ReadOnlyMemory<byte> GetMemory();

    /// <summary>
    /// Reads a value from the current position in the packet.
    /// </summary>
    T Read<T>();
    /// <summary>
    /// Reads a value from the specified position in the packet.
    /// </summary>
    T Read<T>(int position);
    /// <summary>
    /// Reads a value from the specified position in the packet and advances the position.
    /// </summary>
    T Read<T>(ref int position);

    /// <summary>
    /// Reads an array of values from the current position in the packet.
    /// </summary>
    T[] ReadArray<T>();
    /// <summary>
    /// Reads an array of values from the specified position in the packet.
    /// </summary>
    T[] ReadArray<T>(int position);
    /// <summary>
    /// Reads an array of values from the specified position in the packet and advances the position.
    /// </summary>
    T[] ReadArray<T>(ref int position);

    /// <summary>
    /// Parses an object from the current position in the packet.
    /// </summary>
    T Parse<T>() where T : IParsable<T>;
    /// <summary>
    /// Parses an object from the specified position in the packet.
    /// </summary>
    T Parse<T>(int position) where T : IParsable<T>;
    /// <summary>
    /// Parses an object from the specified position in the packet and advances the position.
    /// </summary>
    T Parse<T>(ref int position) where T : IParsable<T>;

    /// <summary>
    /// Parses an array of objects from the current position in the packet.
    /// </summary>
    T[] ParseAll<T>() where T : IParsable<T>, IManyParsable<T>;
    /// <summary>
    /// Parses an array of objects from the specified position in the packet.
    /// </summary>
    T[] ParseAll<T>(int pos) where T : IParsable<T>, IManyParsable<T>;
    /// <summary>
    /// Parses an array of objects from the specified position in the packet and advances the position.
    /// </summary>
    T[] ParseAll<T>(ref int pos) where T : IParsable<T>, IManyParsable<T>;

    /// <summary>
    /// Creates a copy of this packet.
    /// </summary>
    IPacket Copy();
}
