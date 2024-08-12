namespace Xabbo.Messages;

/// <summary>
/// Represents an object that can be serialized to a packet.
/// </summary>
public interface IComposable
{
    void Compose(IPacket packet, ref int pos);
}