using System;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IRemoteExtension.InterceptorDisconnected"/> event.
/// </summary>
public sealed class DisconnectedEventArgs(Exception? error) : EventArgs
{
    /// <summary>
    /// Gets the error that caused the disconnection.
    /// </summary>
    public Exception? Error { get; } = error;

    /// <summary>
    /// Gets or sets whether to reconnect to the remote interceptor.
    /// </summary>
    public bool Reconnect { get; set; }
}
