using System;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IExtension.Initialized"/> event.
/// </summary>
public class ExtensionInitializedEventArgs(bool? isGameConnected = null) : EventArgs
{
    /// <summary>
    /// Gets whether the game is already connected at the time of initialization, if provided by the interceptor service.
    /// </summary>
    public bool? IsGameConnected { get; } = isGameConnected;
}
