using System;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class RequiredOutAttribute : IdentifiersAttribute
    {
        public RequiredOutAttribute(params string[] identifiers)
          : base(Destination.Server, identifiers)
        { }
    }
}
