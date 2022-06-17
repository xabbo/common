using System;
using System.Linq;

namespace Xabbo.Messages.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ReceiveAttribute : IdentifiersAttribute
    {
        public ReceiveAttribute(params string[] identifiers)
            : base(Destination.Client, identifiers)
        { }
    }
}
