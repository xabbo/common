using System;

namespace Xabbo.Messages;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class RequiredInAttribute(params string[] identifiers)
    : IdentifiersAttribute(Direction.Incoming, identifiers) { }
