using System;

namespace Xabbo.Messages.Dispatcher;

internal sealed class Unsubscriber(IMessageDispatcher dispatcher, HeaderSet headers, Action<InterceptArgs> callback) : IDisposable
{
    private readonly IMessageDispatcher _dispatcher = dispatcher;
    private readonly HeaderSet _headers = headers;
    private readonly Action<InterceptArgs> _callback = callback;

    private bool _disposed;

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
