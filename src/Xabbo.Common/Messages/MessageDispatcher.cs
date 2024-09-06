using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

using Xabbo.Interceptor;

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
    private ClientType _currentClient = ClientType.None;

    /// <summary>
    /// Gets the interceptor associated with this dispatcher.
    /// </summary>
    public IInterceptor Interceptor { get; }

    /// <summary>
    /// Gets the message manager used by this dispatcher.
    /// </summary>
    public IMessageManager Messages => Interceptor.Messages;

    public MessageDispatcher(IInterceptor interceptor)
    {
        Interceptor = interceptor;
        Interceptor.Connected += OnGameConnected;
        Interceptor.Disconnected += OnGameDisconnected;
    }

    private void OnGameConnected(GameConnectedArgs e)
    {
        _lock.Wait();
        try
        {
            _currentClient = e.Session.Client.Type;
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

    private void OnGameDisconnected()
    {
        _lock.Wait();
        try
        {
            _currentClient = ClientType.None;
            _handlers.Clear();
        }
        finally
        {
            _lock.Release();
        }
    }

    public void Dispose()
    {
        Interceptor.Connected -= OnGameConnected;
    }

    private DispatchList GetDispatchList(Header header) => _handlers.GetOrAdd(header, _ => new DispatchList());

    public IDisposable Register(InterceptGroup group)
    {
        _lock.Wait();
        try
        {
            Transient? transient = null;
            if (_currentClient != ClientType.None || !group.Persistent)
                transient = Attach(group);

            if (!group.Persistent)
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
        if (_currentClient == ClientType.None)
            throw new InvalidOperationException("The game is not connected.");

        var transients = new List<IDisposable>(group.Count);
        var headerMap = new Dictionary<InterceptHandler, HashSet<Header>>();

        Identifiers? unresolved = null;

        // First pass makes sure identifiers are resolved where required.
        foreach (var handler in group)
        {
            // Skip if the current client is not targeted.
            if ((handler.Target & _currentClient) == ClientType.None)
                continue;
            var set = new Headers([.. handler.Headers]);
            foreach (var identifier in handler.Identifiers)
            {
                // Only attach identifiers for the current client if we are using targeted identifiers.
                if (handler.UseTargetedIdentifiers && identifier.Client != _currentClient)
                    continue;
                if (Messages.TryGetHeader(identifier, out Header header))
                    set.Add(header);
                else
                    (unresolved ??= []).Add(identifier);
            }
            headerMap[handler] = set;
        }

        if (unresolved is not null)
            throw new UnresolvedIdentifiersException(unresolved);

        foreach (var handler in group)
        {
            // If the handler does not exist here, the current client is not targeted.
            if (!headerMap.TryGetValue(handler, out var headers))
                continue;
            foreach (var header in headers)
                transients.Add(GetDispatchList(header).Register(handler));
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
