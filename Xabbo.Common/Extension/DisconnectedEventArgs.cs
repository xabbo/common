using System;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IRemoteExtension.InterceptorDisconnected"/> event.
/// </summary>
public sealed class DisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the error that caused the disconnection.
    /// </summary>
    public Exception? Error { get; }

    /// <summary>
    /// Gets or sets whether to reconnect to the remote interceptor.
    /// </summary>
    public bool Reconnect { get; set; }

    /// <summary>
    /// Constructs a new <see cref="DisconnectedEventArgs"/>.
    /// </summary>
    /// <param name="error">The error that caused the disconnection.</param>
    public DisconnectedEventArgs(Exception? error)
    {
        Error = error;
    }
}
