using Xabbo.Messages;

namespace Xabbo.Interceptor.Attributes;

public sealed class InterceptInAttribute : InterceptAttribute
{
    public InterceptInAttribute(params string[] identifiers)
        : base(Destination.Client, identifiers)
    { }
}
