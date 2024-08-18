using System;
using System.Collections;
using System.Collections.Generic;

namespace Xabbo.Messages;

/// <summary>
/// Defines a set of related intercept handlers.
/// </summary>
public sealed class InterceptGroup : IReadOnlyList<InterceptHandler>
{
    private readonly List<InterceptHandler> _handlers = [];

    public InterceptHandler this[int index] => _handlers[index];
    public int Count => _handlers.Count;

    public InterceptGroup() { }
    public InterceptGroup(IEnumerable<InterceptHandler> handlers)
    {
        _handlers.AddRange(handlers);
    }

    public bool Transient { get; set; }
    public void Add(InterceptHandler handler) => _handlers.Add(handler);
    public void Add(ReadOnlySpan<Header> headers, Action<Intercept> callback) => Add(new(headers, callback));

    public IEnumerator<InterceptHandler> GetEnumerator() => _handlers.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

/// <summary>
/// Defines a set of headers with an intercept callback.
/// </summary>
public sealed class InterceptHandler(ReadOnlySpan<Header> headers, ReadOnlySpan<Identifier> identifiers, Action<Intercept> callback)
{
    public InterceptHandler(ReadOnlySpan<Header> headers, Action<Intercept> callback) : this(headers, [], callback) { }
    public InterceptHandler(ReadOnlySpan<Identifier> identifiers, Action<Intercept> callback) : this([], identifiers, callback) { }

    private readonly Header[] _headers = headers.ToArray();
    private readonly Identifier[] _identifiers = identifiers.ToArray();
    public ReadOnlySpan<Header> Headers => _headers;
    public ReadOnlySpan<Identifier> Identifiers => _identifiers;
    public Action<Intercept> Callback => callback;
}