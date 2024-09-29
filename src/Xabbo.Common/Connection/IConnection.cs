using System;
using System.Threading;

using Xabbo.Messages;

namespace Xabbo.Connection;

/// <summary>
/// Represents a connection to the game server.
/// </summary>
public interface IConnection : IParserContext
{
    /// <summary>
    /// Gets whether a connection to the game is currently established.
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
    /// Occurs when a connection to the game is established.
    /// </summary>
    event Action<ConnectedEventArgs>? Connected;

    /// <summary>
    /// Occurs when a connection to the game ends.
    /// </summary>
    event Action? Disconnected;

    /// <summary>
    /// Sends a packet to the client or server, specified by the direction of the packet's header.
    /// </summary>
    void Send(IPacket packet);
}
