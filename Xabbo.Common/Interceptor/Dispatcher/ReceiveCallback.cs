using System;
using System.Reflection;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher;

internal abstract class ReceiveCallback : BindingCallback
{
    protected ReceiveCallback(Header header, object target, MethodInfo targetMethod, Delegate @delegate)
        : base(header, target, targetMethod, @delegate)
    { }

    public void Invoke(object? sender, IReadOnlyPacket packet)
    {
        if (_isUnsubscribed) return;
        OnInvoked(sender, packet);
    }

    protected abstract void OnInvoked(object? sender, IReadOnlyPacket packet);
}

internal sealed class OpenReceiveCallback : ReceiveCallback
{
    private readonly Action<object, object?, IReadOnlyPacket> _callback;

    public OpenReceiveCallback(Header header, object target, MethodInfo targetMethod, Action<object, object?, IReadOnlyPacket> callback)
        : base(header, target, targetMethod, callback)
    {
        _callback = callback;
    }

    protected override void OnInvoked(object? sender, IReadOnlyPacket packet)
    {
        if (Target is null)
            throw new NullReferenceException("Open receive callback target is null.");

        _callback(Target, sender, packet);
    }
}

internal sealed class ClosedReceiveCallback : ReceiveCallback
{
    private readonly Action<object?, IReadOnlyPacket> _callback;

    public ClosedReceiveCallback(Header header, object target, MethodInfo targetMethod, Delegate @delegate)
        : base(header, target, targetMethod, @delegate)
    {
        if (@delegate is not Action<object?, IReadOnlyPacket> callback)
            throw new Exception($"Invalid delegate type {@delegate.GetType().Name} for {GetType().Name}");

        _callback = callback;
    }

    protected override void OnInvoked(object? sender, IReadOnlyPacket packet)
    {
        _callback(sender, packet);
    }
}
