namespace Xabbo.Messages;

/// <summary>
/// Represents a float that is serialized as a string.
/// </summary>
public readonly struct FloatString(float value)
{
    public float Value { get; } = value;

    public static implicit operator FloatString(float value) => new(value);
    public static implicit operator float(FloatString legacyFloat) => legacyFloat.Value;
}
