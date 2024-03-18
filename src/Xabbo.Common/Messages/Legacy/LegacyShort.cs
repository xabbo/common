namespace Xabbo.Messages;

/// <summary>
/// Represents a <see cref="short"/> that is serialized as an <see cref="int"/> on Flash sessions for compatibility.
/// </summary>
public readonly struct LegacyShort(short value)
{
    public short Value { get; } = value;

    public static implicit operator LegacyShort(short value) => new(value);
    public static implicit operator short(LegacyShort legacyShort) => legacyShort.Value;
}
