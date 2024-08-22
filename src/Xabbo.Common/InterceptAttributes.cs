using System;
using System.Collections.Immutable;

namespace Xabbo;

/// <summary>
/// Indicates that this class intercepts messages.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class InterceptsAttribute : Attribute;

/// <summary>
/// Indicates that this method intercepts on the specified clients.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class InterceptsOnAttribute(ClientType clients) : Attribute
{
    public ClientType Clients { get; } = clients;
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
