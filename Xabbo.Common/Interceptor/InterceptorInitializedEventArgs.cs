using System;

namespace Xabbo.Interceptor
{
    public class InterceptorInitializedEventArgs : EventArgs
    {
        public bool? IsGameConnected { get; }

        public InterceptorInitializedEventArgs(bool? isGameConnected = null)
        {
            IsGameConnected = isGameConnected;
        }
    }
}
