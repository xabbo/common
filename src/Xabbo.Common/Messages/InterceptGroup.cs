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

    /// <summary>
    /// Constructs a new empty intercept group.
    /// </summary>
    public InterceptGroup() { }

    /// <summary>
    /// Constructs a new intercept group with the specified handlers.
    /// </summary>
    public InterceptGroup(ReadOnlySpan<InterceptHandler> handlers)
    {
        _handlers = [.. handlers];
    }

    /// <summary>
    /// Whether this is a persistent intercept group.
    /// </summary>
    /// <remarks>
    /// Persistent intercept groups can be attached before a connection is established
    /// when message information is not yet available. They will be persisted by a message
    /// dispatcher and re-attached each time the game connects, until the registration
    /// returned by <see cref="IMessageDispatcher.Register(InterceptGroup)"/> is disposed of.
    /// </remarks>
    public bool Persistent { get; set; }

    /// <summary>
    /// Adds the specified handler to the intercept group.
    /// </summary>
    public void Add(InterceptHandler handler) => _handlers.Add(handler);

    IEnumerator<InterceptHandler> IEnumerable<InterceptHandler>.GetEnumerator() => _handlers.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<InterceptHandler>)this).GetEnumerator();
}
