using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents an unsigned fixed-length base64-encoded integer.
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
        return (B64)(((buf[0] & 0x3f) << 6) | (buf[1] & 0x3f));
    }

    public static implicit operator ushort(B64 b64) => b64._value;
    public static implicit operator B64(ushort value) => new((ushort)(value & 0x0fff));
    public static explicit operator B64(int value) => new((ushort)(value & 0x0fff));

    public override string ToString() => _value.ToString();
}
