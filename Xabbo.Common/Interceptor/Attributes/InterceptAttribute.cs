using System;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class InterceptAttribute : IdentifiersAttribute
{
    internal InterceptAttribute(Destination destination, string[] identifiers)
        : base(destination, identifiers)
    { }
}
