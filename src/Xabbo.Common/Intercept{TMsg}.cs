using System;

using Xabbo.Interceptor;
using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Contains the event arguments of an intercepted message.
/// </summary>
/// <typeparam name="TMsg">The type of the message.</typeparam>
/// <param name="inner">The inner intercept instance to wrap.</param>
public ref struct Intercept<TMsg>(ref Intercept inner)
    where TMsg : IMessage<TMsg>
{
    /// <summary>
    /// Wraps the targeted message intercept callback.
    /// </summary>
    public static InterceptCallback Wrap(InterceptCallback<TMsg> callback) => (intercept) => {
        callback(new Intercept<TMsg>(ref intercept));
    };

    private readonly Intercept _inner = inner;

    private TMsg? msg;

    /// <summary>
    /// Gets the parsed message for this intercept.
    /// </summary>
    public TMsg Msg => msg ??= TMsg.Parse(_inner.Packet.Reader());

    /// <summary>
    /// Gets the interceptor that intercepted this message.
    /// </summary>
    public readonly IInterceptor Interceptor => _inner.Interceptor;

    /// <summary>
    /// Gets the time that the message was intercepted.
    /// </summary>
    public readonly DateTime Timestamp => _inner.Timestamp;

    /// <summary>
    /// Gets the direction of the message.
    /// </summary>
    public readonly Direction Direction => _inner.Direction;

    /// <summary>
    /// Gets the sequence number of the intercepted message.
    /// </summary>
    public readonly int Sequence => _inner.Sequence;

    /// <summary>
    /// Gets whether the packet will be blocked by the interceptor.
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