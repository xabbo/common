using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents the direction of a message.
/// </summary>
[Flags]
public enum Direction
{
    Unknown = 0,
    Incoming = 1,
    Outgoing = 2,
    Both = Incoming | Outgoing
}
