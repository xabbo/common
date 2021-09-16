using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;
using Xabbo.Interceptor.Tasks;
using System.Collections.Generic;

namespace Xabbo.Interceptor
{
    public static class InterceptorExtensions
    {
        /// <summary>
        /// Asynchronously captures the first packet with one of the specified target headers.
        /// Throws an <see cref="OperationCanceledException"/> if no matching packets are received
        /// after the specified <paramref name="timeout"/> in milliseconds, or the specified 
        /// <see cref="CancellationToken"/> is cancelled.
        /// </summary>
        public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor,
            IEnumerable<Header> headers, int timeout, bool blockPacket = false, CancellationToken cancellationToken = default)
        {
            if (headers is null || !headers.Any())
                throw new ArgumentException("At least one target header must be specified.");

            return new CaptureMessageTask(interceptor, headers, blockPacket)
                .ExecuteAsync(timeout, cancellationToken);
        }

        /// <summary>
        /// Asynchronously captures the first packet with one of the specified target headers.
        /// Throws an <see cref="OperationCanceledException"/> if no matching packets are received
        /// after the specified <paramref name="timeout"/> in milliseconds.
        /// </summary>
        public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor,
            Header header, int timeout, bool blockPacket = false, CancellationToken cancellationToken = default)
        {
            return ReceiveAsync(interceptor, new[] { header }, timeout, blockPacket, cancellationToken);
        }
    }
}
