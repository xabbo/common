using System;

using Xabbo.Common;

namespace Xabbo.Messages;

/// <summary>
/// Contains event arguments of an intercepted packet.
/// </summary>
public class InterceptArgs : EventArgs, IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Gets the time that the packet was intercepted.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Gets the destination of the packet.
    /// </summary>
    public Destination Destination { get; }

    /// <summary>
    /// Gets the sequence number of the intercepted packet.
    /// </summary>
    public int Step { get; init; }

    /// <summary>
    /// Gets or replaces the intercepted packet.
    /// </summary>
    public IPacket Packet { get; set; }

    /// <summary>
    /// Gets the original, unmodified packet that was intercepted.
    /// </summary>
    public IReadOnlyPacket OriginalPacket { get; }

    /// <summary>
    /// Gets if the packet's destination is to the client.
    /// </summary>
    public bool IsIncoming => Destination == Destination.Client;

    /// <summary>
    /// Gets if the packet's destination is to the server.
    /// </summary>
    public bool IsOutgoing => Destination == Destination.Server;

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
    /// Constructs a new InterceptArgs instance with the specified destination and packet.
    /// </summary>
    public InterceptArgs(Destination destination, IPacket packet)
    {
        Timestamp = DateTime.Now;
        Destination = destination;
        Packet = packet;

        OriginalPacket = new Packet(packet.Protocol, packet.Header, packet.Buffer);
    }

    /// <summary>
    /// Flags the packet to be blocked from its destination by the interceptor.
    /// </summary>
    public void Block() => IsBlocked = true;

    /// <inheritdoc cref="Dispose()" />
    protected virtual void Dispose(bool disposing)
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
