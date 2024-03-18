namespace Xabbo.Messages;

/// <summary>
/// Represents a <see cref="long"/> that is serialized as an <see cref="int"/> on Flash sessions for compatibility.
/// </summary>
public readonly struct LegacyLong(long value)
{
    public long Value { get; } = value;

    public static implicit operator LegacyLong(long value) => new(value);
    public static implicit operator long(LegacyLong legacyLong) => legacyLong.Value;
}
