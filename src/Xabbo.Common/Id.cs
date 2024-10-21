using System;

namespace Xabbo;

/// <summary>
/// Represents a numeric identifier.
/// </summary>
/// <remarks>
/// This type is represented as the following:
/// <list type="bullet">
/// <item>On Unity as a <see cref="long"/>.</item>
/// <item>On Flash as an <see cref="int"/>.</item>
/// <item>On Shockwave as a <see cref="Messages.VL64"/>.</item>
/// </list>
/// </remarks>
public readonly record struct Id : IComparable<Id>, IComparable
{
    /// <summary>
    /// The minimum value of and <see cref="Id"/>.
    /// </summary>
    public const long MinValue = long.MinValue;

    /// <summary>
    /// The maximum value of an <see cref="Id"/>.
    /// </summary>
    public const long MaxValue = long.MaxValue;

    private readonly long _value;
    private Id(long value) => _value = value;

    public static implicit operator long(Id id) => id._value;
    public static implicit operator Id(long value) => new(value);

    public static explicit operator Id(string s)
    {
        if (!int.TryParse(s, out int value))
            throw new Exception($"Invalid ID: {s}");
        return new Id(value);
    }

    /// <summary>
    /// Parses a string representation of an Id to its value and
    /// returns whether the conversion was successful.
    /// </summary>
    /// <param name="s">The string to parse.</param>
    /// <param name="id">The parsed <see cref="Id"/>.</param>
    public static bool TryParse(string? s, out Id id)
    {
        if (!long.TryParse(s, out long value))
        {
            id = 0;
            return false;
        }
        else
        {
            id = value;
            return true;
        }
    }

    /// <summary>
    /// Returns the value of this <see cref="Id"/> as a string.
    /// </summary>
    public override string ToString() => _value.ToString();

    /// <inheritdoc/>
    public int CompareTo(Id other) => _value.CompareTo(other._value);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is not Id other)
            throw new ArgumentException($"Object must be of type {typeof(Id).FullName}.", nameof(obj));
        return CompareTo(other);
    }
}
