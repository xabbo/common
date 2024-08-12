using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks;

/// <summary>
/// A base class used to implement a task that can intercept packets and asynchronously return a result.
/// </summary>
/// <typeparam name="TResult">The result type of the task.</typeparam>
/// <remarks>
/// Creates a new interceptor task bound to the specified interceptor.
/// </remarks>
/// <param name="interceptor">The interceptor to bind to.</param>
public abstract class InterceptorTask<TResult>(IInterceptor interceptor)
{
    private IDisposable? _attachment;
    private readonly SemaphoreSlim _executeOnce = new(1);
    private readonly TaskCompletionSource<TResult> _completion
        = new(TaskCreationOptions.RunContinuationsAsynchronously);

    private CancellationTokenRegistration? _disconnectRegistration;

    /// <summary>
    /// The interceptor that this task is bound to.
    /// </summary>
    protected IInterceptor Interceptor { get; } = interceptor;

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
        if (!_executeOnce.Wait(0, cancellationToken))
            throw new InvalidOperationException("The interceptor task has already been executed.");

        var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.Token.Register(() => SetCanceled());
        if (timeout > 0) cts.CancelAfter(timeout);

        _disconnectRegistration ??= Interceptor.DisconnectToken.Register(OnDisconnected);

        try
        {
            OnAttach();
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
            _executeOnce.Dispose();
        }
    }

    public virtual void Attach(IMessageDispatcher dispatcher) { }

    /// <summary>
    /// Attaches this task to the interceptor's dispatcher.
    /// </summary>
    protected virtual void OnAttach()
    {
        if (this is IMessageHandler handler)
            _attachment = handler.Attach(Interceptor);
    }

    /// <summary>
    /// Detaches this task from the interceptor's dispatcher.
    /// </summary>
    protected virtual void OnRelease() => _attachment?.Dispose();

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
