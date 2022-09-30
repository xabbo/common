using System;
using System.Collections.Generic;

namespace Xabbo.Interceptor.Dispatcher;

internal sealed class InterceptorBinding
{
    public IInterceptHandler Handler { get; }
    public IReadOnlyCollection<BindingCallback> Callbacks { get; }

    public InterceptorBinding(IInterceptHandler handler, IEnumerable<BindingCallback> callbacks)
    {
        Handler = handler;
        Callbacks = new List<BindingCallback>(callbacks).AsReadOnly();
    }
}
