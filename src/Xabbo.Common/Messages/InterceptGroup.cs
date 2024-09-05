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
    public InterceptGroup(ReadOnlySpan<InterceptHandler> handlers)
    {
        _handlers = [.. handlers];
    }

    /// <summary>
    /// Gets or sets whether this is a persistent intercept group.
    /// Persistent intercept groups can be attached before a connection is established,
    /// and will be re-attached each time the game connects.
    /// </summary>
    public bool Persistent { get; set; }

    public void Add(InterceptHandler handler) => _handlers.Add(handler);
    public void Add(ReadOnlySpan<Header> headers, InterceptCallback callback) => Add(new(headers, callback));

    public IEnumerator<InterceptHandler> GetEnumerator() => _handlers.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
