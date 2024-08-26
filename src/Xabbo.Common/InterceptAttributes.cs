using System;
using System.Collections.Immutable;

namespace Xabbo;

/// <summary>
/// Indicates that this class or method intercepts messages for the specified clients.
/// If no clients are specified, all clients are targeted.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class InterceptsAttribute(ClientType client) : Attribute
{
    public ClientType Client { get; } = client;
    public InterceptsAttribute() : this(ClientType.All) { }
}

/// <summary>
/// Indicates that this method intercepts the specified incoming messages.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class InterceptInAttribute(params string[] identifiers) : Attribute
{
    public ImmutableArray<string> Identifiers { get; } = [.. identifiers];
}

/// <summary>
/// Indicates that this method intercepts the specified outgoing messages.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class InterceptOutAttribute(params string[] identifiers) : Attribute
{
    public ImmutableArray<string> Identifiers { get; } = [.. identifiers];
}
