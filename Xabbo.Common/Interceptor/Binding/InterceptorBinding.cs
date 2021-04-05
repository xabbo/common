using System;
using System.Collections.Generic;

namespace Xabbo.Interceptor.Binding
{
    internal class InterceptorBinding
    {
        public object Target { get; }
        public IReadOnlyCollection<BindingCallback> Callbacks { get; }

        public InterceptorBinding(object target, IEnumerable<BindingCallback> callbacks)
        {
            Target = target;
            Callbacks = new List<BindingCallback>(callbacks).AsReadOnly();
        }
    }
}
