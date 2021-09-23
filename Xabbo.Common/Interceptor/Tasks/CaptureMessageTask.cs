using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks
{
    public class CaptureMessageTask : InterceptorTask<IPacket>
    {
        private readonly bool _blockPacket;
        private readonly Header[] _targetHeaders;

        /// <summary>
        /// Asynchronously captures the first message with a matching target header.
        /// </summary>
        public CaptureMessageTask(IInterceptor interceptor,
            IEnumerable<Header> targetHeaders, bool blockPacket = false)
            : base(interceptor)
        {
            _blockPacket = blockPacket;

            if (targetHeaders is Header[] array)
            {
                _targetHeaders = array;
            }
            else
            {
                _targetHeaders = targetHeaders.ToArray();
            }
        }

        protected override void Bind()
        {
            foreach (Header header in _targetHeaders)
                _interceptor.Dispatcher.AddIntercept(header, OnIntercept, _interceptor.Client);
        }

        protected override void Release()
        {
            foreach (Header header in _targetHeaders)
                _interceptor.Dispatcher.RemoveIntercept(header, OnIntercept);
        }

        protected override Task OnExecuteAsync() => Task.CompletedTask;

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
