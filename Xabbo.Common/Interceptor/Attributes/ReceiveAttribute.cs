using System;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ReceiveAttribute : IdentifiersAttribute
    {
        public ReceiveAttribute(params string[] identifiers)
            : base(Destination.Client, identifiers)
        { }
    }
}
