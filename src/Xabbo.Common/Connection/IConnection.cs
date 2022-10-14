using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Connection;

/// <summary>
/// Represents a connection to the game server.
/// </summary>
public interface IConnection
{
    /// <summary>
    /// Gets if the connection is established.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets a cancellation token that is triggered when the connection is lost.
    /// </summary>
    CancellationToken DisconnectToken { get; }

    /// <summary>
    /// Gets the client type for this connnection.
    /// </summary>
    ClientType Client { get; }

    /// <summary>
    /// Gets the client identifier for this connection.
    /// </summary>
    string ClientIdentifier { get; }

    /// <summary>
    /// Sends a packet to the client or server, specified by the destination of the packet's header.
    /// </summary>
    void Send(IReadOnlyPacket packet);

    /// <summary>
    /// Asynchronously sends a packet to the client or server, specified by the destination of the packet's header.
    /// </summary>
    ValueTask SendAsync(IReadOnlyPacket packet);

    /// <summary>
    /// Asynchronously captures the first packet sent or received with any of the specified headers.
    /// </summary>
    /// <param name="headers">Specifies which headers to listen for.</param>
    /// <param name="timeout">The maximum time in milliseconds to wait for a packet to be captured. <c>-1</c> specifies no timeout.</param>
    /// <param name="block">Whether the captured packet should be blocked from its destination.</param>
    /// <param name="cancellationToken">The token used to cancel this operation.</param>
    /// <returns>A task that completes once a packet has been captured, or the operation times out.</returns>
    Task<IPacket> ReceiveAsync(HeaderSet headers, int timeout = -1, bool block = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously captures the first packet sent or received with any of the specified headers,
    /// where the provided callback <paramref name="shouldCapture"/> returns <see langword="true"/>.
    /// </summary>
    /// <param name="headers">Specifies which headers to listen for.</param>
    /// <param name="shouldCapture">A callback that may inspect packets with matching headers and return whether the packet should be captured or not.</param>
    /// <param name="timeout">The maximum time in milliseconds to wait for a packet to be captured. <c>-1</c> specifies no timeout.</param>
    /// <param name="block">Whether the captured packet should be blocked from its destination.</param>
    /// <param name="cancellationToken">The token used to cancel this operation.</param>
    /// <returns>A task that completes once a packet has been captured, or the operation times out.</returns>
    Task<IPacket> ReceiveAsync(HeaderSet headers, Func<IReadOnlyPacket, bool> shouldCapture, int timeout = -1, bool block = false, CancellationToken cancellationToken = default);
}
