using Xabbo.Connection;

using Xabbo.Messages;

namespace Xabbo.Interceptor;

/// <summary>
/// Represents a packet interceptor that can read, modify and send packets.
/// </summary>
public interface IInterceptor : IConnection, IInterceptorContext
{
    /// <summary>
    /// Gets the message dispatcher associated with this interceptor.
    /// </summary>
    IMessageDispatcher Dispatcher { get; }

    IInterceptor IInterceptorContext.Interceptor => this;
}
