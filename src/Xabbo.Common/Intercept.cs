using System;

using Xabbo.Interceptor;
using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Contains the event arguments of an intercepted packet.
/// </summary>
public readonly ref struct Intercept(IInterceptor interceptor, ref IPacket packet, ref bool block)
{
    private readonly Header _originalHeader = packet.Header;
    private readonly ref IPacket _packet = ref packet;
    private readonly ref bool _blocked = ref block;

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
    public IPacket Packet
    {
        get => _packet;
        set => _packet = value ?? throw new ArgumentNullException(nameof(Packet));
    }

    /// <summary>
    /// Gets whether the packet will be blocked by the interceptor.
    /// </summary>
    public bool IsBlocked => _blocked;

    /// <summary>
    /// Gets whether the intercepted packet's unmodified header matches any of the specified identifiers.
    /// </summary>
    public bool Is(ReadOnlySpan<Identifier> identifiers) => Interceptor.Messages.Is(_originalHeader, identifiers);

    /// <summary>
    /// Flags the packet to be blocked from its destination by the interceptor.
    /// </summary>
    public void Block() => _blocked = true;
}
