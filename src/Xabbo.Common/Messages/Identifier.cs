﻿using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents a client type, direction, and message name.
/// </summary>
/// <remarks>
/// Used to associate a message name with a <see cref="Header"/> that is resolved at runtime.
/// </remarks>
public readonly record struct Identifier(ClientType Client, Direction Direction, string Name)
{
    public static readonly Identifier Unknown = new();

    public Identifier()
        : this(ClientType.None, Direction.None, "")
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
            result += Direction.ToShortString() + ":";
        if (Client != ClientType.None)
            result += Client.ToShortString() + ":";
        return result + Name;
    }

    public override string ToString() => ToString(false);

    public static implicit operator Identifier((Direction direction, string name) x) => new(ClientType.None, x.direction, x.name);
    public static implicit operator Identifier((ClientType client, Direction direction, string name) x) => new(x.client, x.direction, x.name);

    public static implicit operator ReadOnlySpan<Identifier>(in Identifier identifier) => new(in identifier);
}
