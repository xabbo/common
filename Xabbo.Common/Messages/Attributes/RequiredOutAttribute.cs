using System;

namespace Xabbo.Messages
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequiredOutAttribute : IdentifiersAttribute
    {
        public RequiredOutAttribute(params string[] identifiers)
          : base(Destination.Server, identifiers)
        { }
    }
}
