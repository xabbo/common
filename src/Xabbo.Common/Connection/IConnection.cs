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
    /// Gets the message manager associated with this connection.
    /// </summary>
    IMessageManager Messages { get; }

    /// <summary>
    /// Gets if the connection is established.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets a cancellation token that is triggered when the connection is lost.
    /// </summary>
    CancellationToken DisconnectToken { get; }

    /// <summary>
    /// Gets the client for the current session.
    /// </summary>
    ClientInfo Client { get; }

    /// <summary>
    /// Gets the hotel for the current session.
    /// </summary>
    Hotel Hotel { get; }

    /// <summary>
    /// Sends a packet to the client or server, specified by the direction of the packet's header.
    /// </summary>
    void Send(IReadOnlyPacket packet);

    /// <summary>
    /// Sends a packet to the client or server, specified by the direction of the header.
    /// </summary>
    void Send(Header header, params object[] values);

    /// <summary>
    /// Sends a packet to the client or server, specified by the direction of the identifier.
    /// </summary>
    void Send(Identifier identifier, params object[] values);

    /// <summary>
    /// Asynchronously captures the first packet sent or received with any of the specified headers.
    /// </summary>
    /// <param name="headers">Specifies which headers to listen for.</param>
    /// <param name="timeout">The maximum time in milliseconds to wait for a packet to be captured. <c>-1</c> specifies no timeout.</param>
    /// <param name="block">Whether the captured packet should be blocked from its destination.</param>
    /// <param name="shouldCapture">A callback that inspects intercepted packets and return whether the packet should be captured or not.</param>
    /// <param name="cancellationToken">The token used to cancel this operation.</param>
    /// <returns>A task that completes once a packet has been captured, or the operation times out.</returns>
    Task<IPacket> ReceiveAsync(ReadOnlySpan<Header> headers,
        int timeout = -1, bool block = false,
        Func<IReadOnlyPacket, bool>? shouldCapture = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously captures the first packet sent or received with any of the specified headers.
    /// </summary>
    /// <param name="identifiers">Specifies which messages to listen for.</param>
    /// <param name="timeout">The maximum time in milliseconds to wait for a packet to be captured. <c>-1</c> specifies no timeout.</param>
    /// <param name="block">Whether the captured packet should be blocked from its destination.</param>
    /// <param name="shouldCapture">A callback that inspects intercepted packets and return whether the packet should be captured or not.</param>
    /// <param name="cancellationToken">The token used to cancel this operation.</param>
    /// <returns>A task that completes once a packet has been captured, or the operation times out.</returns>
    Task<IPacket> ReceiveAsync(ReadOnlySpan<Identifier> identifiers,
        int timeout = -1, bool block = false,
        Func<IReadOnlyPacket, bool>? shouldCapture = null,
        CancellationToken cancellationToken = default);
}
