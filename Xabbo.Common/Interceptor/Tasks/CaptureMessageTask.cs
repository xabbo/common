using System;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks
{
    public class CaptureMessageTask : InterceptorTask<IPacket>
    {
        private readonly Destination _destination;
        private readonly bool _blockPacket;
        private readonly Header[] _targetHeaders;

        public CaptureMessageTask(IInterceptor interceptor,
            Destination destination, bool blockPacket, params Header[] targetHeaders)
            : base(interceptor)
        {
            _destination = destination;
            _blockPacket = blockPacket;
            _targetHeaders = targetHeaders;
        }

        protected override void Bind()
        {
            foreach (Header header in _targetHeaders)
                _interceptor.Dispatcher.AddIntercept(_destination, header, OnIntercept);
        }

        protected override void Release()
        {
            foreach (Header header in _targetHeaders)
                _interceptor.Dispatcher.RemoveIntercept(_destination, header, OnIntercept);
        }

        protected override ValueTask OnExecuteAsync() => ValueTask.CompletedTask;

        private void OnIntercept(InterceptArgs e)
        {
            try
            {
                if (SetResult(e.Packet.Clone()))
                {
                    if (_blockPacket)
                        e.Block();
                }
            }
            catch (Exception ex) { SetException(ex); }
        }
    }
}
