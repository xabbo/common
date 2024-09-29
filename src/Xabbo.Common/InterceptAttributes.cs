using System;
using System.Collections.Immutable;

namespace Xabbo;

/// <summary>
/// Indicates that a class or method intercepts messages for the specified clients.
/// If no clients are specified, then all clients are targeted.
/// </summary>
/// <remarks>
/// When placed on a method, and an <see cref="InterceptInAttribute"/> or
/// <see cref="InterceptOutAttribute"/> is also present, this attribute specifies
/// which clients the intercept handler should target. If no directional intercept
/// attributes are specified, then the method is considered to be a message handler,
/// the attribute must not specify any client types, and the method must have
/// one of the following function signatures:
/// <list type="bullet">
/// <item>void (<see cref="Intercept{TMsg}"/>)</item>
/// <item>void (<see cref="Intercept"/>, <see cref="Messages.IMessage{TMsg}"/>)</item>
/// <item>void (<see cref="Messages.IMessage{TMsg}"/>)</item>
/// <item><see cref="Messages.IMessage"/>? (<see cref="Messages.IMessage{TMsg}"/>)</item>
/// </list>
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class InterceptAttribute(ClientType clients) : Attribute
{
    /// <summary>
    /// The target clients on which to intercept packets.
    /// </summary>
    public ClientType Clients { get; } = clients;

    /// <summary>
    /// Constructs a new intercept attribute targeting all clients.
    /// </summary>
    public InterceptAttribute() : this(ClientType.All) { }
}

/// <summary>
/// When placed on a method, indicates that the method intercepts the specified incoming messages.
/// </summary>
/// <param name="identifiers">The names of the identifiers to intercept.</param>
[AttributeUsage(AttributeTargets.Method)]
public sealed class InterceptInAttribute(params string[] identifiers) : Attribute
{
    /// <summary>
    /// The names of the identifiers to intercept.
    /// </summary>
    public ImmutableArray<string> Identifiers { get; } = [.. identifiers];
}

/// <summary>
/// When placed on a method, indicates that the method intercepts the specified outgoing messages.
/// </summary>
/// <param name="identifiers">The names of the identifiers to intercept.</param>
[AttributeUsage(AttributeTargets.Method)]
public sealed class InterceptOutAttribute(params string[] identifiers) : Attribute
{
    /// <summary>
    /// The names of the identifiers to intercept.
    /// </summary>
    public ImmutableArray<string> Identifiers { get; } = [.. identifiers];
}
