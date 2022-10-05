namespace Xabbo.Messages;

/// <summary>
/// Represents a <see cref="long"/> that is serialized to an <see cref="int"/> on Flash sessions for compatibility.
/// </summary>
public struct LegacyLong
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    public long Value { get; }

    /// <summary>
    /// Constructs a new <see cref="LegacyLong"/> with the specified value.
    /// </summary>
    public LegacyLong(long value)
    {
        Value = value;
    }

    public static implicit operator LegacyLong(long value) => new LegacyLong(value);
    public static implicit operator long(LegacyLong legacyLong) => legacyLong.Value;
}
