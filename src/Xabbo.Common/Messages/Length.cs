namespace Xabbo.Messages;

/// <summary>
/// Represents a collection length with client-specific serialization:
/// <list type="bullet">
/// <item>On Unity: as a <see cref="short"/>.</item>
/// <item>Otherwise as an <see cref="int"/>.</item>
/// </list>
/// </summary>
public readonly struct Length(int value)
{
    public int Value { get; } = value;

    public static implicit operator Length(int value) => new(value);
    public static implicit operator int(Length legacyShort) => legacyShort.Value;
}
