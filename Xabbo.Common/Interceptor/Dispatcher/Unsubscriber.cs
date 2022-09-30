using System;
using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher;

internal sealed class Unsubscriber : IDisposable
{
    private readonly IInterceptDispatcher _dispatcher;
    private readonly HeaderSet _headers;
    private readonly Action<InterceptArgs> _callback;

    private bool _disposed;

    public Unsubscriber(IInterceptDispatcher dispatcher, HeaderSet headers, Action<InterceptArgs> callback)
    {
        _dispatcher = dispatcher;
        _headers = headers;
        _callback = callback;
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;

        if (disposing)
        {
            foreach (Header header in _headers)
            {
                _dispatcher.RemoveIntercept(header, _callback);
            }
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        Dispose(true);
    }
}
