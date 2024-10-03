using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents an unsigned fixed-length radix-64 encoded integer.
/// </summary>
public readonly record struct B64
{
    /// <summary>
    /// The minimum value of a <see cref="B64"/>.
    /// </summary>
    public const ushort MinValue = 0;

    /// <summary>
    /// The maximum value of a <see cref="B64"/>.
    /// </summary>
    public const ushort MaxValue = 0x0fff;

    private readonly ushort _value;
    private B64(ushort value) => _value = value;

    /// <summary>
    /// Encodes a B64 value to the specified span.
    /// </summary>
    /// <param name="buf">The span to encode into.</param>
    /// <param name="value">The value to encode.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If the value is outside the range of a B64,
    /// or the length of the provided span is less than 2.
    /// </exception>
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
    /// Decodes a B64 value from the specified read-only byte span.
    /// </summary>
    /// <param name="buf">The span to decode from.</param>
    /// <returns>The decoded B64 value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">If the length of the provided span is less than 2.</exception>
    /// <exception cref="ArgumentException">
    /// If any of the input bytes' 2 most significant bits are not set to <c>01</c>.
    /// </exception>
    public static B64 Decode(ReadOnlySpan<byte> buf)
    {
        if (buf.Length < 2)
            throw new ArgumentOutOfRangeException(nameof(buf), "Not enough bytes to decode B64.");
        if ((buf[0] & 0xc0) != 0x40)
            throw new ArgumentException($"Invalid byte encountered when decoding B64: 0x{buf[0]:x02}.");
        if ((buf[1] & 0xc0) != 0x40)
            throw new ArgumentException($"Invalid byte encountered when decoding B64: 0x{buf[1]:x02}.");
        return (B64)(((buf[0] & 0x3f) << 6) | (buf[1] & 0x3f));
    }

    public static implicit operator ushort(B64 b64) => b64._value;
    public static implicit operator B64(ushort value) => new((ushort)(value & 0x0fff));
    public static explicit operator B64(int value) => new((ushort)(value & 0x0fff));

    /// <summary>
    /// Returns the value of this <see cref="B64"/> as a string.
    /// </summary>
    public override string ToString() => _value.ToString();
}
