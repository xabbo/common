using System;
using System.Text;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher
{
    public class InterceptorBindingFailedException : Exception
    {
        public IInterceptHandler Handler { get; }
        public Identifiers UnknownIdentifiers { get; }
        public Identifiers UnresolvedIdentifiers { get; }

        public InterceptorBindingFailedException(
            IInterceptHandler handler,
            Identifiers unknownIdentifiers,
            Identifiers unresolvedIdentifiers)
            : base(BuildMessage(handler, unknownIdentifiers, unresolvedIdentifiers))
        {
            Handler = handler;
            UnknownIdentifiers = unknownIdentifiers;
            UnresolvedIdentifiers = unresolvedIdentifiers;
        }

        private static string BuildMessage(IInterceptHandler handler,
            Identifiers unknownIdentifiers,
            Identifiers unresolvedIdentifiers)
        {
            var sb = new StringBuilder();
            sb.Append($"Failed to bind to target '{handler.GetType().FullName}'.");

            if (unknownIdentifiers.Count > 0)
            {
                sb.Append(" Unknown identifiers - ");
                sb.Append(unknownIdentifiers.ToString());
                sb.Append('.');
            }

            if (unresolvedIdentifiers.Count > 0)
            {
                sb.Append(" Unresolved identifiers - ");
                sb.Append(unresolvedIdentifiers.ToString());
                sb.Append('.');
            }

            return sb.ToString();
        }
    }
}
