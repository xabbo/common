using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Xabbo.Messages;

public sealed class AmbiguousIdentifierException(Identifier identifier, IEnumerable<MessageNames> names)
    : Exception(BuildMessage(identifier, names))
{
    public Identifier Identifier { get; } = identifier;
    public ImmutableArray<MessageNames> Names { get; } = names.ToImmutableArray();

    static string BuildMessage(Identifier identifier, IEnumerable<MessageNames> sets)
    {
        var sb = new StringBuilder();

        sb.Append($"The identifier \"{identifier}\" is ambiguous between the following message sets: ");

        bool first = true;
        foreach (var set in sets)
        {
            if (!first)
                sb.Append(", ");
            first = false;
            sb.Append('{');
            sb.Append(set.ToString());
            sb.Append('}');
        }
        sb.Append('.');

        return sb.ToString();
    }
}