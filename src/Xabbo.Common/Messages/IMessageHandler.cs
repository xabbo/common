using System;

using Xabbo.Interceptor;

namespace Xabbo.Messages;

/// <summary>
/// Represents a message handler that can be attached to an interceptor.
/// </summary>
/// <remarks>
/// The source generator will implement this interface on partial classes marked with
/// <see cref="InterceptAttribute"/> or <see cref="ExtensionAttribute"/>.
/// Intercept handlers will be generated for methods marked with <see cref="InterceptAttribute"/>,
/// <see cref="InterceptInAttribute"/> or <see cref="InterceptOutAttribute"/>.
/// </remarks>
public interface IMessageHandler
{
    /// <summary>
    /// Attaches the handler to the specified interceptor.
    /// </summary>
    /// <returns>
    /// An <see cref="IDisposable"/> that detaches the handler
    /// when <see cref="IDisposable.Dispose"/> is called.
    /// </returns>
    IDisposable Attach(IInterceptor interceptor);
}
