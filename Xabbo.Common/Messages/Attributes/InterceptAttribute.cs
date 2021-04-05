using System;

namespace Xabbo.Messages
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InterceptAttribute : IdentifiersAttribute
    {
        internal InterceptAttribute(Destination destination, string[] identifiers)
            : base(destination, identifiers)
        { }
    }
}
