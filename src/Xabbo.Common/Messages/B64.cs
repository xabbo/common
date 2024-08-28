﻿using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents a base64-encoded integer.
/// </summary>
public readonly record struct B64(short Value)
{
    public static readonly B64 MinValue = new(0);
    public static readonly B64 MaxValue = new(0x0fff);

    public static void Encode(Span<byte> buf, B64 value)
    {
        if (value < MinValue || value > MaxValue)
            throw new ArgumentOutOfRangeException(nameof(value), "Value is outside the range of a B64.");

        if (buf.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(buf), "Not enough bytes to encode B64.");

        buf[0] = (byte)(0x40 | ((value >> 6) & 0x3f));
        buf[1] = (byte)(0x40 | (value & 0x3f));
    }

    /// <summary>
    /// Decodes a B64 value from the specified buffer.
    /// </summary>
    public static B64 Decode(ReadOnlySpan<byte> buf)
    {
        if (buf.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(buf), "Not enough bytes to decode B64.");
        return (short)(((buf[0] & 0x3f) << 6) | (buf[1] & 0x3f));
    }

    public static implicit operator B64(short value) => new((short)(value & 0x0fff));
    public static implicit operator short(B64 b64) => b64.Value;
}
