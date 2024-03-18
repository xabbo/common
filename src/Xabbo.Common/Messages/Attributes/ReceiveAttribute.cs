using System;

namespace Xabbo.Messages;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class ReceiveAttribute(params string[] identifiers)
    : IdentifiersAttribute(Direction.Incoming, identifiers) { }
