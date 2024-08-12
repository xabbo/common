using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks;

/// <summary>
/// A task that captures the first intercepted message with a specified header.
/// </summary>
/// <param name="interceptor">The interceptor to bind to.</param>
/// <param name="headers">The headers to listen for.</param>
/// <param name="block">Whether to block the captured packet.</param>
/// <param name="shouldCapture">A callback that may inspect an intercepted packet and return whether or not it should be captured.</param>
public sealed class CaptureMessageTask(IInterceptor interceptor,
    IEnumerable<Header> headers, bool block = false,
    Func<IReadOnlyPacket, bool>? shouldCapture = null) : InterceptorTask<IPacket>(interceptor)
{
    private readonly bool _block = block;
    private readonly Header[] _headers = headers.ToArray();
    private readonly Func<IReadOnlyPacket, bool>? _shouldCapture = shouldCapture;
    private IDisposable? _registration;

    protected override void OnAttach()
        => _registration ??= Interceptor.Intercept(_headers, OnIntercept);

    protected override void OnRelease()
        => _registration?.Dispose();

    protected override ValueTask OnExecuteAsync() => ValueTask.CompletedTask;

    private void OnIntercept(Intercept e)
    {
        try
        {
            if (_shouldCapture?.Invoke(e.Packet) == false)
                return;

            if (SetResult(e.Packet.Copy()))
            {
                if (_block)
                    e.Block();
            }
        }
        catch (Exception ex) { SetException(ex); }
    }
}
