using System;
using System.Collections.Generic;

namespace Xabbo.Messages.Dispatcher;

internal sealed class InterceptorBinding
{
    public IMessageHandler Handler { get; }
    public IReadOnlyCollection<BindingCallback> Callbacks { get; }

    public InterceptorBinding(IMessageHandler handler, IEnumerable<BindingCallback> callbacks)
    {
        Handler = handler;
        Callbacks = new List<BindingCallback>(callbacks).AsReadOnly();
    }
}
