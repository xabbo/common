using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Represents a packet's content as a string.
/// </summary>
/// <remarks>
/// Only supported on Shockwave.
/// This can only be read or written when the position is at the start of the packet,
/// after which the position will be set to the end of the packet.
/// </remarks>
public readonly record struct PacketContent(string Value) : IParserComposer<PacketContent>
{
    static PacketContent IParser<PacketContent>.Parse(in PacketReader p) => p.ReadContent();
    void IComposer.Compose(in PacketWriter p) => p.WriteContent(Value);

    public static implicit operator PacketContent(string value) => new(value);
    public static implicit operator string(PacketContent content) => content.Value;
}
