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
        /// The instance type must have at least one receive or intercept attribute on one of its methods.
        /// </summary>
        void Bind(object target);

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
    }
}
