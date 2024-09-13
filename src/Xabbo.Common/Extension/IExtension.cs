using System;

using Xabbo.Interceptor;
using Xabbo.Messages;

namespace Xabbo.Extension;

/// <summary>
/// Represents an extension interface provided by an interceptor service.
/// </summary>
public interface IExtension : IInterceptor
{
    /// <summary>
    /// Invoked when the extension is activated by the user.
    /// </summary>
    event Action? Activated;

    /// <summary>
    /// Invoked when a packet has been intercepted.
    /// </summary>
    event InterceptCallback? Intercepted;
}