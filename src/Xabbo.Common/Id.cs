namespace Xabbo;

/// <summary>
/// Represents a numeric identifier with client-specific serialization:
/// <list type="bullet">
/// <item>On Unity: as a <see cref="long"/>.</item>
/// <item>On Flash/Shockwave: as an<see cref="int"/>.</item>
/// </list>
/// </summary>
public readonly struct Id(long value)
{
    public long Value { get; } = value;

    public static implicit operator Id(long value) => new(value);
    public static implicit operator long(Id id) => id.Value;

    public override string ToString() => Value.ToString();
}
