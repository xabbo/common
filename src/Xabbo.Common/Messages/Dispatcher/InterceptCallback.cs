using System;
using System.Reflection;

namespace Xabbo.Messages.Dispatcher;

internal abstract class InterceptCallback(Header header, object? target, MethodInfo method, Delegate @delegate)
    : BindingCallback(header, target, method, @delegate)
{
    public Direction Direction { get; } = header.Direction;
    public bool IsOutgoing => Direction == Direction.Outgoing;
    public bool IsIncoming => Direction == Direction.Incoming;

    public abstract void Invoke(InterceptArgs e);
}

internal sealed class OpenInterceptCallback(Header header, object target, MethodInfo method, Action<object, InterceptArgs> callback)
    : InterceptCallback(header, target, method, callback)
{
    private readonly Action<object, InterceptArgs> _callback = callback;

    public override void Invoke(InterceptArgs e)
    {
        if (Target is null)
            throw new NullReferenceException("Open intercept callback target is null.");

        _callback(Target, e);
    }
}

internal sealed class ClosedInterceptCallback(Header header, object? target, MethodInfo method, Action<InterceptArgs> handler)
    : InterceptCallback(header, target, method, handler)
{
    private readonly Action<InterceptArgs> _handler = handler;

    public override void Invoke(InterceptArgs e) => _handler(e);
}
