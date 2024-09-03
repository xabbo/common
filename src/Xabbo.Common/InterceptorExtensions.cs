using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Interceptor;
using Xabbo.Interceptor.Tasks;
using Xabbo.Messages;

namespace Xabbo;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class InterceptorExtensions
{
    public static IDisposable Intercept(this IInterceptor interceptor, ReadOnlySpan<Header> headers, InterceptCallback callback)
        => interceptor.Dispatcher.Register(new([new(headers, callback)]) { Persistent = true });

    public static IDisposable Intercept(this IInterceptor interceptor, ReadOnlySpan<Identifier> identifiers, InterceptCallback callback)
        => interceptor.Dispatcher.Register(new([new(identifiers, callback)]) { Persistent = true });

    /// <summary>
    /// Asynchronously captures the first intercepted packet matching any of the specified headers.
    /// </summary>
    /// <param name="interceptor">The interceptor.</param>
    /// <param name="headers">Specifies which headers to listen for.</param>
    /// <param name="timeout">The maximum time in milliseconds to wait for a packet to be captured. <c>-1</c> specifies no timeout.</param>
    /// <param name="block">Whether the captured packet should be blocked from its destination.</param>
    /// <param name="shouldCapture">A callback that inspects intercepted packets and return whether the packet should be captured or not.</param>
    /// <param name="cancellationToken">The token used to cancel this operation.</param>
    /// <returns>A task that completes once a packet has been captured, or the operation times out.</returns>
    public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor, ReadOnlySpan<Header> headers,
        int timeout = -1, bool block = false, Func<IPacket, bool>? shouldCapture = null,
        CancellationToken cancellationToken = default)
    {
        return new CaptureMessageTask(interceptor, headers, block, shouldCapture).ExecuteAsync(timeout, cancellationToken);
    }

    /// <summary>
    /// Asynchronously captures the first intercepted packet matching any of the specified headers.
    /// </summary>
    /// <param name="interceptor">The interceptor.</param>
    /// <param name="identifiers">Specifies which messages to listen for.</param>
    /// <param name="timeout">The maximum time in milliseconds to wait for a packet to be captured. <c>-1</c> specifies no timeout.</param>
    /// <param name="block">Whether the captured packet should be blocked from its destination.</param>
    /// <param name="shouldCapture">A callback that inspects intercepted packets and return whether the packet should be captured or not.</param>
    /// <param name="cancellationToken">The token used to cancel this operation.</param>
    /// <returns>A task that completes once a packet has been captured, or the operation times out.</returns>
    public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor, ReadOnlySpan<Identifier> identifiers,
        int timeout = -1, bool block = false, Func<IPacket, bool>? shouldCapture = null,
        CancellationToken cancellationToken = default)
    {
        return new CaptureMessageTask(interceptor, [.. interceptor.Messages.Resolve(identifiers)], block, shouldCapture).ExecuteAsync(timeout, cancellationToken);
    }
}