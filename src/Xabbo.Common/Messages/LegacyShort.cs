namespace Xabbo.Messages;

/// <summary>
/// Represents a <see cref="short"/> that is serialized to an <see cref="int"/> on Flash sessions for compatibility.
/// </summary>
public struct LegacyShort
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    public short Value { get; }

    /// <summary>
    /// Constructs a new <see cref="LegacyShort"/> with the specified value.
    /// </summary>
    public LegacyShort(short value)
    {
        Value = value;
    }

    public static implicit operator LegacyShort(short value) => new LegacyShort(value);
    public static implicit operator short(LegacyShort legacyShort) => legacyShort.Value;
}
