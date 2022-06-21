using System;

namespace Xabbo.Interceptor;

/// <summary>
/// The event arguments used when the connection to the remote interceptor is lost.
/// </summary>
public class DisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the error that caused the disconnection.
    /// </summary>
    public Exception? Error { get; }

    /// <summary>
    /// Gets or sets whether to reconnect to the remote interceptor.
    /// </summary>
    public bool Reconnect { get; set; }

    public DisconnectedEventArgs(Exception? error)
    {
        Error = error;
    }
}
