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
        public static Task<IPacket> ReceiveAsync(this IInterceptor interceptor,
            int timeout, CancellationToken cancellationToken,
            params Header[] targetHeaders)
        {
            if (targetHeaders.Length == 0)
                throw new ArgumentException("At least one target header must be specified.");

            if (targetHeaders.Any(header => header.Destination == Destination.Client))
                throw new InvalidOperationException("Outgoing target header specified in ReceiveAsync.");

            return new CaptureMessageTask(interceptor, Destination.Client, false, targetHeaders)
                .ExecuteAsync(timeout, cancellationToken);
        }

        public static Task<IPacket> CaptureOutAsync(this IInterceptor interceptor,
            int timeout, CancellationToken cancellationToken,
            params Header[] targetHeaders)
        {
            if (targetHeaders.Length == 0)
                throw new ArgumentException("At least one target header must be specified.");

            if (targetHeaders.Any(header => header.Destination == Destination.Client))
                throw new InvalidOperationException("Incoming target header specified in CaptureOutAsync.");

            return new CaptureMessageTask(interceptor, Destination.Server, false, targetHeaders)
                .ExecuteAsync(timeout, cancellationToken);
        }
    }
}
