using System;

using Xabbo.Messages;
using Xabbo.Interceptor;

namespace Xabbo.Extension;

/// <summary>
/// Represents an extension interface provided by a packet interceptor service.
/// </summary>
public interface IExtension : IInterceptor
{
    /// <summary>
    /// Invoked when the extension has been initialized by the interceptor.
    /// </summary>
    event EventHandler<ExtensionInitializedEventArgs>? Initialized;

    /// <summary>
    /// Invoked when a connection to the game is established.
    /// </summary>
    event EventHandler<GameConnectedEventArgs>? Connected;

    /// <summary>
    /// Invoked when the extension is selected in the interceptor UI.
    /// </summary>
    event EventHandler? Clicked;

    /// <summary>
    /// Invoked when a packet has been intercepted.
    /// </summary>
    event EventHandler<InterceptArgs>? Intercepted;

    /// <summary>
    /// Invoked when the connection to the game ends.
    /// </summary>
    event EventHandler? Disconnected;
}