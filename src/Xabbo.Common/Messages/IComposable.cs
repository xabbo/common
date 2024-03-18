namespace Xabbo.Messages;

/// <summary>
/// Represents a composable object that can be serialized to a packet.
/// </summary>
public interface IComposable
{
    /// <summary>
    /// Writes this object to the specified packet.
    /// </summary>
    /// <param name="packet">The packet to write to.</param>
    void Compose(IPacket packet);
}
