using Xabbo.Messages;

namespace Xabbo.Interceptor;

public sealed class InterceptInAttribute : InterceptAttribute
{
    public InterceptInAttribute(params string[] identifiers)
        : base(Destination.Client, identifiers)
    { }
}
