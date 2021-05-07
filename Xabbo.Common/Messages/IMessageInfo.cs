using System;

namespace Xabbo.Messages
{
    public interface IMessageInfo
    {
        /// <summary>
        /// Gets the destination of the message.
        /// </summary>
        Destination Destination { get; }

        /// <summary>
        /// Gets the direction of the message.
        /// </summary>
        Direction Direction { get; }

        /// <summary>
        /// Gets if the message is an incoming message.
        /// </summary>
        bool IsIncoming { get; }

        /// <summary>
        /// Gets if the message is an outgoing message.
        /// </summary>
        bool IsOutgoing { get; }

        /// <summary>
        /// Gets the name of the message for the current client, or a fallback name if it is unable to be resolved.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The name of the message in the Unity client, or <c>null</c> if it does not exist.
        /// </summary>
        string? UnityName { get; }

        /// <summary>
        /// The name of the message in the Flash client, or <c>null</c> if it does not exist.
        /// </summary>
        string? FlashName { get; }

        /// <summary>
        /// The header value of the message for the current client, or <c>-1</c> if it is unresolved.
        /// </summary>
        short Header { get; }
    }
}
