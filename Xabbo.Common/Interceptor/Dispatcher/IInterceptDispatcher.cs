using System;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher
{
    /// <summary>
    /// A service that creates bindings for routing messages
    /// to methods decorated with receive or intercept attributes.
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
        bool IsBound(object target);

        /// <summary>
        /// Binds the specified target object to this dispatcher.
        /// </summary>
        /// <param name="target">The target object to bind.</param>
        /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
        /// <returns>
        /// <c>true</c> if successfully bound, or <c>false</c> if the target
        /// does not have a receive or intercept attribute on any of its methods.
        /// Throws if any of the message identifiers are unable to be resolved.
        /// </returns>
        bool Bind(object target, ClientType requiredClientHeaders);

        /// <summary>
        /// Releases the binding to the specified target.
        /// </summary>
        /// <returns>Whether the binding was released or not.</returns>
        bool Release(object target);

        /// <summary>
        /// Adds an intercept callback that is invoked when a message with the target header is sent to the specified destination.
        /// </summary>
        /// <param name="header">Specifies the target header to intercept.</param>
        /// <param name="callback">The action to invoke when a message is intercepted.</param>
        /// <param name="requiredClientHeaders">Specifies which client headers must be resolved for the binding to be successful.</param>
        void AddIntercept(Header header, Action<InterceptArgs> callback, ClientType requiredClientHeaders);

        /// <summary>
        /// Removes the specified intercept callback bound to the target header and destination.
        /// </summary>
        /// <returns>Whether the callback was removed or not.</returns>
        bool RemoveIntercept(Header header, Action<InterceptArgs> action);

        /// <summary>
        /// Releases all bound intercept handlers and intercept callbacks.
        /// </summary>
        void ReleaseAll();
    }
}
