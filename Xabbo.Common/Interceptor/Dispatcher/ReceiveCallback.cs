using System;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher
{
    internal abstract class ReceiveCallback : BindingCallback
    {
        protected ReceiveCallback(short header, object target, Delegate @delegate)
            : base(header, target, @delegate.Method, @delegate)
        { }

        public void Invoke(object? sender, IReadOnlyPacket packet)
        {
            if (_isUnsubscribed) return;
            OnInvoked(sender, packet);
        }

        protected abstract void OnInvoked(object? sender, IReadOnlyPacket packet);
    }

    internal class OpenReceiveCallback : ReceiveCallback
    {
        private readonly Action<object, object?, IReadOnlyPacket> _callback;

        public OpenReceiveCallback(short header, object target, Action<object, object?, IReadOnlyPacket> callback)
            : base(header, target, callback)
        {
            _callback = callback;
        }

        protected override void OnInvoked(object? sender, IReadOnlyPacket packet)
        {
            _callback(Target, sender, packet);
        }
    }

    internal sealed class ClosedReceiveCallback : ReceiveCallback
    {
        private readonly Action<object?, IReadOnlyPacket> _callback;

        public ClosedReceiveCallback(short header, object target, Delegate @delegate)
            : base(header, target, @delegate)
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
}
