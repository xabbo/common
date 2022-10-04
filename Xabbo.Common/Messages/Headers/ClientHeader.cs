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
    /// The destination of this header.
    /// </summary>
    public Destination Destination { get; init; }

    /// <summary>
    /// The value of this header.
    /// </summary>
    public short Value { get; init; }

    /// <summary>
    /// The name of this header.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    public ClientHeader() { }

    public override int GetHashCode() => (Client, Destination, Value).GetHashCode();
    public override bool Equals(object? obj) => obj is ClientHeader other && Equals(other);

    /// <summary>
    /// Returns true if the client, destination and value are equal.
    /// The name of the header is not tested for equality.
    /// </summary>
    public bool Equals(ClientHeader other)
    {
        return
            Client == other.Client &&
            Destination == other.Destination &&
            Value == other.Value;
    }

    public static implicit operator ClientHeader((ClientType, Destination, short) tuple)
    {
        return new ClientHeader()
        {
            Client = tuple.Item1,
            Destination = tuple.Item2,
            Value = tuple.Item3
        };
    }
}
