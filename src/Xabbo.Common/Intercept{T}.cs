using System;

using Xabbo.Interceptor;
using Xabbo.Messages;

namespace Xabbo;

public readonly ref struct Intercept<T>(ref Intercept inner, T msg)
    where T : IMessage<T>
{
    /// <summary>
    /// Wraps the targeted message intercept callback. The returned
    /// <see cref="InterceptCallback"/> will parse the <typeparamref name="T"/>
    /// and pass it to the wrapped callback.
    /// </summary>
    public static InterceptCallback Wrap(InterceptCallback<T> callback) => (intercept) => {
        callback(new Intercept<T>(ref intercept, T.Parse(intercept.Packet.Reader())));
    };

    private readonly Intercept _inner = inner;

    /// <summary>
    /// Gets the parsed message for this intercept.
    /// </summary>
    public readonly T Msg = msg;

    /// <summary>
    /// Gets the interceptor that intercepted this packet.
    /// </summary>
    public IInterceptor Interceptor => _inner.Interceptor;

    /// <summary>
    /// Gets the time that the packet was intercepted.
    /// </summary>
    public DateTime Timestamp => _inner.Timestamp;

    /// <summary>
    /// Gets the direction of the packet.
    /// </summary>
    public Direction Direction => _inner.Direction;

    /// <summary>
    /// Gets the sequence number of the intercepted packet.
    /// </summary>
    public int Sequence => _inner.Sequence;

    /// <summary>
    /// Gets or replaces the intercepted packet.
    /// </summary>
    public IPacket Packet
    {
        get => _inner.Packet;
        set => _inner.Packet = value;
    }

    /// <summary>
    /// Gets if the packet is to be blocked by the interceptor.
    /// </summary>
    public bool IsBlocked => _inner.IsBlocked;

    /// <summary>
    /// Gets whether the intercepted packet's unmodified header matches any of the specified identifiers.
    /// </summary>
    public bool Is(ReadOnlySpan<Identifier> identifiers) => _inner.Is(identifiers);

    /// <summary>
    /// Flags the packet to be blocked from its destination by the interceptor.
    /// </summary>
    public void Block() => _inner.Block();
}