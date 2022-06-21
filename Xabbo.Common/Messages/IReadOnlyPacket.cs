using System;

using Xabbo.Common;

namespace Xabbo.Messages;

/// <summary>
/// Represents a readable packet of binary data with a message header.
/// </summary>
public interface IReadOnlyPacket : IDisposable
{
    /// <summary>
    /// Gets the client protocol hint of the packet.
    /// </summary>
    ClientType Protocol { get; }

    /// <summary>
    /// Gets the message header of the packet.
    /// </summary>
    Header Header { get; }

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
    /// Returns whether a boolean can be read from the current position in the packet.
    /// </summary>
    /// <returns><c>true</c> if a byte can be read from the current position in the packet and its value is either <c>0</c> or <c>1</c>.</returns>
    bool CanReadBool();

    /// <summary>
    /// Returns whether a string can be read from the current position in the packet.
    /// </summary>
    bool CanReadString();

    /// <summary>
    /// Advances the packet's position by the specified number of bytes.
    /// </summary>
    IReadOnlyPacket Skip(int bytes);

    /// <summary>
    /// Reads a boolean from the current position in the packet.
    /// </summary>
    bool ReadBool();

    /// <summary>
    /// Reads a boolean from the specified position in the packet.
    /// </summary>
    bool ReadBool(int position);

    /// <summary>
    /// Reads a byte from the current position in the packet.
    /// </summary>
    byte ReadByte();

    /// <summary>
    /// Reads a byte from the specified position in the packet.
    /// </summary>
    byte ReadByte(int position);

    /// <summary>
    /// Reads a short from the current position in the packet.
    /// </summary>
    short ReadShort();

    /// <summary>
    /// Reads a  from the specified position in the packet.
    /// </summary>
    short ReadShort(int position);

    /// <summary>
    /// Reads an integer from the current position in the packet.
    /// </summary>
    int ReadInt();

    /// <summary>
    /// Reads an integer from the specified position in the packet.
    /// </summary>
    int ReadInt(int position);

    /// <summary>
    /// Reads a 32-bit floating point number from the current position in the packet.
    /// </summary>
    float ReadFloat();

    /// <summary>
    /// Reads a 32-bit floating point number from the specified position in the packet.
    /// </summary>
    float ReadFloat(int position);

    /// <summary>
    /// Reads a long from the current position in the packet.
    /// </summary>
    long ReadLong();

    /// <summary>
    /// Reads a long from the specified positions in the packet.
    /// </summary>
    long ReadLong(int position);

    /// <summary>
    /// Reads a string from the current position in the packet.
    /// </summary>
    string ReadString();

    /// <summary>
    /// Reads a string from the specified position in the packet.
    /// </summary>
    string ReadString(int position);

    /// <summary>
    /// Reads a string from the current position in the packet and parses it into a floating point number.
    /// </summary>
    float ReadFloatAsString();

    /// <summary>
    /// Reads a string from the specified position in the packet and parses it into a floating point number.
    /// </summary>
    float ReadFloatAsString(int position);

    /// <summary>
    /// Copies from the current position in the packet into the <see cref="Span{T}"/>.
    /// </summary>
    void Read(Span<byte> buffer);

    /// <summary>
    /// Copies from the specified position in the packet into the <see cref="Span{T}"/>
    /// </summary>
    void ReadBytes(Span<byte> buffer, int position);

    /// <summary>
    /// Reads a short if the packet's protocol is Unity,
    /// an int if it is Flash,
    /// or throws if unknown.
    /// </summary>
    short ReadLegacyShort();

    /// <summary>
    /// Reads a float if the packet's protocol is Unity,
    /// a float (as a string) if it is Flash,
    /// or throws if unknown.
    /// </summary>
    float ReadLegacyFloat();

    /// <summary>
    /// Reads a long if the packet's protocol is Unity,
    /// an int if it is Flash,
    /// or throws if unknown.
    /// </summary>
    long ReadLegacyLong();

    /// <summary>
    /// Creates a copy of this packet.
    /// </summary>
    IPacket Copy();
}
