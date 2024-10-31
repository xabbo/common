using System.Globalization;

namespace Xabbo;

/// <summary>
/// Represents a float that is parsed and composed as a string.
/// </summary>
public readonly struct FloatString(float value)
{
    public static string Format(float value) => value.ToString("0.0##############", CultureInfo.InvariantCulture);

    public float Value { get; } = value;

    public static implicit operator FloatString(float value) => new(value);
    public static implicit operator float(FloatString legacyFloat) => legacyFloat.Value;

    public static implicit operator string(FloatString floatString) => Format(floatString.Value);
    public static explicit operator FloatString(string value) => new(float.Parse(value, CultureInfo.InvariantCulture));

    public override string ToString() => Format(Value);
}
