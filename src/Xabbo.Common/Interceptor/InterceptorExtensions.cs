using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Interceptor;
using Xabbo.Interceptor.Tasks;

using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Provides various extension methods for interceptors.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class InterceptorExtensions
{
    /// <summary>
    /// Sends a packet with the specified header to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync(this IInterceptor interceptor, Header header)
    {
        using Packet p = new(header, interceptor.Client);
        await interceptor.SendAsync(p);
    }

    /// <summary>
    /// Sends a packet with the header specified by an identifier to either the client or server, depending on the destination.
    /// If a direction is not specified, it is assumed to be outgoing.
    /// </summary>
    public static ValueTask SendAsync(this IInterceptor interceptor, Identifier identifier)
    {
        if (identifier.Direction == Direction.Unknown)
            identifier = identifier.WithDirection(Direction.Outgoing);

        return SendAsync(interceptor, interceptor.Messages[identifier]);
    }

    /// <summary>
    /// Sends a packet with the specified header to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send(this IInterceptor interceptor, Header header)
    {
        using Packet p = new(header, interceptor.Client);
        interceptor.Send(p);
    }

    /// <summary>
    /// Sends a packet with the header specified by an identifier to either the client or server, depending on the destination.
    /// </summary>
    public static void Send(this IInterceptor interceptor, Identifier identifier)
        => Send(interceptor, interceptor.Messages[identifier]);

    /// <summary>
    /// Asynchronously receives a packet with any of the specified headers.
    /// </summary>
    public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor, HeaderSet headers, int timeout = -1, bool block = false, CancellationToken cancellationToken = default)
        => new CaptureMessageTask(interceptor, headers, block).ExecuteAsync(timeout, cancellationToken);

    /// <summary>
    /// Asynchronously captures a packet with any of the specified headers.
    /// </summary>
    public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor, ITuple headers, int timeout = -1, bool block = false, CancellationToken cancellationToken = default)
        => ReceiveAsync(interceptor, HeaderSet.FromTuple(headers), timeout, block, cancellationToken);

    /// <summary>
    /// Asynchronously receives a packet with the specified header.
    /// </summary>
    public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor, Header header, int timeout = -1, bool block = false, CancellationToken cancellationToken = default)
        => ReceiveAsync(interceptor, [header], timeout, block, cancellationToken);

    /// <summary>
    /// Registers a callback that is invoked when a packet with a matching header is intercepted.
    /// </summary>
    /// <param name="interceptor">The interceptor.</param>
    /// <param name="headers">Specifies which headers to intercept.</param>
    /// <param name="handler">The handler to invoke when a message is intercepted.</param>
    public static void OnIntercept(this IInterceptor interceptor, HeaderSet headers, Action<InterceptArgs> handler)
        => interceptor.Dispatcher.AddIntercept(headers, handler, interceptor.Client);

    /// <summary>
    /// Binds the specified target object to the dispatcher.
    /// </summary>
    /// <returns>
    /// <c>true</c> if successfully bound, or <c>false</c> if the target
    /// does not have a receive or intercept attribute on any of its methods.
    /// Throws if any of the required message identifiers are unable to be resolved.
    /// </returns>
    public static bool Bind(this IInterceptor interceptor, IMessageHandler handler)
        => interceptor.Dispatcher.Bind(handler, interceptor.Client);

    /// <summary>
    /// Releases the specified target object from the dispatcher.
    /// </summary>
    /// <returns>Whether the binding was released or not.</returns>
    public static bool Release(this IInterceptor interceptor, IMessageHandler handler) => interceptor.Dispatcher.Release(handler);
}
