using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents a message name and direction.
/// </summary>
public readonly struct Identifier
{
    /// <summary>
    /// Gets the direction of this message.
    /// </summary>
    public Direction Direction { get; }

    /// <summary>
    /// Gets whether this is an outgoing message.
    /// </summary>
    public bool IsOutgoing => Direction == Direction.Outgoing;

    /// <summary>
    /// Gets whether this is an incoming message.
    /// </summary>
    public bool IsIncoming => Direction == Direction.Incoming;

    /// <summary>
    /// Gets the name of this message.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Constructs a new message <see cref="Identifier"/> with the specified name and destination.
    /// </summary>
    /// <param name="direction">The direction of the message.</param>
    /// <param name="name">The name of the message.</param>
    public Identifier(Direction direction, string name)
    {
        if (direction != Direction.Incoming
            && direction != Direction.Outgoing
            && direction != Direction.Unknown)
        {
            throw new ArgumentException(
                "The direction of an identifier must be incoming, outgoing or unknown.",
                nameof(direction)
            );
        }

        ArgumentException.ThrowIfNullOrEmpty(name);

        for (int i = 0; i < name.Length; i++)
            if (!char.IsAsciiLetterOrDigit(name[i]))
                throw new ArgumentException("Identifier name must only consist of alphanumeric characters.", nameof(name));

        Direction = direction;
        Name = name;
    }

    /// <summary>
    /// Returns a new <see cref="Identifier"/> with the destination changed.
    /// </summary>
    /// <param name="direction">The new direction.</param>
    /// <returns>A new instance of <see cref="Identifier"/> with the destination changed.</returns>
    public Identifier WithDirection(Direction direction) => new(direction, Name);

    public override int GetHashCode() => Name.ToUpperInvariant().GetHashCode();

    public override bool Equals(object? obj)
    {
        return
            obj is Identifier other &&
            Equals(other);
    }

    /// <summary>
    /// Gets if this identifier is equal or compatible with the <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The other identifier to compare to.</param>
    /// <returns>
    /// <see langword="true" /> if the names of the two identifiers are equal,
    /// and their directions are equal or either of them is unknown.
    /// </returns>
    public bool Equals(Identifier other)
    {
        return
            (Direction.Equals(other.Direction)
            || Direction == Direction.Unknown
            || other.Direction == Direction.Unknown)
            && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString() => Name;

    public static bool operator ==(Identifier a, Identifier b) => a.Equals(b);

    public static bool operator !=(Identifier a, Identifier b) => !(a == b);

    public static implicit operator Identifier(string name)
    {
        if (name.StartsWith("in:"))
        {
            return new(Direction.Incoming, name[3..]);
        }
        else if (name.StartsWith("out:"))
        {
            return new(Direction.Outgoing, name[4..]);
        }
        else
        {
            return new(Direction.Unknown, name);
        }
    }
}
