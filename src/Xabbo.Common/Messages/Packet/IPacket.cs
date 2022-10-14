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

    /// <inheritdoc cref="IReadOnlyPacket.Skip(int)" />
    new IPacket Skip(int bytes);

    /// <inheritdoc cref="IReadOnlyPacket.Skip(Type[])" />
    new IPacket Skip(params Type[] types);

    /// <summary>
    /// <para>
    /// Returns a span of bytes of the specified length from the current position in the packet that maps to the packet's internal buffer.
    /// If the length of the packet is insufficient, it will be increased and its internal buffer may be grown.
    /// Any bytes that already exist after the current position in the packet will be included from the start of the resulting span.
    /// Advances the packet's position by the specified length.
    /// </para>
    /// <para>
    /// Should be used with methods that write directly to a span without needing to allocate a new byte array, such as <see cref="System.Text.Encoding.GetBytes(ReadOnlySpan{char}, Span{byte})"/>.
    /// </para>
    /// </summary>
    Span<byte> GetSpan(int length);

    /// <summary>
    /// Writes a composable object to the packet.
    /// </summary>
    IPacket Write(IComposable composable);

    /// <summary>
    /// Writes a boolean to the current position in the packet.
    /// </summary>
    IPacket WriteBool(bool value);

    /// <summary>
    /// Writes a boolean to the specified position in the packet.
    /// </summary>
    IPacket WriteBool(bool value, int position);

    /// <summary>
    /// Writes a byte to the current position in the packet.
    /// </summary>
    IPacket WriteByte(byte value);

    /// <summary>
    /// Writes a byte to the specified position in the packet.
    /// </summary>
    IPacket WriteByte(byte value, int position);

    /// <summary>
    /// Writes a short to the current position in the packet.
    /// </summary>
    IPacket WriteShort(short value);

    /// <summary>
    /// Writes a short to the specified position in the packet.
    /// </summary>
    IPacket WriteShort(short value, int position);

    /// <summary>
    /// Writes an integer to the current position in the packet.
    /// </summary>
    IPacket WriteInt(int value);

    /// <summary>
    /// Writes an integer to the specified position in the packet.
    /// </summary>
    IPacket WriteInt(int value, int position);

    /// <summary>
    /// Writes a float to the current position in the packet.
    /// </summary>
    IPacket WriteFloat(float value);

    /// <summary>
    /// Writes a float to the specified position in the packet.
    /// </summary>
    IPacket WriteFloat(float value, int position);

    /// <summary>
    /// Writes a long to the current position in the packet.
    /// </summary>
    IPacket WriteLong(long value);

    /// <summary>
    /// Writes a long to the specified position in the packet.
    /// </summary>
    IPacket WriteLong(long value, int position);

    /// <summary>
    /// Writes a string to the current position in the packet.
    /// </summary>
    IPacket WriteString(string value);

    /// <summary>
    /// Writes a string to the specified position in the packet.
    /// </summary>
    IPacket WriteString(string value, int position);

    /// <summary>
    /// Writes a string representation of a float to the current position in the packet.
    /// </summary>
    IPacket WriteFloatAsString(float value);

    /// <summary>
    /// Writes a string representation of a float to the specified position in the packet.
    /// </summary>
    IPacket WriteFloatAsString(float value, int position);

    /// <summary>
    /// Writes the specified bytes to the current position in the packet.
    /// </summary>
    IPacket WriteBytes(ReadOnlySpan<byte> bytes);

    /// <summary>
    /// Writes the specified bytes to the specified position in the packet.
    /// </summary>
    IPacket WriteBytes(ReadOnlySpan<byte> bytes, int position);

    /// <summary>
    /// Writes a short or an int to the current position in the packet, depending on its protocol.
    /// If the protocol is Unity, writes a short.
    /// If the protocol is Flash, writes an int.
    /// Otherwise, throws an exception.
    /// </summary>
    IPacket WriteLegacyShort(short value);

    /// <summary>
    /// Writes a float or a string to the current position in the packet, depending on its protocol.
    /// If the protocol is Unity, writes a float as 4 bytes.
    /// If the protocol is Flash, writes a string representation of a float.
    /// Otherwise, throws an exception.
    /// </summary>
    IPacket WriteLegacyFloat(float value);

    /// <summary>
    /// Writes a long or an int to the current position in the packet, depending on its protocol.
    /// If the protocol is Unity, writes a long.
    /// If the protocol is Flash, writes an int.
    /// Otherwise, throws an exception.
    /// </summary>
    IPacket WriteLegacyLong(long value);

    /// <summary>
    /// Replaces a string at the current position in the packet.
    /// Adjusts the packet length and offsets any data after the string if the replacement causes the length to change.
    /// Throws if a string cannot be read at the current position in the packet.
    /// </summary>
    IPacket ReplaceString(string value);

    /// <summary>
    /// Replaces a string at the specified position in the packet.
    /// Adjusts the packet length and offsets any data after the string if the replacement causes the length to change.
    /// Throws if a string cannot be read at the specified position in the packet.
    /// </summary>
    IPacket ReplaceString(string value, int position);

    /// <summary>
    /// Replaces a string at the current position in the packet using a transform function.
    /// Adjusts the packet length and offsets any data after the string if the replacement causes the length to change.
    /// Throws if a string cannot be read at the current position in the packet.
    /// </summary>
    IPacket ModifyString(Func<string, string> modifier);

    /// <summary>
    /// Replaces the specified values in the packet.
    /// </summary>
    IPacket Replace(params object[] values);
}
