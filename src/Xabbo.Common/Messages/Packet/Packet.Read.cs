using System;
using System.Buffers.Binary;
using System.Globalization;
using System.IO;
using System.Text;

namespace Xabbo.Messages;

public partial class Packet
{
    /// <inheritdoc />
    public byte ReadByte()
    {
        byte value = Buffer[Position];
        Position++;
        return value;
    }

    /// <inheritdoc />
    public byte ReadByte(int position)
    {
        Position = position;
        return ReadByte();
    }

    /// <inheritdoc />
    public void Read(Span<byte> buffer)
    {
        Buffer[Position..(Position + buffer.Length)].CopyTo(buffer);
        Position += buffer.Length;
    }

    /// <inheritdoc />
    public void ReadBytes(Span<byte> buffer, int position)
    {
        Position = position;
        Read(buffer);
    }

    /// <inheritdoc />
    public bool ReadBool()
    {
        byte value = Buffer[Position];
        if (value != 0 && value != 1)
            throw new InvalidDataException($"Value {value} is outside the range of a boolean.");

        Position++;
        return value == 1;
    }

    /// <inheritdoc />
    public bool ReadBool(int position)
    {
        Position = position;
        return ReadBool();
    }

    /// <inheritdoc />
    public short ReadShort()
    {
        short value = BinaryPrimitives.ReadInt16BigEndian(Buffer[Position..]);
        Position += 2;
        return value;
    }

    /// <inheritdoc />
    public short ReadShort(int position)
    {
        Position = position;
        return ReadShort();
    }

    /// <inheritdoc />
    public int ReadInt()
    {
        int value = BinaryPrimitives.ReadInt32BigEndian(Buffer[Position..]);
        Position += 4;
        return value;
    }

    /// <inheritdoc />
    public int ReadInt(int position)
    {
        Position = position;
        return ReadInt();
    }

    /// <inheritdoc />
    public float ReadFloat()
    {
        float value = BinaryPrimitives.ReadSingleBigEndian(Buffer[Position..]);
        Position += 4;
        return value;
    }

    /// <inheritdoc />
    public float ReadFloat(int position)
    {
        Position = position;
        return ReadFloat();
    }

    /// <inheritdoc />
    public long ReadLong()
    {
        long value = BinaryPrimitives.ReadInt64BigEndian(Buffer[Position..]);
        Position += 8;
        return value;
    }

    /// <inheritdoc />
    public long ReadLong(int position)
    {
        Position = position;
        return ReadLong();
    }

    /// <inheritdoc />
    public string ReadString()
    {
        Span<byte> buffer = Buffer;
        int length = BinaryPrimitives.ReadUInt16BigEndian(buffer[Position..]);
        string value = Encoding.UTF8.GetString(buffer[(Position+2)..(Position+2+length)]);
        Position += (2 + length);
        return value;
    }

    /// <inheritdoc />
    public string ReadString(int position)
    {
        Position = position;
        return ReadString();
    }

    /// <inheritdoc />
    public float ReadFloatAsString()
    {
        return float.Parse(ReadString(), CultureInfo.InvariantCulture);
    }

    /// <inheritdoc />
    public float ReadFloatAsString(int position)
    {
        Position = position;
        return ReadFloatAsString();
    }

    /// <inheritdoc />
    public Span<byte> GetSpan(int length)
    {
        Grow(length);
        Span<byte> span = Buffer[Position..(Position + length)];
        Position += length;
        return span;
    }

    /// <inheritdoc cref="IReadOnlyPacket.ReadLegacyShort()" />
    public short ReadLegacyShort()
    {
        return Protocol switch
        {
            ClientType.Unity => ReadShort(),
            ClientType.Flash => (short)ReadInt(),
            _ => throw new InvalidOperationException("Cannot read legacy short, unknown protocol type.")
        };
    }

    /// <inheritdoc cref="IReadOnlyPacket.ReadLegacyFloat()" />
    public float ReadLegacyFloat()
    {
        return Protocol switch
        {
            ClientType.Unity => ReadFloat(),
            ClientType.Flash => ReadFloatAsString(),
            _ => throw new InvalidOperationException("Cannot read legacy float, unknown protocol type.")
        };
    }

    /// <inheritdoc cref="IReadOnlyPacket.ReadLegacyLong()" />
    public long ReadLegacyLong()
    {
        return Protocol switch
        {
            ClientType.Unity => ReadLong(),
            ClientType.Flash => ReadInt(),
            _ => throw new InvalidOperationException("Cannot read legacy long, unknown protocol type.")
        };
    }
}
