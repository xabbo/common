using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Xabbo.Messages;

/// <summary>
/// Creates a new <see cref="MessageDispatcher"/> using the specified <see cref="IMessageManager"/>.
/// </summary>
/// <param name="messages"></param>
public sealed class MessageDispatcher(IMessageManager messages) : IMessageDispatcher
{
    private readonly struct Registration(IDisposable[] disposables) : IDisposable
    {
        public void Dispose()
        {
            foreach (var disposable in disposables)
                disposable.Dispose();
        }
    }

    private readonly ConcurrentDictionary<Header, DispatchList> _handlers = [];

    /// <summary>
    /// Gets the message manager used by this dispatcher.
    /// </summary>
    public IMessageManager Messages { get; } = messages;

    public IDisposable Register(InterceptGroup group)
    {
        var registrations = new List<IDisposable>(group.Count);
        foreach (var handler in group)
        {
            foreach (var header in handler.Headers)
            {
                registrations.Add(_handlers.GetOrAdd(header, (_) => new DispatchList()).Register(handler));
            }
        }
        return new Registration([.. registrations]);
    }

    /// <summary>
    /// Dispatches the specified intercept event to the registered intercept handlers.
    /// </summary>
    public void Dispatch(Intercept intercept)
    {
        if (_handlers.TryGetValue(Header.All, out var listenAll))
            listenAll.Dispatch(intercept);

        if (_handlers.TryGetValue(intercept.Packet.Header, out var list))
            list.Dispatch(intercept);
    }

    /// <summary>
    /// Releases all registered intercept handlers.
    /// </summary>
    public void Reset()
    {
        _handlers.Clear();
    }
}
