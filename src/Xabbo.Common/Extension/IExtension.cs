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
    event EventHandler<InitializedArgs>? Initialized;

    /// <summary>
    /// Invoked when a connection to the game is established.
    /// </summary>
    event EventHandler<GameConnectedArgs>? Connected;

    /// <summary>
    /// Invoked when the extension is activated by the user.
    /// </summary>
    event EventHandler? Activated;

    /// <summary>
    /// Invoked when a packet has been intercepted.
    /// </summary>
    event EventHandler<Intercept>? Intercepted;

    /// <summary>
    /// Invoked when the connection to the game ends.
    /// </summary>
    event EventHandler? Disconnected;
}