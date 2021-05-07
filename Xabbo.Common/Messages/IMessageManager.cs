using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Xabbo.Messages
{
    public interface IMessageManager
    {
        /// <summary>
        /// Loads the messages for the specified client type
        /// using the message API file path if it is needed.
        /// </summary>
        void LoadHarble(ClientType clientType, string? apiFilePath);

        /// <summary>
        /// Loads the messages for the specified client type.
        /// </summary>
        void LoadMessages(ClientType clientType, IEnumerable<MessageInfo> messages);

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
        bool TryGetHeader(Identifier identifier, [NotNullWhen(true)] out Header? header);

        /// <summary>
        /// Attempts to get a header by its destination and value.
        /// </summary>
        bool TryGetHeaderByValue(Destination destination, short value, [NotNullWhen(true)] out Header? header);

        /// <summary>
        /// Attempts to get a header by its destination and name.
        /// </summary>
        bool TryGetHeaderByName(Destination destination, string name, [NotNullWhen(true)] out Header? header);

        /// <summary>
        /// Attempts to get a message's info by its direction and header.
        /// </summary>
        bool TryGetInfoByHeader(Direction direction, short header, [NotNullWhen(true)] out MessageInfo? info);

        /// <summary>
        /// Attempts to get a message's info by its direction and name.
        /// </summary>
        bool TryGetInfoByName(Direction direction, string name, [NotNullWhen(true)] out MessageInfo? info);
    }
}
