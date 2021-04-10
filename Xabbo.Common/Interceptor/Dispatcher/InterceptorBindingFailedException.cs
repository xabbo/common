using System;
using System.Text;

using Xabbo.Messages;

namespace Xabbo.Interceptor.Dispatcher
{
    public class InterceptorBindingFailedException : Exception
    {
        public object Target { get; }
        public Identifiers UnknownIdentifiers { get; }
        public Identifiers UnresolvedIdentifiers { get; }

        public InterceptorBindingFailedException(
            object target,
            Identifiers unknownIdentifiers,
            Identifiers unresolvedIdentifiers)
            : base(BuildMessage(target, unknownIdentifiers, unresolvedIdentifiers))
        {
            Target = target;
            UnknownIdentifiers = unknownIdentifiers;
            UnresolvedIdentifiers = unresolvedIdentifiers;
        }

        private static string BuildMessage(object target,
            Identifiers unknownIdentifiers,
            Identifiers unresolvedIdentifiers)
        {
            var sb = new StringBuilder();
            sb.Append($"Failed to bind to target '{target.GetType().FullName}'.");

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
