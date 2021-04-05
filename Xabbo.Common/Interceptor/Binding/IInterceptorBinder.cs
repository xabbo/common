using System;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Binding
{
    /// <summary>
    /// A service that creates bindings which route intercepted messages
    /// to methods decorated with intercept attributes.
    /// </summary>
    public interface IInterceptorBinder : IDisposable
    {
        /// <summary>
        /// The interceptor that this binder is associated with.
        /// </summary>
        IInterceptor Interceptor { get; }

        /// <summary>
        /// Gets whether the specified target is bound to the interceptor or not.
        /// </summary>
        bool IsBound(object target);

        /// <summary>
        /// Binds the interceptor to the specified target.
        /// </summary>
        void Bind(object target);

        /// <summary>
        /// Releases the binding to the specified target.
        /// </summary>
        bool Release(object target);

        /// <summary>
        /// Adds an intercept callback that is invoked when a message with the target header is sent to the specified destination.
        /// </summary>
        bool AddIntercept(Destination destination, Header targetHeader, Action<InterceptArgs> callback);

        /// <summary>
        /// Removes the specified intercept callback bound to the target header and destination.
        /// </summary>
        bool RemoveIntercept(Destination destiantion, Header targetHeader, Action<InterceptArgs> callback);
    }
}
