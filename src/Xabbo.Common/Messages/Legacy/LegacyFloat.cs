namespace Xabbo.Messages;

/// <summary>
/// Represents a <see cref="float"/> that is serialized as a <see cref="string"/> on Flash sessions for compatibility.
/// </summary>
public readonly struct LegacyFloat(float value)
{
    public float Value { get; } = value;

    public static implicit operator LegacyFloat(float value) => new(value);
    public static implicit operator float(LegacyFloat legacyFloat) => legacyFloat.Value;
}
