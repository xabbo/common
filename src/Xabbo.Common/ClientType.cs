using System;

namespace Xabbo;

/// <summary>
/// Represents a type of game client.
/// </summary>
[Flags]
public enum ClientType
{
    /// <summary>
    /// Represents no particular client.
    /// </summary>
    None,
    /// <summary>
    /// Represents the Unity client.
    /// </summary>
    Unity = 1 << 0,
    /// <summary>
    /// Represents the Flash client.
    /// </summary>
    Flash = 1 << 1,
    /// <summary>
    /// Represents the Shockwave client.
    /// </summary>
    Shockwave = 1 << 2,
    /// <summary>
    /// Represents all clients.
    /// </summary>
    All = Unity | Flash | Shockwave,
}
