using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents a message name and direction.
/// </summary>
public sealed class Identifier
{
    /// <summary>
    /// Gets the destination of this message.
    /// </summary>
    public Destination Destination { get; }

    /// <summary>
    /// Gets whether this is an outgoing message.
    /// </summary>
    public bool IsOutgoing => Destination == Destination.Server;

    /// <summary>
    /// Gets whether this is an incoming message.
    /// </summary>
    public bool IsIncoming => Destination == Destination.Client;

    /// <summary>
    /// Gets the name of this message.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Constructs a new message <see cref="Identifier"/> with the specified name and destination.
    /// </summary>
    /// <param name="destination">The destination of the message.</param>
    /// <param name="name">The name of the message.</param>
    public Identifier(Destination destination, string name)
    {
        Destination = destination;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    /// <summary>
    /// Returns a new <see cref="Identifier"/> with the destination changed.
    /// </summary>
    /// <param name="destination">The new destination.</param>
    /// <returns>A new instance of <see cref="Identifier"/> with the destination changed.</returns>
    public Identifier WithDestination(Destination destination) => new(destination, Name);

    public override int GetHashCode()
    {
        int hashCode = Name.GetHashCode();
        return IsOutgoing ? hashCode : ~hashCode;
    }

    public override bool Equals(object? obj)
    {
        return
            obj is Identifier other &&
            Equals(other);
    }

    public bool Equals(Identifier other)
    {
        return
            Destination.Equals(other.Destination) &&
            string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString() => Name;

    public static bool operator ==(Identifier a, Identifier b)
    {
        if (a is null)
            return b is null;
        return a.Equals(b);
    }

    public static bool operator !=(Identifier a, Identifier b) => !(a == b);

    public static implicit operator Identifier(string name) => new Identifier(Destination.Unknown, name);
}
