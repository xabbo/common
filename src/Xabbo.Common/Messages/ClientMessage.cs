using System;

namespace Xabbo.Messages;

/// <summary>
/// Defines information about a client message.
/// </summary>
public readonly record struct ClientMessage(ClientType Client, Direction Direction, short Header, string Name)
{
    public override int GetHashCode() => (Client, Direction, Header, Name.ToUpperInvariant()).GetHashCode();

    public bool Equals(ClientMessage other) =>
        Client == other.Client &&
        Direction == other.Direction &&
        Header == other.Header &&
        string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);

    public static implicit operator ClientMessage((ClientType client, Direction dir, short header, string name) x)
        => new(x.client, x.dir, x.header, x.name);
}


