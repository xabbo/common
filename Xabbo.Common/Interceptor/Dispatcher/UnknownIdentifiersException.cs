using System;
using System.Text;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher
{
    public class UnknownIdentifiersException : Exception
    {
        public Identifiers Identifiers { get; }

        public UnknownIdentifiersException(Identifiers identifiers)
            : base(BuildMessage(identifiers))
        {
            Identifiers = identifiers;
        }

        private static string BuildMessage(Identifiers identifiers)
        {
            var sb = new StringBuilder();

            sb.Append("Unknown identifiers. ");
            sb.Append(identifiers.ToString());

            return sb.ToString();
        }
    }
}
