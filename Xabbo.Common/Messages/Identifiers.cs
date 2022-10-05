using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Represents a set of <see cref="Identifier"/>s.
/// </summary>
public sealed class Identifiers : HashSet<Identifier>
{
    /// <summary>
    /// Gets the identifiers in this set with an unknown destination.
    /// </summary>
    public IEnumerable<Identifier> Unknown => this.Where(id => id.Destination == Destination.Unknown);
    /// <summary>
    /// Gets the incoming identifiers in this set.
    /// </summary>
    public IEnumerable<Identifier> Incoming => this.Where(id => id.Destination == Destination.Client);
    /// <summary>
    /// Gets the outgoing identifiers in this set.
    /// </summary>
    public IEnumerable<Identifier> Outgoing => this.Where(id => id.Destination == Destination.Server);

    /// <summary>
    /// Creates an empty identifier set.
    /// </summary>
    public Identifiers() { }

    /// <summary>
    /// Creates an identifier set from the specified identifiers.
    /// </summary>
    public Identifiers(IEnumerable<Identifier> identifiers)
        : base(identifiers)
    { }

    /// <summary>
    /// Creates an identifier set from the specified incoming and outgoing names.
    /// </summary>
    /// <param name="incoming">The names of the incoming identifiers.</param>
    /// <param name="outgoing">The names of the outgoing identifiers.</param>
    public Identifiers(string[]? incoming = null, string[]? outgoing = null)
    {
        if (incoming != null)
        {
            foreach (string identifier in incoming)
                Add(new Identifier(Destination.Client, identifier));
        }

        if (outgoing != null)
        {
            foreach (string identifier in outgoing)
                Add(new Identifier(Destination.Server, identifier));
        }
    }

    /// <summary>
    /// Adds a new identifier with the specified destination and name to this set.
    /// </summary>
    /// <param name="destination"></param>
    /// <param name="name"></param>
    public void Add(Destination destination, string name) => Add(new Identifier(destination, name));

    /// <summary>
    /// Adds a range of new identifiers with the specified destination and names to this set.
    /// </summary>
    public void Add(Destination destination, params string[] names)
    {
        foreach (string name in names)
            Add(destination, name);
    }

    /// <summary>
    /// Gets a string representation of this identifier set.
    /// </summary>
    public override string ToString()
    {
        var sb = new StringBuilder();

        if (Unknown.Any())
            sb.Append(string.Join(", ", Unknown.Select(x => x.Name)));

        if (Incoming.Any())
        {
            if (sb.Length > 0) sb.Append("; ");
            sb.Append("Incoming: ");
            sb.Append(string.Join(", ", Incoming.Select(x => x.Name)));
        }

        if (Outgoing.Any())
        {
            if (sb.Length > 0) sb.Append("; ");
            sb.Append("Outgoing: ");
            sb.Append(string.Join(", ", Outgoing.Select(x => x.Name)));
        }

        return sb.ToString();
    }
}
