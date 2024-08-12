using System;

using Xabbo.Interceptor;

namespace Xabbo.Messages;

/// <summary>
/// Represents a message handler that can be attached to an interceptor.
/// </summary>
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