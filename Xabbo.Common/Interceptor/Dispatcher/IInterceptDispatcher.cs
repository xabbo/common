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
        /// Binds the specified target instance to this dispatcher.
        /// Returns <c>true</c> if successfully bound, or <c>false</c> if the target
        /// does not have a receive or intercept attribute on any of its methods.
        /// Throws if any of the message identifiers are unable to be resolved.
        /// </summary>
        bool Bind(object target);

        /// <summary>
        /// Releases the binding to the specified target.
        /// </summary>
        /// <returns>Whether the target instance was released or not.</returns>
        bool Release(object target);

        /// <summary>
        /// Adds an intercept callback that is invoked when a message with the target header is sent to the specified destination.
        /// </summary>
        void AddIntercept(Destination destination, Header targetHeader, Action<InterceptArgs> callback);

        /// <summary>
        /// Removes the specified intercept callback bound to the target header and destination.
        /// </summary>
        /// <returns>Whether the callback was removed or not.</returns>
        bool RemoveIntercept(Destination destination, Header targetHeader, Action<InterceptArgs> callback);

        /// <summary>
        /// Releases all bound intercept handlers and intercept callbacks.
        /// </summary>
        void ReleaseAll();
    }
}
