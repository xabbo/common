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
public readonly struct Length(int value)
{
    public int Value { get; } = value;

    public static implicit operator Length(int value) => new(value);
    public static implicit operator int(Length legacyShort) => legacyShort.Value;

    public override string ToString() => Value.ToString();
}
