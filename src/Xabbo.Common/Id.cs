using System;

namespace Xabbo;

/// <summary>
/// Represents a numeric identifier with client-specific serialization:
/// <list type="bullet">
/// <item>On Unity: as a <see cref="long"/>.</item>
/// <item>On Flash/Shockwave: as an <see cref="int"/>.</item>
/// </list>
/// </summary>
public readonly struct Id(long value)
{
    public long Value { get; } = value;

    public static implicit operator Id(long value) => new(value);
    public static implicit operator long(Id id) => id.Value;

    public static explicit operator Id(string s)
    {
        if (!int.TryParse(s, out int value))
            throw new Exception($"Invalid ID: {s}");
        return new Id(value);
    }

    public override string ToString() => Value.ToString();
}
