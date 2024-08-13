using System;

using Xabbo.Interceptor;

namespace Xabbo.Messages;

/// <summary>
/// Contains the event arguments of an intercepted packet.
/// </summary>
public sealed class Intercept(IInterceptor interceptor, IPacket packet) : EventArgs, IDisposable
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
    public Direction Direction { get; } = packet.Header.Direction;

    /// <summary>
    /// Gets the sequence number of the intercepted packet.
    /// </summary>
    public int Sequence { get; init; }

    /// <summary>
    /// Gets or replaces the intercepted packet.
    /// </summary>
    public IPacket Packet { get; set; } = packet;

    /// <summary>
    /// Gets the original, unmodified packet that was intercepted.
    /// </summary>
    public IReadOnlyPacket OriginalPacket { get; } = new Packet(packet.Header, packet.Buffer);

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
    /// Gets whether the intercepted packet's header matches any of the specified identifiers.
    /// </summary>
    public bool Is(ReadOnlySpan<Identifier> identifiers) => Interceptor.Messages.Is(OriginalPacket.Header, identifiers);

    /// <summary>
    /// Flags the packet to be blocked from its destination by the interceptor.
    /// </summary>
    public void Block() => IsBlocked = true;

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
    /// Disposes this Intercept instance.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
