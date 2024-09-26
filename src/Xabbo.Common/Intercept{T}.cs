using System;

using Xabbo.Interceptor;
using Xabbo.Messages;

namespace Xabbo;

public ref struct Intercept<T>(ref Intercept inner)
    where T : IMessage<T>
{
    /// <summary>
    /// Wraps the targeted message intercept callback.
    /// </summary>
    public static InterceptCallback Wrap(InterceptCallback<T> callback) => (intercept) => {
        callback(new Intercept<T>(ref intercept));
    };

    private readonly Intercept _inner = inner;

    private T? msg;

    /// <summary>
    /// Gets the parsed message for this intercept.
    /// </summary>
    public T Msg => msg ??= T.Parse(_inner.Packet.Reader());

    /// <summary>
    /// Gets the interceptor that intercepted this packet.
    /// </summary>
    public readonly IInterceptor Interceptor => _inner.Interceptor;

    /// <summary>
    /// Gets the time that the packet was intercepted.
    /// </summary>
    public readonly DateTime Timestamp => _inner.Timestamp;

    /// <summary>
    /// Gets the direction of the packet.
    /// </summary>
    public readonly Direction Direction => _inner.Direction;

    /// <summary>
    /// Gets the sequence number of the intercepted packet.
    /// </summary>
    public readonly int Sequence => _inner.Sequence;

    /// <summary>
    /// Gets if the packet is to be blocked by the interceptor.
    /// </summary>
    public readonly bool IsBlocked => _inner.IsBlocked;

    /// <summary>
    /// Gets whether the intercepted packet's unmodified header matches any of the specified identifiers.
    /// </summary>
    public readonly bool Is(ReadOnlySpan<Identifier> identifiers) => _inner.Is(identifiers);

    /// <summary>
    /// Flags the packet to be blocked from its destination by the interceptor.
    /// </summary>
    public readonly void Block() => _inner.Block();
}