using System;
using System.Collections.Generic;

namespace Xabbo.Messages
{
    public interface IMessageInfo
    {
        /// <summary>
        /// The destination of the message.
        /// </summary>
        Destination Destination { get; }

        /// <summary>
        /// Gets if the message is an incoming message.
        /// </summary>
        bool IsIncoming { get; }

        /// <summary>
        /// Gets if the message is an outgoing message.
        /// </summary>
        bool IsOutgoing { get; }

        /// <summary>
        /// The name of the message as it appears in the Unity client,
        /// or a relevant name if it does not exist in the Unity client.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The hash of the message class in the Flash client.
        /// </summary>
        string Hash { get; }

        /// <summary>
        /// The header value of the message as it exists in the Unity client, or <c>-1</c> if it does not exist.
        /// </summary>
        short UnityHeader { get; }

        /// <summary>
        /// The header value of the message for the current client, or <c>-1</c> if it is unresolved.
        /// </summary>
        short Header { get; }

        /// <summary>
        /// The aliases of the message name.
        /// </summary>
        IReadOnlyCollection<string> Aliases { get; }

        /// <summary>
        /// The packet structure of the message.
        /// </summary>
        string Structure { get; }
    }
}
