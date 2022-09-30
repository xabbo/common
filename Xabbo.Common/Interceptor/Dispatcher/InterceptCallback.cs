using System;
using System.Reflection;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher;

internal abstract class InterceptCallback : BindingCallback
{
    public Destination Destination { get; }
    public bool IsOutgoing => Destination == Destination.Server;
    public bool IsIncoming => Destination == Destination.Client;

    public InterceptCallback(Header header, object? target, MethodInfo method, Delegate @delegate)
        : base(header, target, method, @delegate)
    {
        Destination = header.Destination;
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
    private readonly Action<InterceptArgs> _callback;

    public ClosedInterceptCallback(Header header, object? target, MethodInfo method, Action<InterceptArgs> callback)
        : base(header, target, method, callback)
    {
        _callback = callback;
    }

    public override void Invoke(InterceptArgs e)
    {
        _callback(e);
    }
}
