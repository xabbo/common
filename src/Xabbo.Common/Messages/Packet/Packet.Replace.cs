using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace Xabbo.Messages;

public partial class Packet
{
    private void ReplaceValue(object value)
    {
        ArgumentNullException.ThrowIfNull(value);

        switch (value)
        {
            case bool x: WriteBool(x); break;
            case byte x: WriteByte(x); break;
            case short x: WriteShort(x); break;
            case ushort x: WriteShort((short)x); break;
            case int x: WriteInt(x); break;
            case uint x: WriteInt((int)x); break;
            case float x: WriteLegacyFloat(x); break;
            case long x: WriteLegacyLong(x); break;
            case ulong x: WriteLegacyLong((long)x); break;
            case string x: ReplaceString(x); break;
            default: throw new ArgumentException($"Cannot replace value of type: {value.GetType().FullName}.");
        }
    }

    /// <inheritdoc cref="IPacket.ReplaceString(string)" />
    public Packet ReplaceString(string value)
    {
        int preStrLen = BinaryPrimitives.ReadInt16BigEndian(_buffer.Span[Position..]);
        if (Length < (Position + 2 + preStrLen))
            throw new InvalidOperationException($"Cannot replace string at position {Position}");

        int preLen = Length;
        int postStrLen = Encoding.UTF8.GetByteCount(value);

        int diff = postStrLen - preStrLen;
        if (diff > 0)
        {
            GrowToSize(Length + diff);
        }
        else if (diff < 0)
        {
            _buffer = _memoryOwner.Memory[..(Length + diff)];
        }
        else
        {
            Encoding.UTF8.GetBytes(value, _buffer.Span[(Position + 2)..]);
            Position += (2 + postStrLen);
            return this;
        }

        // offset bytes after string value
        _memoryOwner
            .Memory[(Position + 2 + preStrLen)..preLen]
            .CopyTo(_memoryOwner.Memory[(Position + 2 + postStrLen)..]);

        // write the new string length + bytes
        BinaryPrimitives.WriteInt16BigEndian(_buffer.Span[Position..], (short)postStrLen);
        Encoding.UTF8.GetBytes(value, _buffer.Span[(Position + 2)..]);

        Position += (2 + postStrLen);
        return this;
    }
    IPacket IPacket.ReplaceString(string value) => ReplaceString(value);

    /// <inheritdoc cref="IPacket.ReplaceString(string, int)" />
    public Packet ReplaceString(string value, int position)
    {
        Position = position;
        return ReplaceString(value);
    }
    IPacket IPacket.ReplaceString(string value, int position) => ReplaceString(value, position);

    /// <inheritdoc cref="IPacket.ModifyString(Func{string, string})" />
    public Packet ModifyString(Func<string, string> modifier)
    {
        int position = Position;
        string value = ReadString();
        ReplaceString(modifier(value), position);
        return this;
    }
    IPacket IPacket.ModifyString(Func<string, string> modifier) => ModifyString(modifier);

    /// <inheritdoc cref="IPacket.Replace(object[])" />
    public Packet Replace(params object[] values)
    {
        foreach (object value in values)
            ReplaceValue(value);

        return this;
    }
    IPacket IPacket.Replace(object[] values) => Replace(values);
}
