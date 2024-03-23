using System;
using System.Buffers.Binary;
using System.Globalization;
using System.Text;

namespace Xabbo.Messages;

public partial class Packet
{
    public Packet Write(IComposable composable)
    {
        composable.Compose(this);
        return this;
    }
    IPacket IPacket.Write(IComposable composable) => Write(composable);

    public Packet WriteByte(byte value)
    {
        Grow(1);
        _buffer.Span[Position++] = value;
        return this;
    }
    IPacket IPacket.WriteByte(byte value) => WriteByte(value);

    public Packet WriteByte(byte value, int position)
    {
        Position = position;
        return WriteByte(value);
    }
    IPacket IPacket.WriteByte(byte value, int position) => WriteByte(value, position);

    public Packet WriteBytes(ReadOnlySpan<byte> bytes)
    {
        Grow(bytes.Length);
        bytes.CopyTo(_buffer.Span[Position..]);
        Position += bytes.Length;
        return this;
    }
    IPacket IPacket.WriteBytes(ReadOnlySpan<byte> bytes) => WriteBytes(bytes);

    public Packet WriteBytes(ReadOnlySpan<byte> bytes, int position)
    {
        Position = position;
        return WriteBytes(bytes);
    }
    IPacket IPacket.WriteBytes(ReadOnlySpan<byte> bytes, int position) => WriteBytes(bytes, position);

    public Packet WriteBool(bool value) => WriteByte((byte)(value ? 1 : 0));
    IPacket IPacket.WriteBool(bool value) => WriteBool(value);

    public Packet WriteBool(bool value, int position)
    {
        Position = position;
        return WriteBool(value);
    }
    IPacket IPacket.WriteBool(bool value, int position) => WriteBool(value, position);

    public Packet WriteShort(short value)
    {
        Grow(2);
        BinaryPrimitives.WriteInt16BigEndian(_buffer.Span[Position..], value);
        Position += 2;
        return this;
    }
    IPacket IPacket.WriteShort(short value) => WriteShort(value);

    public Packet WriteShort(short value, int position)
    {
        Position = position;
        return WriteShort(value);
    }
    IPacket IPacket.WriteShort(short value, int position) => WriteShort(value, position);

    public Packet WriteInt(int value)
    {
        Grow(4);
        BinaryPrimitives.WriteInt32BigEndian(_buffer.Span[Position..], value);
        Position += 4;
        return this;
    }
    IPacket IPacket.WriteInt(int value) => WriteInt(value);

    public Packet WriteInt(int value, int position)
    {
        Position = position;
        return WriteInt(value);
    }
    IPacket IPacket.WriteInt(int value, int position) => WriteInt(value, position);

    public Packet WriteLong(long value)
    {
        if (Protocol == ClientType.Flash)
            throw new InvalidOperationException("Cannot write long when the packet protocol is Flash.");

        Grow(8);
        BinaryPrimitives.WriteInt64BigEndian(_buffer.Span[Position..], value);
        Position += 8;
        return this;
    }
    IPacket IPacket.WriteLong(long value) => WriteLong(value);

    public Packet WriteLong(long value, int position)
    {
        Position = position;
        return WriteLong(value);
    }
    IPacket IPacket.WriteLong(long value, int position) => WriteLong(value, position);

    public Packet WriteFloat(float value)
    {
        Grow(4);
        BinaryPrimitives.WriteSingleBigEndian(_buffer.Span[Position..], value);
        Position += 4;
        return this;
    }
    IPacket IPacket.WriteFloat(float value) => WriteFloat(value);

    /// <inheritdoc cref="IPacket.WriteFloat(float, int)" />
    public Packet WriteFloat(float value, int position)
    {
        Position = position;
        return WriteFloat(value);
    }
    IPacket IPacket.WriteFloat(float value, int position) => WriteFloat(value, position);

    /// <inheritdoc cref="IPacket.WriteFloatAsString(float)" />
    public Packet WriteFloatAsString(float value)
    {
        return WriteString(value.ToString("0.0##############", CultureInfo.InvariantCulture));
    }
    IPacket IPacket.WriteFloatAsString(float value) => WriteFloatAsString(value);

    /// <inheritdoc cref="IPacket.WriteFloatAsString(float, int)" />
    public Packet WriteFloatAsString(float value, int position)
    {
        Position = position;
        return WriteFloatAsString(value);
    }
    IPacket IPacket.WriteFloatAsString(float value, int position) => WriteFloatAsString(value, position);

    /// <inheritdoc cref="IPacket.WriteString(string)" />
    public Packet WriteString(string value)
    {
        int len = Encoding.UTF8.GetByteCount(value);
        WriteShort((short)len);

        Grow(len);
        Encoding.UTF8.GetBytes(value, _buffer.Span[Position..]);
        Position += len;
        return this;
    }
    IPacket IPacket.WriteString(string value) => WriteString(value);

    /// <inheritdoc cref="IPacket.WriteString(string, int)" />
    public Packet WriteString(string value, int position)
    {
        Position = position;
        return WriteString(value);
    }
    IPacket IPacket.WriteString(string value, int position) => WriteString(value, position);

    /// <inheritdoc cref="IPacket.WriteLegacyShort(short)" />
    public Packet WriteLegacyShort(short value)
    {
        return Protocol switch
        {
            ClientType.Unity => WriteShort(value),
            ClientType.Flash => WriteInt(value),
            _ => throw new InvalidOperationException("Cannot write legacy short, unknown protocol type.")
        };
    }
    IPacket IPacket.WriteLegacyShort(short value) => WriteLegacyShort(value);

    /// <inheritdoc cref="IPacket.WriteLegacyFloat(float)" />
    public Packet WriteLegacyFloat(float value)
    {
        return Protocol switch
        {
            ClientType.Unity => WriteFloat(value),
            ClientType.Flash => WriteFloatAsString(value),
            _ => throw new InvalidOperationException("Cannot write legacy float, unknown protocol type.")
        };
    }
    IPacket IPacket.WriteLegacyFloat(float value) => WriteLegacyFloat(value);

    /// <inheritdoc cref="IPacket.WriteLegacyLong(long)" />
    public Packet WriteLegacyLong(long value)
    {
        return Protocol switch
        {
            ClientType.Unity => WriteLong(value),
            ClientType.Flash => WriteInt((int)value),
            _ => throw new InvalidOperationException("Cannot write legacy long, unknown protocol type.")
        };
    }
    IPacket IPacket.WriteLegacyLong(long value) => WriteLegacyLong(value);
}
