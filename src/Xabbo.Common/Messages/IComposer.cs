namespace Xabbo.Messages;

/// <summary>
/// Represents an object that can be composed to a packet.
/// </summary>
public interface IComposer
{
    void Compose(in PacketWriter p);
}