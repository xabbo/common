using System;
using System.Numerics;

namespace Xabbo.Messages;

/// <summary>
/// Represents a signed variable-length radix-64 encoded integer.
/// </summary>
public readonly record struct VL64
{
    /// <summary>
    /// The minimum value of a <see cref="VL64"/>.
    /// </summary>
    public const int MinValue = -0x7fffffff;

    /// <summary>
    /// The maximum value of a <see cref="VL64"/>.
    /// </summary>
    public const int MaxValue = 0x7fffffff;

    private readonly int _value;
    private VL64(int value) => _value = value;

    /// <summary>
    /// Returns the number of bytes required to represent the specified VL64.
    /// </summary>
    public static int EncodeLength(VL64 value) => (32 - BitOperations.LeadingZeroCount((uint)Math.Abs(value)) + 9) / 6;

    /// <summary>
    /// Returns the number of bytes required to decode a VL64, given (and including) the first byte.
    /// </summary>
    public static int DecodeLength(byte value) => (value >> 3) & 7;

    /// <summary>
    /// Encodes a VL64 value to the specified byte span.
    /// </summary>
    /// <param name="buf">The span to encode into.</param>
    /// <param name="value">The value to encode.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If the value is outside the range of a VL64.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If the length of the provided span is insufficient to encode the specified VL64 value.
    /// </exception>
    public static void Encode(Span<byte> buf, VL64 value)
    {
        if (value < MinValue || value > MaxValue)
            throw new ArgumentOutOfRangeException(nameof(value), "Value is outside the range of a VL64.");

        int n = EncodeLength(value);
        if (buf.Length < n)
            throw new ArgumentException($"Not enough space in buffer to encode {value} as a VL64. (need {n} bytes, have {buf.Length})");

        int abs = Math.Abs(value);
        buf[0] = (byte)(0x40 | ((n & 7) << 3) | ((value >> 31) & 4) | (abs & 3));
        for (int i = 1; i < n; i++)
            buf[i] = (byte)(0x40 | ((abs >> (2 + 6 * (i - 1))) & 0x3f));
    }

    /// <summary>
    /// Decodes a VL64 value from the specified read-only byte span.
    /// </summary>
    /// <param name="buf">The span to decode from.</param>
    /// <returns>The decoded VL64 value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If the provided span does not have enough bytes to decode a VL64 based on the length decoded from the first byte.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If the length decoded from the first byte is outside the valid range of 1-6,
    /// or any of the input bytes' 2 most significant bits are not set to <c>01</c>.
    /// </exception>
    public static VL64 Decode(ReadOnlySpan<byte> buf)
    {
        if (buf.Length < 1)
            throw new ArgumentOutOfRangeException(nameof(buf), "Not enough bytes to decode VL64.");
        if ((buf[0] & 0xc0) != 0x40)
            throw new ArgumentException($"Invalid byte encountered when decoding VL64: 0x{buf[0]:x02}.");
        int n = DecodeLength(buf[0]);
        if (n is < 1 or > 6)
            throw new ArgumentException($"Invalid VL64 length: {n}.", nameof(buf));
        if (buf.Length < n)
            throw new ArgumentOutOfRangeException(nameof(buf), "Not enough bytes to decode VL64.");

        int value = buf[0] & 3;
        for (int i = 1; i < n; i++)
        {
            if ((buf[i] & 0xc0) != 0x40)
                throw new ArgumentException($"Invalid byte encountered when decoding VL64: 0x{buf[i]:x02}.");
            value |= (buf[i] & 0x3f) << (2 + 6 * (i - 1));
        }

        if ((buf[0] & 4) != 0)
            value *= -1;

        return value;
    }

    public static implicit operator int(VL64 vl64) => vl64._value;
    public static implicit operator VL64(int value) => new(value);

    /// <summary>
    /// Returns the value of this <see cref="VL64"/> as a string.
    /// </summary>
    public override string ToString() => _value.ToString();
}
