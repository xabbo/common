using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks
{
    public abstract class InterceptorTask<TResult>
    {
        private readonly SemaphoreSlim _executionSemaphore = new SemaphoreSlim(1);
        private readonly TaskCompletionSource<TResult> _completion;

        protected readonly IInterceptor _interceptor;
        protected Incoming In => _interceptor.Messages.In;
        protected Outgoing Out => _interceptor.Messages.Out;

        protected InterceptorTask(IInterceptor interceptor)
        {
            _interceptor = interceptor;
            _completion = new TaskCompletionSource<TResult>(
                TaskCreationOptions.RunContinuationsAsynchronously
            );
        }

        public TResult Execute(int timeout, CancellationToken token) => ExecuteAsync(timeout, token).GetAwaiter().GetResult();

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

                return await ExecuteAsync().ConfigureAwait(false);
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

        protected virtual void Bind()
        {
            _interceptor.Dispatcher.Bind(this, _interceptor.Client);
            _interceptor.Disconnected += OnDisconnected;
        }

        protected virtual void Release()
        {
            _interceptor.Dispatcher.Release(this);
            _interceptor.Disconnected -= OnDisconnected;
        }

        /// <summary>
        /// Called when the connection ends while the interceptor task is in progress.
        /// </summary>
        protected virtual void OnDisconnected(object? sender, EventArgs e)
        {
            _completion.TrySetCanceled();
        }

        protected abstract Task OnExecuteAsync();

        protected virtual async Task<TResult> ExecuteAsync()
        {
            await OnExecuteAsync();
            return await _completion.Task;
        }

        protected bool SetResult(TResult result) => _completion.TrySetResult(result);
        protected bool SetCanceled() => _completion.TrySetCanceled();
        protected bool SetException(Exception ex) => _completion.TrySetException(ex);

        protected void Send(Header header, params object[] values) => _interceptor.Send(header, values);
        protected void Send(IReadOnlyPacket packet) => _interceptor.Send(packet);
        protected Task SendAsync(Header header, params object[] values) => _interceptor.SendAsync(header, values);
        protected Task SendAsync(IReadOnlyPacket packet) => _interceptor.SendAsync(packet);
    }
}
