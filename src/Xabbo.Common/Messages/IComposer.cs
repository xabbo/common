namespace Xabbo.Messages;

/// <summary>
/// Represents an object that can be composed to a packet.
/// </summary>
public interface IComposer
{
    /// <summary>
    /// Composes the object to a packet using the specified <see cref="PacketWriter"/>.
    /// </summary>
    void Compose(in PacketWriter p);
}
