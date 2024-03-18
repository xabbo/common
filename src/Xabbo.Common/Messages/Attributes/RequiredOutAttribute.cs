using System;

namespace Xabbo.Messages;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
public sealed class RequiredOutAttribute(params string[] identifiers)
    : IdentifiersAttribute(Direction.Outgoing, identifiers) { }
