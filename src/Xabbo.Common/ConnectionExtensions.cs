using System;
using System.ComponentModel;

using Xabbo.Connection;
using Xabbo.Messages;

namespace Xabbo;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ConnectionExtensions
{
    /// <summary>
    /// Sends an empty packet with the specified message header.
    /// </summary>
    public static void Send(this IConnection connection, Header header)
    {
        using Packet packet = new(header);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends an empty packet with the specified message identifier.
    /// </summary>
    public static void Send(this IConnection connection, Identifier identifier)
    {
        using Packet packet = new(connection.Messages.Resolve(identifier));
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a message to the client or server, specified by the direction of the message identifier or header.
    /// </summary>
    public static void Send(this IConnection connection, IMessage message)
    {
        Header header;
        Identifier identifier = message.Identifier;

        if (identifier != Identifier.Unknown)
        {
            header = connection.Messages.Resolve(identifier);
        }
        else
        {
            header = message.Header;
            if (header == Header.Unknown)
                throw new Exception("Message identifier and header are both unknown.");
        }

        using Packet packet = new(header);
        packet.Writer().Compose(message);
        connection.Send(packet);
    }
}