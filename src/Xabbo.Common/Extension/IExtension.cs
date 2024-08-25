using System;

using Xabbo.Messages;
using Xabbo.Interceptor;

namespace Xabbo.Extension;

/// <summary>
/// Represents an extension interface provided by an interceptor service.
/// </summary>
public interface IExtension : IInterceptor
{
    /// <summary>
    /// Invoked when the extension has been initialized by the interceptor.
    /// </summary>
    event Action<InitializedArgs>? Initialized;

    /// <summary>
    /// Invoked when the extension is activated by the user.
    /// </summary>
    event Action? Activated;

    /// <summary>
    /// Invoked when a packet has been intercepted.
    /// </summary>
    event Action<Intercept>? Intercepted;
}