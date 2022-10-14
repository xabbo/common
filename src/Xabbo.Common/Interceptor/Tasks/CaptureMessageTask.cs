using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks;

/// <summary>
/// A task that captures the first intercepted message with a specified header.
/// </summary>
public sealed class CaptureMessageTask : InterceptorTask<IPacket>
{
    private readonly bool _block;
    private readonly Header[] _headers;
    private readonly Func<IReadOnlyPacket, bool>? _shouldCapture;

    /// <summary>
    /// Constructs a new <see cref="CaptureMessageTask"/> targeting the specified headers.
    /// </summary>
    /// <param name="interceptor">The interceptor to bind to.</param>
    /// <param name="headers">The headers to listen for.</param>
    /// <param name="block">Whether to block the captured packet.</param>
    /// <param name="shouldCapture">A callback that may inspect an intercepted packet and return whether or not it should be captured.</param>
    public CaptureMessageTask(IInterceptor interceptor,
        IEnumerable<Header> headers, bool block = false,
        Func<IReadOnlyPacket, bool>? shouldCapture = null)
        : base(interceptor)
    {
        _block = block;
        _shouldCapture = shouldCapture;

        if (headers is Header[] array)
        {
            _headers = array;
        }
        else
        {
            _headers = headers.ToArray();
        }
    }

    protected override void OnBind()
    {
        foreach (Header header in _headers)
            Interceptor.Dispatcher.AddIntercept(header, OnIntercept, Interceptor.Client);
    }

    protected override void OnRelease()
    {
        foreach (Header header in _headers)
            Interceptor.Dispatcher.RemoveIntercept(header, OnIntercept);
    }

    protected override ValueTask OnExecuteAsync() => ValueTask.CompletedTask;

    private void OnIntercept(InterceptArgs e)
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
