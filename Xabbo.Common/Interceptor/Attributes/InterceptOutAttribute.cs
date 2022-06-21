using Xabbo.Messages;

namespace Xabbo.Interceptor.Attributes
{
    public sealed class InterceptOutAttribute : InterceptAttribute
    {
        public InterceptOutAttribute(params string[] identifiers)
            : base(Destination.Server, identifiers)
        { }
    }
}
