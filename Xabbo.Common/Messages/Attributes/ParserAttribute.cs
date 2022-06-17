using System;
using System.Collections.Generic;

namespace Xabbo.Messages.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ParserAttribute : Attribute
    {
        public HashSet<string> Messages { get; }

        public ParserAttribute(params string[] messages)
        {
            Messages = new HashSet<string>(messages, StringComparer.OrdinalIgnoreCase);
        }
    }
}
