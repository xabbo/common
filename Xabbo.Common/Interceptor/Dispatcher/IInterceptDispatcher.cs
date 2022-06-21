using System;
using System.Runtime.CompilerServices;

using Xabbo.Common;
using Xabbo.Messages;
using Xabbo.Interceptor.Attributes;

namespace Xabbo.Interceptor.Dispatcher;

/// <summary>
/// Represents a service that routes messages to methods decorated with <see cref="ReceiveAttribute"/> or <see cref="InterceptAttribute"/>.
/// </summary>
public interface IInterceptDispatcher
{
    /// <summary>
    /// Routes the specified message to all targets bound to this dispatcher.
    /// </summary>
    void DispatchMessage(object? sender, IReadOnlyPacket packet);

    /// <summary>
    /// Routes the specified <see cref="InterceptArgs"/> to all targets bound to this dispatcher.
    /// </summary>
    void DispatchIntercept(InterceptArgs intercept);

    /// <summary>
    /// Gets whether the specified target instance is bound to this dispatcher or not.
    /// </summary>
    bool IsBound(IInterceptHandler handler);

    /// <summary>
    /// Binds the specified intercept handler to this dispatcher.
    /// Throws an exception if any of the message identifiers are unable to be resolved.
    /// </summary>
    /// <param name="handler">The intercept handler to bind.</param>
    /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
    /// <returns>
    /// <c>true</c> if successfully bound, or <c>false</c> if the intercept handler
    /// does not have any receive or intercept attributes on any of its methods.
    /// </returns>
    bool Bind(IInterceptHandler handler, ClientType requiredClientHeaders);

    /// <summary>
    /// Releases the binding to the specified intercept handler.
    /// </summary>
    /// <returns>Whether the binding was released or not.</returns>
    bool Release(IInterceptHandler handler);

    /// <summary>
    /// Registers a callback that is invoked when a message with a matching header is intercepted.
    /// </summary>
    /// <param name="headers">Specifies which headers to intercept.</param>
    /// <param name="callback">The callback to invoke when a message is intercepted.</param>
    /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
    void AddIntercept(HeaderSet headers, Action<InterceptArgs> callback, ClientType requiredClientHeaders);

    /// <summary>
    /// Registers a callback that is invoked when a packet with a matching header is intercepted.
    /// </summary>
    /// <param name="headers">Specifies which headers to intercept.</param>
    /// <param name="callback">The callback to invoke when a message is intercepted.</param>
    /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
    public void AddIntercept(ITuple headers, Action<InterceptArgs> callback, ClientType requiredClientHeaders)
        => AddIntercept(HeaderSet.FromTuple(headers), callback, requiredClientHeaders);

    /// <summary>
    /// Removes the specified intercept callback bound to the target header and destination.
    /// </summary>
    /// <returns>Whether the callback was removed or not.</returns>
    bool RemoveIntercept(Header header, Action<InterceptArgs> callback);

    /// <summary>
    /// Releases all bound intercept handlers and intercept callbacks.
    /// </summary>
    void ReleaseAll();
}
