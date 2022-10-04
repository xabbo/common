using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Connection;

/// <summary>
/// A base class for <see cref="IConnection"/> implementations.
/// This class provides the generic Send/Receive extension methods from within subclasses
/// without requiring the <see langword="this"/> keyword to access them.
/// </summary>
public abstract partial class ConnectionBase : IConnection
{
    /* Generic Send/Receive methods are implemented by the source generator. */

    /// <inheritdoc/>
    public abstract bool IsConnected { get; }

    /// <inheritdoc/>
    public abstract CancellationToken DisconnectToken { get; }

    /// <inheritdoc/>
    public abstract ClientType Client { get; }

    /// <inheritdoc/>
    public abstract string ClientIdentifier { get; }

    /// <inheritdoc/>
    public abstract void Send(IReadOnlyPacket packet);

    /// <inheritdoc/>
    public abstract ValueTask SendAsync(IReadOnlyPacket packet);

    /// <inheritdoc/>
    public abstract Task<IPacket> ReceiveAsync(HeaderSet headers, int timeout = -1, bool block = false, CancellationToken cancellationToken = default);

    /// <inheritdoc/>
    public abstract Task<IPacket> ReceiveAsync(HeaderSet headers, Func<IReadOnlyPacket, bool> shouldCapture, int timeout = -1, bool block = false, CancellationToken cancellationToken = default);
}
