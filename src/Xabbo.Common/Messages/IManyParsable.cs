using System.Collections.Generic;

namespace Xabbo.Messages;

/// <summary>
/// Represents a collection that can be deserialized from a packet.
/// </summary>
public interface IManyParsable<T> where T : IParsable<T>
{
    static abstract IEnumerable<T> Parse(IReadOnlyPacket packet);
}
