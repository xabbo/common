using System;

namespace Xabbo;

/// <summary>
/// Represents an array length.
/// </summary>
/// <remarks>
/// This type is represented as the following:
/// <list type="bullet">
/// <item>On Unity as a <see cref="short"/>.</item>
/// <item>On Flash as an <see cref="int"/>.</item>
/// <item>On Shockwave as a <see cref="Messages.VL64"/>.</item>
/// </list>
/// </remarks>
public readonly record struct Length : IComparable<Length>, IComparable
{
    private readonly ushort _value;
    private Length(ushort value) => _value = value;

    public static implicit operator ushort(Length length) => length._value;
    public static implicit operator Length(ushort value) => new(value);
    public static explicit operator Length(int value) => new(checked((ushort)value));

    /// <summary>
    /// Returns the value of this <see cref="Length"/> as a string.
    /// </summary>
    public override string ToString() => _value.ToString();

    /// <inheritdoc/>
    public int CompareTo(Length other) => _value.CompareTo(other._value);

    /// <inheritdoc/>
    public int CompareTo(object? obj)
    {
        if (obj is not Length other)
            throw new ArgumentException($"Object must be of type {typeof(Length).FullName}.", nameof(obj));
        return CompareTo(other);
    }
}
