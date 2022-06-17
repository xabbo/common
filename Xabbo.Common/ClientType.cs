using System;

namespace Xabbo.Common
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
        Unity = 1 << 1,
        /// <summary>
        /// Represents all client types.
        /// </summary>
        All = Flash | Unity
    }
}
