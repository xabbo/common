using System;
using System.Linq;
using System.Text;

namespace Xabbo.Messages;

public sealed class UnresolvedIdentifiersException(Identifiers identifiers)
    : Exception(ConstructMessage(identifiers))
{
    public Identifiers Identifiers { get; } = identifiers;

    private static string ConstructMessage(Identifiers identifiers)
    {
        var sb = new StringBuilder();
        sb.Append("Unresolved identifiers (");

        if (identifiers.Incoming.Any())
        {
            sb.Append("Incoming: ");
            sb.Append(string.Join(", ", identifiers.Incoming));
        }

        if (identifiers.Outgoing.Any())
        {
            if (identifiers.Incoming.Any())
                sb.Append("; ");
            sb.Append("Outgoing: ");
            sb.Append(string.Join(", ", identifiers.Outgoing));
        }

        sb.Append(')');

        return sb.ToString();
    }
}
