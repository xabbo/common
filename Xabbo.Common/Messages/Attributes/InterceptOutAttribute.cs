using System;

namespace Xabbo.Messages.Attributes
{
    public sealed class InterceptOutAttribute : InterceptAttribute
    {
        public InterceptOutAttribute(params string[] identifiers)
            : base(Destination.Server, identifiers)
        { }
    }
}
