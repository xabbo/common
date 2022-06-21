using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Common;

namespace Xabbo.Messages;

public interface IMessageManager
{
    /// <summary>
    /// Initializes the message manager.
    /// </summary>
    Task InitializeAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Loads the specified client message information.
    /// </summary>
    void LoadMessages(IEnumerable<IClientMessageInfo> messages);

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
    bool TryGetHeaderByValue(Destination destination, ClientType clientType, short value, [NotNullWhen(true)] out Header? header);

    /// <summary>
    /// Attempts to get a header by its destination and name.
    /// </summary>
    bool TryGetHeaderByName(Destination destination, string name, [NotNullWhen(true)] out Header? header);

    /// <summary>
    /// Attempts to get a message's info by its direction and header.
    /// </summary>
    bool TryGetInfoByHeader(Direction direction, ClientType clientType, short header, [NotNullWhen(true)] out MessageInfo? info);

    /// <summary>
    /// Attempts to get a message's info by its direction and name.
    /// </summary>
    bool TryGetInfoByName(Direction direction, string name, [NotNullWhen(true)] out MessageInfo? info);
}
