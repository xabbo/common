using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks;

/// <summary>
/// A base class used to implement a task that can intercept packets and asynchronously return a result.
/// </summary>
/// <typeparam name="TResult">The result type of the task.</typeparam>
public abstract class InterceptorTask<TResult> : IMessageHandler
{
    private readonly SemaphoreSlim _executionSemaphore = new(1);
    private readonly TaskCompletionSource<TResult> _completion;

    private CancellationTokenRegistration? _disconnectRegistration;

    /// <summary>
    /// The interceptor that this task is bound to.
    /// </summary>
    protected IInterceptor Interceptor { get; }

    /// <summary>
    /// The incoming messages provided by the interceptor's message manager.
    /// </summary>
    protected Incoming In => Interceptor.Messages.In;

    /// <summary>
    /// The outgoing messages provided by the interceptor's message manager.
    /// </summary>
    protected Outgoing Out => Interceptor.Messages.Out;

    /// <summary>
    /// Creates a new interceptor task bound to the specified interceptor.
    /// </summary>
    /// <param name="interceptor">The interceptor to bind to.</param>
    protected InterceptorTask(IInterceptor interceptor)
    {
        Interceptor = interceptor;
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
    /// <exception cref="TimeoutException">If the task fails to complete within the specified timeout.</exception>
    public async Task<TResult> ExecuteAsync(int timeout, CancellationToken cancellationToken)
    {
        if (!_executionSemaphore.Wait(0, cancellationToken))
            throw new InvalidOperationException("The interceptor task has already been executed.");

        CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.Token.Register(() => SetCanceled());
        if (timeout > 0) cts.CancelAfter(timeout);

        _disconnectRegistration ??= Interceptor.DisconnectToken.Register(OnDisconnected);

        try
        {
            OnBind();

            await OnExecuteAsync().ConfigureAwait(false);
            return await _completion.Task;
        }
        catch (OperationCanceledException ex)
        when (!cancellationToken.IsCancellationRequested)
        {
            throw new TimeoutException($"The interceptor task '{GetType().FullName}' timed out.", ex);
        }
        finally
        {
            OnRelease();

            cts?.Dispose();
            _disconnectRegistration?.Unregister();

            _executionSemaphore.Dispose();
        }
    }

    /// <summary>
    /// Binds this task to the interceptor's dispatcher.
    /// </summary>
    protected virtual void OnBind() => Interceptor.Bind(this);

    /// <summary>
    /// Releases this task from the interceptor's dispatcher.
    /// </summary>
    protected virtual void OnRelease() => Interceptor.Release(this);

    /// <summary>
    /// Invoked when the connection ends while the interceptor task is in progress.
    /// </summary>
    protected virtual void OnDisconnected()
    {
        _completion.TrySetException(
            new Exception($"Disconnected while executing interceptor task '{GetType().FullName}'.")
        );
    }

    /// <summary>
    /// Invoked when the task is executed.
    /// </summary>
    protected abstract ValueTask OnExecuteAsync();

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
}
