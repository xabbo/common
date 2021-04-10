using System;
using System.Reflection;

namespace Xabbo.Interceptor.Dispatcher
{
    public abstract class BindingCallback
    {
        protected volatile bool _isUnsubscribed = false;

        public short Header { get; }
        public object Target { get; }
        public MethodInfo Method { get; }
        public Delegate Delegate { get; }
        public bool IsUnsubscribed => _isUnsubscribed;

        public BindingCallback(short header, object target, MethodInfo method, Delegate @delegate)
        {
            Header = header;
            Target = target;
            Method = method;
            Delegate = @delegate;
        }

        public void Unsubscribe()
        {
            _isUnsubscribed = true;
        }
    }
}
