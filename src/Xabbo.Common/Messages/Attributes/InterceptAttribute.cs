using System;

namespace Xabbo.Messages;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class InterceptAttribute : IdentifiersAttribute
{
    internal InterceptAttribute(Direction direction, string[] identifiers)
        : base(direction, identifiers)
    { }
}
