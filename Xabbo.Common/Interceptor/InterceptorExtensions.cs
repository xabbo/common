using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;
using Xabbo.Interceptor.Tasks;

namespace Xabbo.Interceptor
{
    public static class InterceptorExtensions
    {
        /// <summary>
        /// Asynchronously captures the first incoming packet with one of the specified target headers.
        /// Throws an <see cref="OperationCanceledException"/> if no matching packets are received
        /// after the specified <paramref name="timeout"/> in milliseconds, or the specified 
        /// <see cref="CancellationToken"/> is cancelled.
        /// </summary>
        public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor,
            int timeout, CancellationToken cancellationToken, params Header[] targetHeaders)
        {
            if (targetHeaders is null || targetHeaders.Length == 0)
                throw new ArgumentException("At least one target header must be specified.");

            if (targetHeaders.Any(header => header.Destination == Destination.Client))
                throw new InvalidOperationException("Outgoing target header specified in ReceiveAsync.");

            return new CaptureMessageTask(interceptor, Destination.Client, false, targetHeaders)
                .ExecuteAsync(timeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously captures the first incoming packet with one of the specified target headers.
        /// Throws an <see cref="OperationCanceledException"/> if no matching packets are received
        /// after the specified <paramref name="timeout"/> in milliseconds.
        /// </summary>
        public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor,
            int timeout, params Header[] targetHeaders)
        {
            return ReceiveAsync(interceptor, timeout, CancellationToken.None, targetHeaders);
        }

        /// <summary>
        /// Asynchronously captures the first outgoing packet with one of the specified target headers.
        /// Throws an <see cref="OperationCanceledException"/> if no matching packets are sent
        /// after the specified <paramref name="timeout"/> in milliseconds, or the specified 
        /// <see cref="CancellationToken"/> is cancelled.
        /// </summary>
        public static Task<IPacket> CaptureOutAsync(this IInterceptor interceptor,
            int timeout, CancellationToken cancellationToken, params Header[] targetHeaders)
        {
            if (targetHeaders is null || targetHeaders.Length == 0)
                throw new ArgumentException("At least one target header must be specified.");

            if (targetHeaders.Any(header => header.Destination == Destination.Client))
                throw new InvalidOperationException("Incoming target header specified in CaptureOutAsync.");

            return new CaptureMessageTask(interceptor, Destination.Server, false, targetHeaders)
                .ExecuteAsync(timeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously captures the first outgoing packet with one of the specified target headers.
        /// Throws an <see cref="OperationCanceledException"/> if no matching packets are sent
        /// after the specified <paramref name="timeout"/> in milliseconds.
        /// </summary>
        public static Task<IPacket> CaptureOutAsync(this IInterceptor interceptor,
            int timeout, params Header[] targetHeaders)
        {
            return CaptureOutAsync(interceptor, timeout, CancellationToken.None, targetHeaders);
        }
    }
}
