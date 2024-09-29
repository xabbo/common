using System;
using System.ComponentModel;

using Xabbo.Connection;
using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Provides extensions methods for <see cref="IConnection"/>.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Advanced)]
public static class ConnectionExtensions
{
    /// <summary>
    /// Sends an empty packet with the specified message header.
    /// </summary>
    public static void Send(this IConnection connection, Header header)
    {
        using Packet packet = new(header, connection.Session.Client.Type);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends an empty packet with the specified message identifier.
    /// </summary>
    public static void Send(this IConnection connection, Identifier identifier)
    {
        using Packet packet = new(
            connection.Messages.Resolve(identifier),
            connection.Session.Client.Type
        );
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a message to the client or server, specified by the direction of the message.
    /// </summary>
    public static void Send(this IConnection connection, IMessage message)
    {
        UnsupportedClientException.ThrowIf(connection.Session.Client.Type, ~message.GetSupportedClients());

        Identifier identifier = message.GetIdentifier(connection.Session.Client.Type);
        if (identifier == Identifier.Unknown)
            throw new Exception($"No identifier for IMessage({message.GetType().Name}).");

        using Packet packet = new(
            connection.Messages.Resolve(identifier),
            connection.Session.Client.Type
        );
        packet.Writer().Compose<IComposer>(message);
        connection.Send(packet);
    }
}
