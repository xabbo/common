using System;
using System.Runtime.CompilerServices;

using Xabbo.Interceptor;

namespace Xabbo.Messages.Dispatcher;

/// <summary>
/// Represents a service that routes messages to callbacks and bound <see cref="IMessageHandler"/> instances.
/// </summary>
public interface IMessageDispatcher
{
    /// <summary>
    /// Routes the specified packet to all handlers bound to this dispatcher.
    /// </summary>
    void DispatchPacket(IInterceptor? sender, IReadOnlyPacket packet);

    /// <summary>
    /// Routes the specified <see cref="InterceptArgs"/> to all intercept handlers bound to this dispatcher.
    /// </summary>
    void DispatchIntercept(InterceptArgs intercept);

    /// <summary>
    /// Gets whether the specified intercept handler is bound to this dispatcher or not.
    /// </summary>
    bool IsBound(IMessageHandler handler);

    /// <summary>
    /// Binds the specified message handler instance to this dispatcher.
    /// Throws an exception if any of the message identifiers are unable to be resolved.
    /// </summary>
    /// <param name="handler">The message handler to bind.</param>
    /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
    /// <returns>
    /// <c>true</c> if successfully bound, or <c>false</c> if the intercept handler
    /// does not have any receive or intercept attributes on any of its methods.
    /// </returns>
    bool Bind(IMessageHandler handler, ClientType requiredClientHeaders);

    /// <summary>
    /// Releases the binding to the specified message handler.
    /// </summary>
    /// <returns>Whether the binding was released or not.</returns>
    bool Release(IMessageHandler handler);

    /// <summary>
    /// Registers a callback that is invoked when a message with a matching header is intercepted.
    /// </summary>
    /// <param name="headers">Specifies which headers to intercept.</param>
    /// <param name="handler">The callback to invoke when a message is intercepted.</param>
    /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
    void AddIntercept(HeaderSet headers, Action<InterceptArgs> handler, ClientType requiredClientHeaders);

    /// <summary>
    /// Registers a callback that is invoked when a packet with a matching header is intercepted.
    /// </summary>
    /// <param name="headers">Specifies which headers to intercept.</param>
    /// <param name="handler">The callback to invoke when a message is intercepted.</param>
    /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
    public void AddIntercept(ITuple headers, Action<InterceptArgs> handler, ClientType requiredClientHeaders)
        => AddIntercept(HeaderSet.FromTuple(headers), handler, requiredClientHeaders);

    /// <summary>
    /// Removes the specified intercept callback bound to the target header and destination.
    /// </summary>
    /// <returns>Whether the callback was removed or not.</returns>
    bool RemoveIntercept(Header header, Action<InterceptArgs> handler);

    /// <summary>
    /// Releases all bound intercept handlers and intercept callbacks.
    /// </summary>
    void ReleaseAll();
}
