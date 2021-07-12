using System;

namespace Xabbo.Messages
{
    public interface IPacket : IReadOnlyPacket
    {
        /// <summary>
        /// Gets or sets the message header of the packet.
        /// </summary>
        new Header Header { get; set; }

        /// <summary>
        /// Writes the composable object to the packet.
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
        /// Writes a float as a string to the current position in the packet.
        /// </summary>
        IPacket WriteFloatAsString(float value);

        /// <summary>
        /// Writes a float as a string to the specified position in the packet.
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
        /// Writes the specified values to the current position in the packet.
        /// </summary>
        IPacket WriteValues(params object[] values);

        /// <summary>
        /// Replaces values from the current position in the packet.
        /// </summary>
        IPacket Replace(params object[] values);

        /// <summary>
        /// Reads a string from the current position in the packet
        /// and replaces it with the result provided by a transform function.
        /// </summary>
        IPacket Replace(Func<string, string> transform);

        /// <summary>
        /// Replaces values from the specified position in the packet.
        /// </summary>
        IPacket ReplaceAt(int position, params object[] values);

        /// <summary>
        /// Reads a string from the specified position in the packet
        /// and replaces it with the result provided by a transform function.
        /// </summary>
        IPacket ReplaceAt(int position, Func<string, string> transform);

        /// <summary>
        /// Writes a short if the packet's protocol is Unity,
        /// an int if it is Flash,
        /// or throws if unknown.
        /// </summary>
        IPacket WriteLegacyShort(short value);

        /// <summary>
        /// Writes a float if the packet's protocol is Unity,
        /// a float (as a string) if it is Flash,
        /// or throws if unknown.
        /// </summary>
        IPacket WriteLegacyFloat(float value);

        /// <summary>
        /// Writes a long if the packet's protocol is Unity,
        /// an int if it is Flash,
        /// or throws if unknown.
        /// </summary>
        IPacket WriteLegacyLong(long value);
    }
}
