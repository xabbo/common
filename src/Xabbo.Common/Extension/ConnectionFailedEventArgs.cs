using System;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IRemoteExtension.InterceptorConnectionFailed"/> event.
/// </summary>
public sealed class ConnectionFailedEventArgs(int attempt) : EventArgs
{
    /// <summary>
    /// Gets the current number of attempts made to connect to the remote interceptor.
    /// </summary>
    public int Attempt { get; } = attempt;

    /// <summary>
    /// Gets or sets whether to retry connecting to the remote interceptor.
    /// </summary>
    public bool Retry { get; set; }
}
