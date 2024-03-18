using System;

namespace Xabbo.Messages;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public abstract class InterceptAttribute(Direction direction, string[] identifiers)
    : IdentifiersAttribute(direction, identifiers) { }

public sealed class InterceptInAttribute(params string[] identifiers)
    : InterceptAttribute(Direction.Incoming, identifiers) { }

public sealed class InterceptOutAttribute(params string[] identifiers)
    : InterceptAttribute(Direction.Outgoing, identifiers) { }
