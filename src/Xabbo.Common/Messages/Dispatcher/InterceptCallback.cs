using System;
using System.Reflection;

namespace Xabbo.Messages.Dispatcher;

internal abstract class InterceptCallback : BindingCallback
{
    public Direction Direction { get; }
    public bool IsOutgoing => Direction == Direction.Outgoing;
    public bool IsIncoming => Direction == Direction.Incoming;

    public InterceptCallback(Header header, object? target, MethodInfo method, Delegate @delegate)
        : base(header, target, method, @delegate)
    {
        Direction = header.Direction;
    }

    public abstract void Invoke(InterceptArgs e);
}

internal sealed class OpenInterceptCallback : InterceptCallback
{
    private readonly Action<object, InterceptArgs> _callback;

    public OpenInterceptCallback(Header header, object target, MethodInfo method, Action<object, InterceptArgs> callback)
        : base(header, target, method, callback)
    {
        _callback = callback;
    }

    public override void Invoke(InterceptArgs e)
    {
        if (Target is null)
            throw new NullReferenceException("Open intercept callback target is null.");

        _callback(Target, e);
    }
}

internal sealed class ClosedInterceptCallback : InterceptCallback
{
    private readonly Action<InterceptArgs> _handler;

    public ClosedInterceptCallback(Header header, object? target, MethodInfo method, Action<InterceptArgs> handler)
        : base(header, target, method, handler)
    {
        _handler = handler;
    }

    public override void Invoke(InterceptArgs e)
    {
        _handler(e);
    }
}
