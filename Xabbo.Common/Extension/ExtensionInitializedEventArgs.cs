using System;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IExtension.Initialized"/> event.
/// </summary>
public class ExtensionInitializedEventArgs : EventArgs
{
    /// <summary>
    /// Gets whether the game is already connected at the time of initialization, if provided by the interceptor service.
    /// </summary>
    public bool? IsGameConnected { get; }

    /// <summary>
    /// Constructs a new <see cref="ExtensionInitializedEventArgs"/>.
    /// </summary>
    /// <param name="isGameConnected">Whether the game is already connected at the time of initialization.</param>
    public ExtensionInitializedEventArgs(bool? isGameConnected = null)
    {
        IsGameConnected = isGameConnected;
    }
}
