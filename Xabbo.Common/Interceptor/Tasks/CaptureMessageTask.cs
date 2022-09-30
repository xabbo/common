using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Tasks;

/// <summary>
/// Captures the first message with a matching target header.
/// </summary>
public sealed class CaptureMessageTask : InterceptorTask<IPacket>
{
    private readonly bool _block;
    private readonly Header[] _headers;

    /// <summary>
    /// Creates a new <see cref="CaptureMessageTask"/> targeting the specified headers.
    /// </summary>
    /// <param name="interceptor">The interceptor to bind to.</param>
    /// <param name="headers">The headers to listen for.</param>
    /// <param name="block">Whether to block the captured packet.</param>
    public CaptureMessageTask(IInterceptor interceptor,
        IEnumerable<Header> headers, bool block = false)
        : base(interceptor)
    {
        _block = block;

        if (headers is Header[] array)
        {
            _headers = array;
        }
        else
        {
            _headers = headers.ToArray();
        }
    }

    protected override void Bind()
    {
        foreach (Header header in _headers)
            Interceptor.Dispatcher.AddIntercept(header, OnIntercept, Interceptor.Client);
    }

    protected override void Release()
    {
        foreach (Header header in _headers)
            Interceptor.Dispatcher.RemoveIntercept(header, OnIntercept);
    }

    protected override ValueTask OnExecuteAsync() => ValueTask.CompletedTask;

    private void OnIntercept(InterceptArgs e)
    {
        try
        {
            if (SetResult(e.Packet.Copy()))
            {
                if (_block)
                    e.Block();
            }
        }
        catch (Exception ex) { SetException(ex); }
    }
}
