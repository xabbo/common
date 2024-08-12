using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Connection;

/// <summary>
/// Represents a connection to the game server.
/// </summary>
public interface IConnection
{
    /// <summary>
    /// Gets the message manager associated with this connection.
    /// </summary>
    IMessageManager Messages { get; }

    /// <summary>
    /// Gets if the connection is established.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets a cancellation token that is triggered when the connection is lost.
    /// </summary>
    CancellationToken DisconnectToken { get; }

    /// <summary>
    /// Gets the client for the current session.
    /// </summary>
    ClientInfo Client { get; }

    /// <summary>
    /// Gets the hotel for the current session.
    /// </summary>
    Hotel Hotel { get; }

    /// <summary>
    /// Sends a packet to the client or server, specified by the direction of the packet's header.
    /// </summary>
    void Send(IReadOnlyPacket packet);
}
