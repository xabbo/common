using System;

namespace Xabbo.Messages;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class ReceiveAttribute : IdentifiersAttribute
{
    public ReceiveAttribute(params string[] identifiers)
        : base(Direction.Incoming, identifiers)
    { }
}
