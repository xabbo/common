using System;
using System.Numerics;

namespace Xabbo.Messages;

/// <summary>
/// Represents a variable length base64-encoded integer.
/// </summary>
public readonly record struct VL64(int Value)
{
    /// <summary>
    /// Returns the number of bytes required to represent the specified VL64.
    /// </summary>
    public static int EncodeLength(VL64 value) => (32 - BitOperations.LeadingZeroCount((uint)Math.Abs(value)) + 9) / 6;

    /// <summary>
    /// Returns the number of bytes required to decode a variable-length base64-encoded integer, given (and including) the first byte.
    /// </summary>
    public static int DecodeLength(byte value) => (value >> 3) & 7;

    public static void Encode(Span<byte> buf, VL64 value)
    {
        int n = EncodeLength(value);
        if (buf.Length < n)
            throw new Exception($"Not enough space in buffer to encode {value} as a VL64. (need {n} bytes, have {buf.Length})");

        int abs = Math.Abs(value);
        buf[0] = (byte)(0x40 | ((n & 7) << 3) | ((value >> 31) & 4) | (abs & 3));
        for (int i = 1; i < n; i++)
            buf[i] = (byte)(0x40 | ((abs >> (2 + 6 * (i - 1))) & 0x3f));
    }

    public static VL64 Decode(ReadOnlySpan<byte> buf)
    {
        if (buf.Length < 1)
            throw new ArgumentOutOfRangeException(nameof(buf), "Not enough bytes to decode VL64.");
        int n = DecodeLength(buf[0]);
        if (n <= 0 || n > 6)
            throw new ArgumentException($"Invalid VL64 length: {n}.", nameof(buf));
        if (buf.Length < n)
            throw new ArgumentOutOfRangeException(nameof(buf), "Not enough bytes to decode VL64.");

        int value = buf[0] & 3;
        for (int i = 1; i < n; i++)
            value |= (buf[i] & 0x3f) << (2 + 6 * (i - 1));

        if ((buf[0] & 4) != 0)
            value *= -1;

        return value;
    }

    public static implicit operator VL64(int value) => new(value);
    public static implicit operator int(VL64 vl64) => vl64.Value;
}
