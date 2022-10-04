using System;

namespace Xabbo.Extension;

/// <summary>
/// The event arguments used when connection to the remote interceptor fails.
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

    public ConnectionFailedEventArgs(int attempt)
    {
        Attempt = attempt;
    }
}
