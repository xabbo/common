﻿using System;

using Xabbo.Interceptor;

namespace Xabbo.Messages;

/// <summary>
/// Contains event arguments of an intercepted packet.
/// </summary>
/// <remarks>
/// Constructs a new InterceptArgs instance with the specified destination and packet.
/// </remarks>
public sealed class InterceptArgs(IInterceptor interceptor, Direction destination, IPacket packet) : EventArgs, IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Gets the interceptor that intercepted this packet.
    /// </summary>
    public IInterceptor Interceptor { get; } = interceptor;

    /// <summary>
    /// Gets the time that the packet was intercepted.
    /// </summary>
    public DateTime Timestamp { get; } = DateTime.Now;

    /// <summary>
    /// Gets the direction of the packet.
    /// </summary>
    public Direction Direction { get; } = destination;

    /// <summary>
    /// Gets the sequence number of the intercepted packet.
    /// </summary>
    public int Step { get; init; }

    /// <summary>
    /// Gets or replaces the intercepted packet.
    /// </summary>
    public IPacket Packet { get; set; } = packet;

    /// <summary>
    /// Gets the original, unmodified packet that was intercepted.
    /// </summary>
    public IReadOnlyPacket OriginalPacket { get; } = new Packet(packet.Header, packet.Buffer, packet.Protocol);

    /// <summary>
    /// Gets if the packet's direction is incoming.
    /// </summary>
    public bool IsIncoming => Direction == Direction.Incoming;

    /// <summary>
    /// Gets if the packet's direction is outgoing.
    /// </summary>
    public bool IsOutgoing => Direction == Direction.Outgoing;

    /// <summary>
    /// Gets if the packet is to be blocked by the interceptor.
    /// </summary>
    public bool IsBlocked { get; private set; }

    /// <summary>
    /// Gets if the packet has been modified from its original state.
    /// </summary>
    public bool IsModified =>
        Packet.Header != OriginalPacket.Header ||
        Packet.Length != OriginalPacket.Length ||
        !Packet.Buffer.SequenceEqual(OriginalPacket.Buffer);

    /// <summary>
    /// Flags the packet to be blocked from its destination by the interceptor.
    /// </summary>
    public void Block() => IsBlocked = true;

    /// <inheritdoc cref="Dispose()" />
    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;

        if (disposing)
        {
            Packet.Dispose();
            OriginalPacket.Dispose();
        }
    }

    /// <summary>
    /// Disposes this InterceptArgs instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
