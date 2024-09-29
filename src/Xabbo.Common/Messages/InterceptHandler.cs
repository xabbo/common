using System;

namespace Xabbo.Messages;

/// <summary>
/// Associates a set of <see cref="Identifier"/> and/or <see cref="Header"/> with an <see cref="InterceptCallback"/>.
/// </summary>
public sealed class InterceptHandler(ReadOnlySpan<Header> headers, ReadOnlySpan<Identifier> identifiers, InterceptCallback callback)
{
    private readonly Header[] _headers = headers.ToArray();
    private readonly Identifier[] _identifiers = identifiers.ToArray();

    /// <summary>
    /// Constructs a new intercept handler with the specified headers and callback.
    /// </summary>
    /// <param name="headers">The headers to intercept.</param>
    /// <param name="callback">The intercept handler callback.</param>
    public InterceptHandler(ReadOnlySpan<Header> headers, InterceptCallback callback) : this(headers, [], callback) { }

    /// <summary>
    /// Constructs a new intercept handler with the specified identifiers and callback.
    /// </summary>
    /// <param name="identifiers">The identifiers to intercept.</param>
    /// <param name="callback">The intercept handler callback.</param>
    public InterceptHandler(ReadOnlySpan<Identifier> identifiers, InterceptCallback callback) : this([], identifiers, callback) { }

    /// <summary>
    /// Specifies which clients this handler should be registered for.
    /// The handler will only be attached on the specified client sessions.
    /// </summary>
    public ClientType Target { get; init; } = ClientType.All;

    /// <summary>
    /// Whether to use client-targeted identifiers.
    /// </summary>
    /// <remarks>
    /// Targeted identifiers will only intercept messages for the their specified client.
    /// For example, a Flash identifier will only intercept messages for the Flash client.
    /// </remarks>
    public bool UseTargetedIdentifiers { get; set; }

    /// <summary>
    /// Gets the target headers.
    /// </summary>
    public ReadOnlySpan<Header> Headers => _headers;

    /// <summary>
    /// Gets the target identifiers.
    /// </summary>
    public ReadOnlySpan<Identifier> Identifiers => _identifiers;

    /// <summary>
    /// Gets the intercept handler callback.
    /// </summary>
    public InterceptCallback Callback => callback;
}
