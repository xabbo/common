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
    public IEnumerable<Identifier> Unknown => this.Where(id => id.Direction == Direction.Unknown);
    /// <summary>
    /// Gets the incoming identifiers in this set.
    /// </summary>
    public IEnumerable<Identifier> Incoming => this.Where(id => id.Direction == Direction.Incoming);
    /// <summary>
    /// Gets the outgoing identifiers in this set.
    /// </summary>
    public IEnumerable<Identifier> Outgoing => this.Where(id => id.Direction == Direction.Outgoing);

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
                Add(new Identifier(Direction.Incoming, identifier));
        }

        if (outgoing != null)
        {
            foreach (string identifier in outgoing)
                Add(new Identifier(Direction.Outgoing, identifier));
        }
    }

    /// <summary>
    /// Adds a new identifier with the specified direction and name to this set.
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="name"></param>
    public void Add(Direction direction, string name) => Add(new Identifier(direction, name));

    /// <summary>
    /// Adds a range of new identifiers with the specified direction and names to this set.
    /// </summary>
    public void Add(Direction direction, params string[] names)
    {
        foreach (string name in names)
            Add(direction, name);
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
