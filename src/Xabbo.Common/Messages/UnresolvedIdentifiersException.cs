using System;
using System.Linq;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Thrown when a set of <see cref="Identifier"/>s fail to resolve to their respective <see cref="Header"/>s.
/// </summary>
/// <param name="identifiers">The identifiers that failed to resolve.</param>
public sealed class UnresolvedIdentifiersException(Identifiers identifiers)
    : Exception(ConstructMessage(identifiers))
{
    public Identifiers Identifiers { get; } = identifiers;

    private static string ConstructMessage(Identifiers identifiers)
    {
        var sb = new StringBuilder("Unresolved identifiers");

        if (identifiers.Count > 0)
        {
            sb.Append(" (");

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
        }

        return sb.ToString();
    }
}
