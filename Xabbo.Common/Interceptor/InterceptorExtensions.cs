using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Interceptor.Tasks;
using Xabbo.Messages;

namespace Xabbo.Interceptor;

[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class InterceptorExtensions
{
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
        => ReceiveAsync(interceptor, new HeaderSet() { header }, timeout, block, cancellationToken);

    /// <summary>
    /// Registers a callback that is invoked when a packet with a matching header is intercepted.
    /// </summary>
    /// <param name="interceptor">The interceptor.</param>
    /// <param name="headers">Specifies which headers to intercept.</param>
    /// <param name="callback">The callback to invoke when a message is intercepted.</param>
    public static void OnIntercept(this IInterceptor interceptor, HeaderSet headers, Action<InterceptArgs> callback)
        => interceptor.Dispatcher.AddIntercept(headers, callback, interceptor.Client);

    /// <summary>
    /// Binds the specified target object to the dispatcher.
    /// </summary>
    /// <returns>
    /// <c>true</c> if successfully bound, or <c>false</c> if the target
    /// does not have a receive or intercept attribute on any of its methods.
    /// Throws if any of the required message identifiers are unable to be resolved.
    /// </returns>
    public static bool Bind(this IInterceptor interceptor, IInterceptHandler handler)
        => interceptor.Dispatcher.Bind(handler, interceptor.Client);

    /// <summary>
    /// Releases the specified target object from the dispatcher.
    /// </summary>
    /// <returns>Whether the binding was released or not.</returns>
    public static bool Release(this IInterceptor interceptor, IInterceptHandler handler) => interceptor.Dispatcher.Release(handler);
}
