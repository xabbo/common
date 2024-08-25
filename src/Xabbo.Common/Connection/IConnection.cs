using System;
using System.Threading;

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
    /// Gets the session information for the current connection.
    /// </summary>
    Session Session { get; }

    /// <summary>
    /// Invoked when a connection to the game is established.
    /// </summary>
    event Action<GameConnectedArgs>? Connected;

    /// <summary>
    /// Invoked when the connection to the game ends.
    /// </summary>
    event Action? Disconnected;

    /// <summary>
    /// Sends a packet to the client or server, specified by the direction of the packet's header.
    /// </summary>
    void Send(IPacket packet);
}
