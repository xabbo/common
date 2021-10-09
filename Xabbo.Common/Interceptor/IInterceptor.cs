using System;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using Xabbo.Messages;
using Xabbo.Interceptor.Dispatcher;
using Xabbo.Interceptor.Tasks;

namespace Xabbo.Interceptor
{
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
        /// Sends a packet with the specified header and values
        /// to either the client or server, depending on the destination of the header.
        /// </summary>
        void Send(Header header, params object[] values);

        /// <summary>
        /// Sends the specified packet to either the client or server,
        /// depending on the destination of the packet header.
        /// </summary>
        void Send(IReadOnlyPacket packet);

        /// <summary>
        /// Sends a packet with the specified header and values
        /// to either the client or server, depending on the destination of the header.
        /// </summary>
        Task SendAsync(Header header, params object[] values);

        /// <summary>
        /// Sends the specified packet to either the client or server,
        /// depending on the destination of the packet header.
        /// </summary>
        Task SendAsync(IReadOnlyPacket packet);

        /// <summary>
        /// Asynchronously receives a packet with any of the specified headers.
        /// </summary>
        public Task<IPacket> ReceiveAsync(HeaderSet headers, int timeout = -1, bool block = false, CancellationToken cancellationToken = default)
            => new CaptureMessageTask(this, headers, block).ExecuteAsync(timeout, cancellationToken);

        /// <summary>
        /// Asynchronously captures a packet with any of the specified headers.
        /// </summary>
        Task<IPacket> ReceiveAsync(ITuple headers, int timeout = -1, bool block = false, CancellationToken cancellationToken = default)
            => ReceiveAsync(HeaderSet.FromTuple(headers), timeout, block, cancellationToken);

        /// <summary>
        /// Asynchronously receives a packet with the specified header.
        /// </summary>
        public Task<IPacket> ReceiveAsync(Header header, int timeout = -1, bool block = false, CancellationToken cancellationToken = default)
            => ReceiveAsync(new HeaderSet() { header }, timeout, block, cancellationToken);

        /// <summary>
        /// Binds the specified target object to the dispatcher.
        /// </summary>
        /// <returns>
        /// <c>true</c> if successfully bound, or <c>false</c> if the target
        /// does not have a receive or intercept attribute on any of its methods.
        /// Throws if any of the message identifiers are unable to be resolved.
        /// </returns>
        bool Bind(object target) => Dispatcher.Bind(target, Client);

        /// <summary>
        /// Releases the specified target object from the dispatcher.
        /// </summary>
        /// <returns>Whether the binding was released or not.</returns>
        bool Release(object target) => Dispatcher.Release(target);
    }
}
