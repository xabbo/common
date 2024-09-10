using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Represents a packet's content as a string.
/// <para />
/// Only supported on Shockwave.
/// </summary>
public readonly record struct PacketContent(string Value) : IParserComposer<PacketContent>
{
    public static PacketContent Parse(in PacketReader p) => p.ReadContent();
    public void Compose(in PacketWriter p) => p.WriteContent(Value);

    public static implicit operator PacketContent(string value) => new(value);
    public static implicit operator string(PacketContent content) => content.Value;
}
