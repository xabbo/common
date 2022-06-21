using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Common;
using Xabbo.Messages;
using Xabbo.Interceptor.Dispatcher;

namespace Xabbo.Interceptor;

/// <summary>
/// Represents a Habbo packet interceptor that can read, modify and send packets.
/// </summary>
public interface IInterceptor
{
    /// <summary>
    /// Gets the message manager associated with this interceptor.
    /// </summary>
    IMessageManager Messages { get; }

    /// <summary>
    /// Gets the dispatcher associated with this interceptor.
    /// </summary>
    IInterceptDispatcher Dispatcher { get; }

    /// <summary>
    /// Gets whether the game is currently connected or not.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets the client identifier for the current connection.
    /// </summary>
    string ClientIdentifier { get; }

    /// <summary>
    /// Gets the client type for the current connection.
    /// </summary>
    ClientType Client { get; }

    /// <summary>
    /// Invoked when a connection to the game is established.
    /// </summary>
    event EventHandler<GameConnectedEventArgs>? Connected;

    /// <summary>
    /// Invoked when the connection to the game ends.
    /// </summary>
    event EventHandler? Disconnected;

    /// <summary>
    /// Invoked when the extension has been initialized by the interceptor.
    /// </summary>
    event EventHandler<InterceptorInitializedEventArgs>? Initialized;

    /// <summary>
    /// Invoked when the extension is selected in the interceptor UI.
    /// </summary>
    event EventHandler? Clicked;

    /// <summary>
    /// Invoked when a packet has been intercepted.
    /// </summary>
    event EventHandler<InterceptArgs>? Intercepted;

    /// <summary>
    /// Gets a cancellation token that is triggered when the connection to the game ends.
    /// </summary>
    CancellationToken DisconnectToken { get; }

    /// <summary>
    /// Sends the specified packet to either the client or server,
    /// depending on the destination of the packet header.
    /// </summary>
    ValueTask SendAsync(IReadOnlyPacket packet);
}
