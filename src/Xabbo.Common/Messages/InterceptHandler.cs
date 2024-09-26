using System;

namespace Xabbo.Messages;

/// <summary>
/// Associates a set of <see cref="Identifier"/>s and/or <see cref="Header"/>s with an <see cref="InterceptCallback"/>.
/// </summary>
public sealed class InterceptHandler(ReadOnlySpan<Header> headers, ReadOnlySpan<Identifier> identifiers, InterceptCallback callback)
{
    public InterceptHandler(ReadOnlySpan<Header> headers, InterceptCallback callback) : this(headers, [], callback) { }
    public InterceptHandler(ReadOnlySpan<Identifier> identifiers, InterceptCallback callback) : this([], identifiers, callback) { }

    /// <summary>
    /// Specifies which clients this handler should be registered for.
    /// The handler will only be attached on the specified client sessions.
    /// </summary>
    public ClientType Target { get; init; } = ClientType.All;

    public bool UseTargetedIdentifiers { get; set; }

    private readonly Header[] _headers = headers.ToArray();
    private readonly Identifier[] _identifiers = identifiers.ToArray();
    public ReadOnlySpan<Header> Headers => _headers;
    public ReadOnlySpan<Identifier> Identifiers => _identifiers;
    public InterceptCallback Callback => callback;
}
