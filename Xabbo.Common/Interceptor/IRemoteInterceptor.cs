using System;
using System.Threading.Tasks;

namespace Xabbo.Interceptor
{
    public interface IRemoteInterceptor : IInterceptor
    {
        /// <summary>
        /// Gets whether the remote interceptor service is running or not.
        /// </summary>
        bool IsRunning { get; }

        /// <summary>
        /// Gets whether a connection to the remote interceptor is established or not.
        /// </summary>
        bool IsInterceptorConnected { get; }

        /// <summary>
        /// Runs the remote interceptor service.
        /// </summary>
        Task RunAsync();

        /// <summary>
        /// Requests to stop the remote interceptor service.
        /// </summary>
        void Stop();

        /// <summary>
        /// Invoked when a connection to the remote interceptor is established.
        /// </summary>
        event EventHandler? InterceptorConnected;

        /// <summary>
        /// Invoked when the connection to the remote interceptor ends.
        /// </summary>
        event EventHandler? InterceptorDisconnected;
    }
}
