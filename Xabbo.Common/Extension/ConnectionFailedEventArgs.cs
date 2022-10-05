using System;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IRemoteExtension.InterceptorConnectionFailed"/> event.
/// </summary>
public sealed class ConnectionFailedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the current number of attempts made to connect to the remote interceptor.
    /// </summary>
    public int Attempt { get; }

    /// <summary>
    /// Gets or sets whether to retry connecting to the remote interceptor.
    /// </summary>
    public bool Retry { get; set; }

    /// <summary>
    /// Constructs a new <see cref="ConnectionFailedEventArgs"/>.
    /// </summary>
    /// <param name="attempt">The current number of attempts made to connect to the remote interceptor.</param>
    public ConnectionFailedEventArgs(int attempt)
    {
        Attempt = attempt;
    }
}
