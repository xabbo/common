using System;
using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks;

/// <summary>
/// A base class used to implement an asynchronous task that intercepts packets returns a result.
/// </summary>
/// <typeparam name="TResult">The result type of the task.</typeparam>
public abstract class InterceptorTask<TResult>(IInterceptor interceptor)
{
    private IDisposable? _attachment;
    private readonly SemaphoreSlim _executeOnce = new(1);
    private readonly TaskCompletionSource<TResult> _completion
        = new(TaskCreationOptions.RunContinuationsAsynchronously);

    private CancellationTokenRegistration? _disconnectRegistration;

    /// <summary>
    /// Gets the clients supported by this interceptor task.
    /// </summary>
    protected virtual ClientType SupportedClients => ClientType.All;

    /// <summary>
    /// The interceptor that this task is attached to.
    /// </summary>
    protected IInterceptor Interceptor { get; } = interceptor;

    /// <summary>
    /// Executes the task synchronously and returns the result.
    /// </summary>
    /// <param name="timeoutMs">The maximum number of milliseconds to wait for a result. Use <c>-1</c> for no timeout.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the task.</param>
    public TResult Execute(int timeoutMs, CancellationToken cancellationToken) => ExecuteAsync(timeoutMs, cancellationToken).GetAwaiter().GetResult();

    /// <summary>
    /// Executes the task synchronously and returns the result.
    /// </summary>
    /// <param name="timeout">The maximum time to wait for a result. Use <see cref="TimeSpan.Zero"/> for no timeout.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the task.</param>
    public TResult Execute(TimeSpan timeout, CancellationToken cancellationToken) => ExecuteAsync(timeout, cancellationToken).GetAwaiter().GetResult();

    /// <summary>
    /// Executes the task asynchronously and returns the result.
    /// </summary>
    /// <param name="timeoutMs">The maximum time to wait for a result in milliseconds. Use <c>-1</c> for no timeout.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the task.</param>
    /// <exception cref="TimeoutException">If the task fails to complete within the specified timeout.</exception>
    public Task<TResult> ExecuteAsync(int timeoutMs = 10000, CancellationToken cancellationToken = default)
        => ExecuteAsync(TimeSpan.FromMilliseconds(timeoutMs), cancellationToken);

    /// <summary>
    /// Executes the task asynchronously and returns the result.
    /// </summary>
    /// <param name="timeout">The maximum time to wait for a result. Use <see cref="TimeSpan.Zero"/>  for no timeout.</param>
    /// <param name="cancellationToken">The cancellation token that can be used to cancel the task.</param>
    /// <exception cref="TimeoutException">If the task fails to complete within the specified timeout.</exception>
    public async Task<TResult> ExecuteAsync(TimeSpan timeout, CancellationToken cancellationToken = default)
    {
        UnsupportedClientException.ThrowIf(Interceptor.Session.Client.Type, ~SupportedClients);

        if (!_executeOnce.Wait(0, cancellationToken))
            throw new InvalidOperationException("The interceptor task has already been executed.");

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.Token.Register(() => SetCanceled());
        if (timeout > TimeSpan.Zero) cts.CancelAfter(timeout);

        try
        {
            _disconnectRegistration = Interceptor.DisconnectToken.Register(OnDisconnected);

            _attachment = OnAttach();
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
            _disconnectRegistration?.Unregister();
            _executeOnce.Dispose();
        }
    }

    /// <summary>
    /// Attaches this task to the interceptor's dispatcher.
    /// The default implementation calls <see cref="IMessageHandler.Attach(IInterceptor)"/> if this task implements <see cref="IMessageHandler"/> .
    /// </summary>
    protected virtual IDisposable? OnAttach()
    {
        if (this is IMessageHandler handler)
            return handler.Attach(Interceptor);
        else
            return null;
    }

    /// <summary>
    /// Detaches this task from the interceptor's dispatcher.
    /// The default implementation calls <see cref="IDisposable.Dispose"/> on the <see cref="IDisposable"/> returned by <see cref="OnAttach"/> .
    /// </summary>
    protected virtual void OnRelease() => _attachment?.Dispose();

    /// <summary>
    /// Invoked when the connection ends while the interceptor task is in progress.
    /// </summary>
    protected virtual void OnDisconnected() => SetException(new Exception($"Disconnected while executing interceptor task '{GetType().FullName}'."));

    /// <summary>
    /// Invoked when the task is executed.
    /// The default implementation synchronously calls <see cref="OnExecute"/>.
    /// </summary>
    protected virtual ValueTask OnExecuteAsync()
    {
        OnExecute();
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Invoked when the task is executed.
    /// </summary>
    protected virtual void OnExecute() { }

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
