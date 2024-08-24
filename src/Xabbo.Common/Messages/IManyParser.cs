using System.Collections.Generic;

namespace Xabbo.Messages;

/// <summary>
/// Represents a collection that can be deserialized from a packet.
/// </summary>
public interface IManyParser<T> where T : IParser<T>
{
    static abstract IEnumerable<T> ParseAll(in PacketReader p);
}
