using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents the direction of a message.
/// </summary>
[Flags]
public enum Direction
{
    /// <summary>
    /// Represents an unknown direction.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// Represents the incoming (client-bound) direction.
    /// </summary>
    Incoming = 1,
    /// <summary>
    /// Represents the outgoing (server-bound) direction.
    /// </summary>
    Outgoing = 2,
    /// <summary>
    /// Represents both incoming and outgoing directions.
    /// </summary>
    Both = Incoming | Outgoing
}
