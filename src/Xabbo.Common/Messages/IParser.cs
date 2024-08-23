namespace Xabbo.Messages;

/// <summary>
/// Represents an object that can be deserialized from a packet.
/// </summary>
public interface IParser<T> where T : IParser<T>
{
    static abstract T Parse(in PacketReader p);
}
