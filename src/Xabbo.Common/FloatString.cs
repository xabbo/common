using System.Globalization;

namespace Xabbo;

/// <summary>
/// Represents a float that is parsed/composed as a string.
/// </summary>
public readonly struct FloatAsString(float value)
{
    public static string Format(float value) => value.ToString("0.0##############", CultureInfo.InvariantCulture);

    public float Value { get; } = value;

    public static implicit operator FloatAsString(float value) => new(value);
    public static implicit operator float(FloatAsString legacyFloat) => legacyFloat.Value;

    public static implicit operator string(FloatAsString floatString) => Format(floatString.Value);
    public static explicit operator FloatAsString(string value) => new(float.Parse(value));
}
