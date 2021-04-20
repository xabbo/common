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
        /// Replaces a string at the current position in the packet.
        /// </summary>
        IPacket ReplaceString(string newValue);

        /// <summary>
        /// Replaces a string at the specified position in the packet.
        /// </summary>
        IPacket ReplaceString(string newValue, int position);

        /// <summary>
        /// Reads a string from the current position in the packet
        /// and replaces it with the result provided by a transform function.
        /// </summary>
        IPacket ReplaceString(Func<string, string> transform);

        /// <summary>
        /// Reads a string from the specified position in the packet
        /// and replaces it with the result provided by a transform function.
        /// </summary>
        IPacket ReplaceString(Func<string, string> transform, int position);

        /// <summary>
        /// Replaces the specified values at the current position in the packet.
        /// </summary>
        IPacket ReplaceValues(params object[] newValues);

        /// <summary>
        /// Replaces the specified values at the specified position in the packet.
        /// </summary>
        IPacket ReplaceValues(object[] newValues, int position);

        /// <summary>
        /// Creates a copy of the packet.
        /// </summary>
        IPacket Clone();
    }
}
