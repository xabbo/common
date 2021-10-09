using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks
{
    /// <summary>
    /// A base class for implementing a task that can intercept packets and asynchronously return a result.
    /// </summary>
    /// <typeparam name="TResult">The result type of the task.</typeparam>
    public abstract class InterceptorTask<TResult>
    {
        private readonly SemaphoreSlim _executionSemaphore = new SemaphoreSlim(1);
        private readonly TaskCompletionSource<TResult> _completion;

        /// <summary>
        /// The interceptor that this task is bound to.
        /// </summary>
        protected readonly IInterceptor _interceptor;

        /// <summary>
        /// The incoming messages provided by the interceptor's message manager.
        /// </summary>
        protected Incoming In => _interceptor.Messages.In;

        /// <summary>
        /// The outgoing messages provided by the interceptor's message manager.
        /// </summary>
        protected Outgoing Out => _interceptor.Messages.Out;

        /// <summary>
        /// Creates a new interceptor task bound to the specified interceptor.
        /// </summary>
        /// <param name="interceptor">The interceptor to bind to.</param>
        protected InterceptorTask(IInterceptor interceptor)
        {
            _interceptor = interceptor;
            _completion = new TaskCompletionSource<TResult>(
                TaskCreationOptions.RunContinuationsAsynchronously
            );
        }

        /// <summary>
        /// Executes the task synchronously and returns the result.
        /// </summary>
        /// <param name="timeout">The maximum number of milliseconds to wait for a result. Use <c>-1</c> for no timeout.</param>
        /// <param name="cancellationToken">The cancellation token that can be used to cancel the task.</param>
        public TResult Execute(int timeout, CancellationToken cancellationToken) => ExecuteAsync(timeout, cancellationToken).GetAwaiter().GetResult();

        /// <summary>
        /// Executes the task asynchronously and returns the result.
        /// </summary>
        /// <param name="timeout">The maximum number of milliseconds to wait for a result. Use <c>-1</c> for no timeout.</param>
        /// <param name="cancellationToken">The cancellation token that can be used to cancel the task.</param>
        public async Task<TResult> ExecuteAsync(int timeout, CancellationToken cancellationToken)
        {
            if (!_executionSemaphore.Wait(0, cancellationToken))
                throw new InvalidOperationException("The interceptor task has already been executed.");

            CancellationTokenSource? cts = null;

            try
            {
                cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                cts.Token.Register(() => SetCanceled());
                if (timeout > 0) cts.CancelAfter(timeout);

                Bind();

                await OnExecuteAsync().ConfigureAwait(false);
                return await _completion.Task;
            }
            catch (OperationCanceledException ex)
            when (!cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException(
                    $"The interceptor task '{GetType().FullName}' timed out.",
                    ex.CancellationToken
                );
            }
            finally
            {
                Release();
                cts?.Cancel();
                cts?.Dispose();
            }
        }

        /// <summary>
        /// Binds this task to the interceptor.
        /// </summary>
        protected virtual void Bind()
        {
            _interceptor.Dispatcher.Bind(this, _interceptor.Client);
            _interceptor.Disconnected += OnDisconnected;
        }

        /// <summary>
        /// Releases this task from the interceptor.
        /// </summary>
        protected virtual void Release()
        {
            _interceptor.Dispatcher.Release(this);
            _interceptor.Disconnected -= OnDisconnected;
        }

        /// <summary>
        /// Invoked when the connection ends while the interceptor task is in progress.
        /// </summary>
        protected virtual void OnDisconnected(object? sender, EventArgs e)
        {
            _completion.TrySetException(
                new Exception($"Disconnected while executing interceptor task '{GetType().FullName}'.")
            );
        }

        /// <summary>
        /// Invoked when the task is executed.
        /// </summary>
        protected abstract Task OnExecuteAsync();

        /// <summary>
        /// Attempts to set the result of this task to the specified value.
        /// </summary>
        protected bool SetResult(TResult result) => _completion.TrySetResult(result);
        /// <summary>
        /// Attempts to set the status of this task to canceled.
        /// </summary>
        protected bool SetCanceled() => _completion.TrySetCanceled();
        /// <summary>
        /// Attempts to set the result of this task to the specified exception.
        /// </summary>
        protected bool SetException(Exception ex) => _completion.TrySetException(ex);

        /// <summary>
        /// Sends a packet with the specified header and values.
        /// </summary>
        protected void Send(Header header, params object[] values) => _interceptor.Send(header, values);
        /// <summary>
        /// Sends the specified packet.
        /// </summary>
        protected void Send(IReadOnlyPacket packet) => _interceptor.Send(packet);
        /// <summary>
        /// Sends a packet with the specified header and values.
        /// </summary>
        protected Task SendAsync(Header header, params object[] values) => _interceptor.SendAsync(header, values);
        /// <summary>
        /// Sends the specified packet.
        /// </summary>
        protected Task SendAsync(IReadOnlyPacket packet) => _interceptor.SendAsync(packet);
    }
}
