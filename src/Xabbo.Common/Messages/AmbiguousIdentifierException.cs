using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Thrown when an identifier resolves to multiple message name sets.
/// </summary>
/// <param name="identifier">The ambiguous identifier.</param>
/// <param name="names">The set of message names resolved by the identifier.</param>
/// <remarks>
/// This can happen when a non-targeted identifier (with <see cref="ClientType.None"/> as its client type)
/// resolves to multiple sets of identifiers such as
/// (<see cref="ClientType.Shockwave"/>/"Objects") and (<see cref="ClientType.Flash"/>/"Objects")
/// which are considered to be different messages.
/// </remarks>
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