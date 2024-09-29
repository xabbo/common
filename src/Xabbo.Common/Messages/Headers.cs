using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Represents a set of <see cref="Header"/>.
/// </summary>
public sealed class Headers : HashSet<Header>
{
    /// <summary>
    /// Gets the unknown identifiers in this set.
    /// </summary>
    public IEnumerable<Header> Unknown => this.Where(id => id.Direction == Direction.None);

    /// <summary>
    /// Gets the incoming identifiers in this set.
    /// </summary>
    public IEnumerable<Header> Incoming => this.Where(id => id.Direction == Direction.In);

    /// <summary>
    /// Gets the outgoing identifiers in this set.
    /// </summary>
    public IEnumerable<Header> Outgoing => this.Where(id => id.Direction == Direction.Out);

    /// <summary>
    /// Creates a new empty header set.
    /// </summary>
    public Headers() { }

    /// <summary>
    /// Creates a header set with the specified headers.
    /// </summary>
    public Headers(IEnumerable<Header> headers)
        : base(headers)
    { }

    /// <summary>
    /// Gets a string representation of this header set.
    /// </summary>
    public override string ToString()
    {
        var sb = new StringBuilder();

        if (Unknown.Any())
            sb.Append(string.Join(", ", Unknown.Select(x => x.Value)));

        if (Incoming.Any())
        {
            if (sb.Length > 0) sb.Append("; ");
            sb.Append("Incoming: ");
            sb.Append(string.Join(", ", Incoming.Select(x => x.Value)));
        }

        if (Outgoing.Any())
        {
            if (sb.Length > 0) sb.Append("; ");
            sb.Append("Outgoing: ");
            sb.Append(string.Join(", ", Outgoing.Select(x => x.Value)));
        }

        return sb.ToString();
    }
}
