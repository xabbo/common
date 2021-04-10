using System;
using System.Reflection;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher
{
    internal abstract class InterceptCallback : BindingCallback
    {
        public Destination Destination { get; }
        public bool IsOutgoing => Destination == Destination.Server;
        public bool IsIncoming => Destination == Destination.Client;

        public InterceptCallback(Destination destination, short header,
            object target, MethodInfo method, Delegate @delegate)
            : base(header, target, method, @delegate)
        {
            Destination = destination;
        }

        public abstract void Invoke(InterceptArgs e);
    }

    internal class OpenInterceptCallback : InterceptCallback
    {
        private readonly Action<object, InterceptArgs> _callback;

        public OpenInterceptCallback(Destination destination, short header,
            object target, MethodInfo method, Action<object, InterceptArgs> callback)
            : base(destination, header, target, method, callback)
        {
            _callback = callback;
        }

        public override void Invoke(InterceptArgs e)
        {
            _callback(Target, e);
        }
    }

    internal class ClosedInterceptCallback : InterceptCallback
    {
        private readonly Action<InterceptArgs> _callback;

        public ClosedInterceptCallback(Destination destination, short header,
            object target, MethodInfo method, Action<InterceptArgs> callback)
            : base(destination, header, target, method, callback)
        {
            _callback = callback;
        }

        public override void Invoke(InterceptArgs e)
        {
            _callback(e);
        }
    }
}
