using System;
using System.Collections.Immutable;

namespace Xabbo;

/// <summary>
/// Indicates that this class or method intercepts messages for the specified clients.
/// If no clients are specified, then all clients are targeted.
/// </summary>
/// <remarks>
/// When attached to a method, and an <see cref="InterceptInAttribute"/> or
/// <see cref="InterceptOutAttribute"/> is also present, this attribute specifies
/// which clients the intercept handler should target. If no directional intercept
/// attributes are specified, then the method is considered to be a message handler,
/// and must have one of the following function signatures:
/// <list type="bullet">
/// <item>void (<see cref="Intercept{T}"/>)</item>
/// <item>void (<see cref="Intercept"/>, <see cref="Messages.IMessage{T}"/>)</item>
/// <item>void (<see cref="Messages.IMessage{T}"/>)</item>
/// <item><see cref="Messages.IMessage"/>? (<see cref="Messages.IMessage{T}"/>)</item>
/// </list>
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class InterceptAttribute(ClientType client) : Attribute
{
    public ClientType Client { get; } = client;
    public InterceptAttribute() : this(ClientType.All) { }
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
