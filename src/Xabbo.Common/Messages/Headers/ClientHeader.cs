namespace Xabbo.Messages;

/// <summary>
/// Specifies message header information for a specific client type.
/// </summary>
public class ClientHeader
{
    /// <summary>
    /// The client type of this header.
    /// </summary>
    public ClientType Client { get; init; }

    /// <summary>
    /// The direction of this header.
    /// </summary>
    public Direction Direction { get; init; }

    /// <summary>
    /// The value of this header.
    /// </summary>
    public short Value { get; init; }

    /// <summary>
    /// The name of this header.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    public override int GetHashCode() => (Client, Direction, Value).GetHashCode();
    public override bool Equals(object? obj) => obj is ClientHeader other && Equals(other);

    /// <summary>
    /// Returns true if the client, destination and value are equal.
    /// The name of the header is not tested for equality.
    /// </summary>
    public bool Equals(ClientHeader other)
    {
        return
            Client == other.Client &&
            Direction == other.Direction &&
            Value == other.Value;
    }

    public static implicit operator ClientHeader((ClientType, Direction, short) tuple)
    {
        return new ClientHeader()
        {
            Client = tuple.Item1,
            Direction = tuple.Item2,
            Value = tuple.Item3
        };
    }
}
