using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Xabbo.Messages;

/// <summary>
/// Creates a new <see cref="MessageDispatcher"/> using the specified <see cref="IMessageManager"/>.
/// </summary>
public sealed class MessageDispatcher : IMessageDispatcher, IDisposable
{
    private sealed class Transient(IDisposable[] disposables) : IDisposable
    {
        public void Dispose()
        {
            foreach (var disposable in disposables)
                disposable.Dispose();
        }
    }

    private sealed class Persistent(MessageDispatcher dispatcher, InterceptGroup group) : IDisposable
    {
        public InterceptGroup Group { get; } = group;
        public void Dispose() => dispatcher.Unregister(this);
    }

    private readonly SemaphoreSlim _lock = new(1, 1);
    private readonly ConcurrentDictionary<Header, DispatchList> _handlers = [];
    private readonly Dictionary<Persistent, Transient?> _persistents = [];

    /// <summary>
    /// Gets the message manager used by this dispatcher.
    /// </summary>
    public IMessageManager Messages { get; }

    public MessageDispatcher(IMessageManager messages)
    {
        Messages = messages;
        Messages.Loaded += OnMessagesLoaded;
    }

    public void Dispose()
    {
        Messages.Loaded -= OnMessagesLoaded;
    }

    private DispatchList GetDispatchList(Header header) => _handlers.GetOrAdd(header, _ => new DispatchList());

    private void OnMessagesLoaded(object? sender, EventArgs e)
    {
        _lock.Wait();
        try
        {
            foreach (var (persistent, transient) in _persistents)
            {
                transient?.Dispose();
                _persistents[persistent] = Attach(persistent.Group);
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    public IDisposable Register(InterceptGroup group)
    {
        _lock.Wait();
        try
        {
            Transient? transient = null;
            if (Messages.Available || group.Transient)
                transient = Attach(group);

            if (group.Transient)
            {
                // Attach should throw an unresolved identifiers exception before we get here.
                if (transient is null)
                    throw new Exception("Failed to register transient intercept: messages unavailable.");
                return transient;
            }
            else
            {
                Persistent persistent = new(this, group);
                _persistents.Add(persistent, transient);
                return persistent;
            }
        }
        finally
        {
            _lock.Release();
        }
    }

    private void Unregister(Persistent persistent)
    {
        _lock.Wait();
        try
        {
            if (_persistents.Remove(persistent, out Transient? transient))
                transient?.Dispose();
        }
        finally
        {
            _lock.Release();
        }
    }

    private Transient Attach(InterceptGroup group)
    {
        var transients = new List<IDisposable>(group.Count);
        var headerMap = new Dictionary<InterceptHandler, HashSet<Header>>();

        // First pass ensures all identifiers are resolved to prevent partially attached intercept groups.
        foreach (var handler in group)
        {
            var set = new HashSet<Header>([.. handler.Headers ]);
            if (handler.Identifiers.Length > 0)
                set.UnionWith(Messages.Resolve(handler.Identifiers));
            headerMap[handler] = set;
        }

        foreach (var handler in group)
        {
            foreach (var header in headerMap[handler])
            {
                transients.Add(GetDispatchList(header).Register(handler));
            }
        }

        return new Transient([.. transients]);
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
