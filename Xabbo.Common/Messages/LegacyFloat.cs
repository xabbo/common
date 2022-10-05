namespace Xabbo.Messages;

/// <summary>
/// Represents a <see cref="float"/> that is serialized to a <see cref="string"/> on Flash sessions for compatibility.
/// </summary>
public struct LegacyFloat
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    public float Value { get; }

    /// <summary>
    /// Constructs a new <see cref="LegacyFloat"/> with the specified value.
    /// </summary>
    public LegacyFloat(float value)
    {
        Value = value;
    }

    public static implicit operator LegacyFloat(float value) => new LegacyFloat(value);
    public static implicit operator float(LegacyFloat legacyFloat) => legacyFloat.Value;
}
