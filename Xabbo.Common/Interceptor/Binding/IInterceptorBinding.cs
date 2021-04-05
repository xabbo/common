using System;

namespace Xabbo.Interceptor.Binding
{
    /// <summary>
    /// A binding that routes intercepted messages to 
    /// methods decorated with intercept attributes.
    /// </summary>
    public interface IInterceptorBinding : IDisposable
    {
        /// <summary>
        /// The binder that created this binding.
        /// </summary>
        IInterceptorBinder Binder { get; }

        /// <summary>
        /// The target instance that this binding was created for.
        /// </summary>
        object Target { get; }
    }
}
