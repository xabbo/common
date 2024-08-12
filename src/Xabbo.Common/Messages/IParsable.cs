namespace Xabbo.Messages;

/// <summary>
/// Represents an object that can be deserialized from a packet.
/// </summary>
public interface IParsable<T> where T : IParsable<T>
{
    static abstract T Parse(IReadOnlyPacket packet, ref int pos);
}
