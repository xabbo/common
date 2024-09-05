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
    /// <summary>
    /// Registers an intercept for the specified headers with the provided <see cref="InterceptCallback"/>.
    /// </summary>
    public static IDisposable Intercept(this IInterceptor interceptor, ReadOnlySpan<Header> headers, InterceptCallback callback)
        => interceptor.Dispatcher.Register(new([new(headers, callback)]) { Persistent = true });

    /// <summary>
    /// Registers an intercept for the specified identifiers with the provided <see cref="InterceptCallback"/>.
    /// </summary>
    public static IDisposable Intercept(this IInterceptor interceptor, ReadOnlySpan<Identifier> identifiers, InterceptCallback callback)
        => interceptor.Dispatcher.Register(new([new(identifiers, callback)]) { Persistent = true });

    /// <summary>
    /// Registers an intercept for the specified message with the provided <see cref="MessageCallback{T}"/>.
    /// </summary>
    public static IDisposable Intercept<T>(this IInterceptor interceptor, MessageCallback<T> callback)
        where T : IMessage<T>
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([ IMessage<T>.CreateHandler(callback) ]) { Persistent = true });
    }

    /// <summary>
    /// Registers an intercept for the specified message with the provided <see cref="InterceptMessageCallback{T}"/>.
    /// </summary>
    public static IDisposable Intercept<T>(this IInterceptor interceptor, InterceptMessageCallback<T> callback)
        where T : IMessage<T>
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([ IMessage<T>.CreateHandler(callback) ]) { Persistent = true });
    }

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

    /// <summary>
    /// Asynchronously captures the first intercepted matching message.
    /// </summary>
    /// <typeparam name="T">The type of message to capture.</typeparam>
    /// <param name="interceptor">The interceptor.</param>
    /// <param name="timeout">The maximum time in milliseconds to wait for a message to be captured. <c>-1</c> specifies no timeout.</param>
    /// <param name="block">Whether the captured message should be blocked from its destination.</param>
    /// <param name="shouldCapture">A callback that inspects an intercepted message and return whether the message should be captured or not.</param>
    /// <param name="cancellationToken">The token used to cancel this operation.</param>
    /// <returns>A task that completes once a message has been captured, or the operation times out.</returns>
    public static async Task<T> ReceiveAsync<T>(this IInterceptor interceptor,
        int timeout = -1, bool block = false, Func<T, bool>? shouldCapture = null,
        CancellationToken cancellationToken = default
    )
        where T : IMessage<T>
    {
        using IPacket packet = await ReceiveAsync(interceptor,
            [.. T.Identifiers],
            timeout,
            block,
            (packet) => {
                int pos = 0;
                PacketReader r = new(packet, ref pos, interceptor);
                if (!T.Matches(in r)) return false;
                pos = 0;
                return shouldCapture?.Invoke(T.Parse(in r)) ?? true;
            },
            cancellationToken
        );
        return T.Parse(packet.Reader());
    }
}