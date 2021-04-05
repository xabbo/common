using System;

namespace Xabbo.Messages
{
    public sealed class InterceptOutAttribute : InterceptAttribute
    {
        public InterceptOutAttribute(params string[] identifiers)
            : base(Destination.Server, identifiers)
        { }
    }
}
