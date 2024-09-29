using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Represents a set of <see cref="Identifier"/>.
/// </summary>
public sealed class Identifiers : HashSet<Identifier>
{
    /// <summary>
    /// Gets the unknown identifiers in this set.
    /// </summary>
    public IEnumerable<Identifier> Unknown => this.Where(id => id.Direction == Direction.None);

    /// <summary>
    /// Gets the incoming identifiers in this set.
    /// </summary>
    public IEnumerable<Identifier> Incoming => this.Where(id => id.Direction == Direction.In);

    /// <summary>
    /// Gets the outgoing identifiers in this set.
    /// </summary>
    public IEnumerable<Identifier> Outgoing => this.Where(id => id.Direction == Direction.Out);

    /// <summary>
    /// Creates a new empty identifier set.
    /// </summary>
    public Identifiers() { }

    /// <summary>
    /// Creates an identifier set with the specified identifiers.
    /// </summary>
    public Identifiers(IEnumerable<Identifier> identifiers)
        : base(identifiers)
    { }

    /// <summary>
    /// Adds a new identifier with the specified direction and name to this set.
    /// </summary>
    public void Add(ClientType client, Direction direction, string name) => Add(new Identifier(client, direction, name));

    /// <summary>
    /// Adds a range of new identifiers with the specified direction and names to this set.
    /// </summary>
    public void Add(ClientType client, Direction direction, params string[] names)
    {
        foreach (string name in names)
            Add(client, direction, name);
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
