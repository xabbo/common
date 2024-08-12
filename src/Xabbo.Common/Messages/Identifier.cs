using System;
using System.Security.Cryptography;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Represents a client type, direction and message name.
/// </summary>
public readonly record struct Identifier(Clients Client, Direction Direction, string Name)
{
    public Identifier()
        : this(Clients.None, Direction.None, "")
    { }

    public override int GetHashCode() => (Client, Direction, Name.ToUpperInvariant()).GetHashCode();

    public bool Equals(Identifier other) =>
        Client == other.Client &&
        Direction == other.Direction &&
        string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);

    public string ToString(bool includeDirection)
    {
        string result = "";
        if (includeDirection && Direction != Direction.None)
            result += Direction.Short() + ":";
        if (Client != Clients.None)
            result += Client.Short() + ":";
        return result + Name;
    }

    public override string ToString() => ToString(false);

    public static implicit operator Identifier((Direction direction, string name) x) => new(Clients.None, x.direction, x.name);
    public static implicit operator Identifier((Clients client, Direction direction, string name) x) => new(x.client, x.direction, x.name);
}
