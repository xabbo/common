using System;
using System.Reflection;

namespace Xabbo.Messages.Dispatcher;

public abstract class BindingCallback(Header header, object? target, MethodInfo method, Delegate @delegate)
{
    protected volatile bool _isUnsubscribed = false;

    public Header Header { get; } = header;
    public object? Target { get; } = target;
    public MethodInfo Method { get; } = method;
    public Delegate Delegate { get; } = @delegate;
    public bool IsUnsubscribed => _isUnsubscribed;

    public void Unsubscribe() => _isUnsubscribed = true;
}
