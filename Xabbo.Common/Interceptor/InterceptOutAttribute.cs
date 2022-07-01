using Xabbo.Messages;

namespace Xabbo.Interceptor;

public sealed class InterceptOutAttribute : InterceptAttribute
{
    public InterceptOutAttribute(params string[] identifiers)
        : base(Destination.Server, identifiers)
    { }
}
