using System;
using System.Threading.Tasks;

using Xabbo.Messages;
using Xabbo.Interceptor.Dispatcher;

namespace Xabbo.Interceptor
{
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
        ClientType ClientType { get; }

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
        /// Invoked when a packet has been intercepted by the remote interceptor.
        /// </summary>
        event EventHandler<InterceptArgs>? Intercepted;

        /// <summary>
        /// Sends a packet with the specified header and values to the server.
        /// </summary>
        Task SendToServerAsync(Header header, params object[] values);

        /// <summary>
        /// Sends the specified packet to the server.
        /// </summary>
        Task SendToServerAsync(IReadOnlyPacket packet);

        /// <summary>
        /// Sends a packet with the specified header and values to the client.
        /// </summary>
        Task SendToClientAsync(Header header, params object[] values);

        /// <summary>
        /// Sends the specified packet to the client.
        /// </summary>
        Task SendToClientAsync(IReadOnlyPacket packet);
    }
}
