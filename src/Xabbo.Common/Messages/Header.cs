namespace Xabbo.Messages;

/// <summary>
/// Specifies a client, direction and header value.
/// </summary>
public readonly record struct Header(Clients Client, Direction Direction, short Value)
{
    public Header(Direction direction, short value) : this(Clients.None, direction, value) { }

    /// <summary>
    /// Represents an unknown header.
    /// </summary>
    public static readonly Header Unknown = new();

    /// <summary>
    /// Represents a header that matches all messages.
    /// </summary>
    public static readonly Header All = new(Clients.All, Direction.Both, 0);

    public static implicit operator Header((Clients client, Direction direction, short value) x) => new(x.client, x.direction, x.value);
    public static implicit operator Header((Direction direction, short value) x) => new(x.direction, x.value);
}