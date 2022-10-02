using System;

using Xabbo.Messages;

namespace Xabbo.Interceptor;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class ReceiveAttribute : IdentifiersAttribute
{
    public ReceiveAttribute(params string[] identifiers)
        : base(Destination.Client, identifiers)
    { }
}
