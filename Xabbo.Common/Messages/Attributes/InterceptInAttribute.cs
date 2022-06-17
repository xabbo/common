using System;

namespace Xabbo.Messages.Attributes
{
    public sealed class InterceptInAttribute : InterceptAttribute
    {
        public InterceptInAttribute(params string[] identifiers)
            : base(Destination.Client, identifiers)
        { }
    }
}
