using System;

namespace Xabbo
{
    /// <summary>
    /// Represents a type of game client.
    /// </summary>
    [Flags]
    public enum ClientType
    {
        /// <summary>
        /// Represents an unknown client type.
        /// </summary>
        Unknown,
        /// <summary>
        /// Represents the Flash client.
        /// </summary>
        Flash = 1 << 0,
        /// <summary>
        /// Represents the Unity client.
        /// </summary>
        Unity = 1 << 2
    }
}
