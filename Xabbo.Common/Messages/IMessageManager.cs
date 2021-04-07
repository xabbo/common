using System;
using System.Diagnostics.CodeAnalysis;

namespace Xabbo.Messages
{
    public interface IMessageManager
    {
        /// <summary>
        /// Loads the messages for the specified client type
        /// using the messages file path if it is needed.
        /// </summary>
        void Load(ClientType clientType, string? messagesPath);

        /// <summary>
        /// Gets the incoming headers.
        /// </summary>
        Incoming In { get; }

        /// <summary>
        /// Gets the outgoing headers.
        /// </summary>
        Outgoing Out { get; }

        /// <summary>
        /// Gets the header with the specified identifier.
        /// </summary>
        Header this[Identifier identifier] { get; }

        /// <summary>
        /// Gets if a header with the specified identifier exists.
        /// </summary>
        bool IdentifierExists(Identifier identifier);

        /// <summary>
        /// Attempts to get a header by its identifier.
        /// </summary>
        bool TryGetHeader(Identifier identifier, [MaybeNullWhen(false)] out Header header);

        /// <summary>
        /// Attempts to get a header by its destination and value.
        /// </summary>
        bool TryGetHeaderByValue(Destination destination, short value, [MaybeNullWhen(false)] out Header header);

        /// <summary>
        /// Attempts to get a header by its destination and name.
        /// </summary>
        bool TryGetHeaderByName(Destination destination, string name, [MaybeNullWhen(false)] out Header header);

        /// <summary>
        /// Attempts to get a header by its destination and hash.
        /// </summary>
        bool TryGetHeaderByHash(Destination destination, string hash, [MaybeNullWhen(false)] out Header header);
    }
}
