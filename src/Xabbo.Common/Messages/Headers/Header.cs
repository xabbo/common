using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Specifies message header information for multiple client types.
/// </summary>
public sealed class Header
{
    /// <summary>
    /// Represents an unknown header.
    /// </summary>
    public static readonly Header Unknown = new();

    /// <summary>
    /// Gets the direction of this header.
    /// </summary>
    public Direction Direction { get; init; }

    /// <summary>
    /// Gets if this is an incoming header.
    /// </summary>
    public bool IsIncoming => Direction == Direction.Incoming;

    /// <summary>
    /// Gets if this is an outgoing header.
    /// </summary>
    public bool IsOutgoing => Direction == Direction.Outgoing;

    /// <summary>
    /// Gets the header information for the Flash client.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ClientHeader? Flash { get; init; }

    /// <summary>
    /// Gets the header information for the Unity client.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ClientHeader? Unity { get; init; }

    /// <summary>
    /// Gets the explicit name of this header.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string? Name { get; init; }

    /// <summary>
    /// Gets the explicit value of this header.
    /// If this value set, it overrides client header information.
    /// It should only be used when a dynamic header from the message manager is unavailable.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public short? Value { get; init; }

    private Header() { Direction = Direction.Unknown; }

    public Header(Direction direction, ClientHeader? unityHeader, ClientHeader? flashHeader)
    {
        Direction = direction;
        Unity = unityHeader;
        Flash = flashHeader;

        Name = Unity?.Name ?? Flash?.Name;
    }

    /// <summary>
    /// Creates a new header with the specified direction and explicit value.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Header(Direction direction, short value = -1, string? name = null)
    {
        Direction = direction;
        Value = value;
        Name = name;
    }

    /// <summary>
    /// Gets the header information for the specified client type.
    /// </summary>
    public ClientHeader? GetClientHeader(ClientType clientType)
    {
        return clientType switch
        {
            ClientType.Flash => Flash,
            ClientType.Unity => Unity,
            _ => null
        };
    }

    /// <summary>
    /// Gets the header value for the specified client type.
    /// </summary>
    public short GetValue(ClientType clientType)
    {
        short headerValue = Value ?? clientType switch
        {
            ClientType.Flash => Flash?.Value,
            ClientType.Unity => Unity?.Value,
            _ => throw new Exception($"Invalid client type specified: {clientType}.")
        } ?? -1;

        if (headerValue <= 0)
        {
            throw new UnresolvedHeaderException(clientType, this);
        }

        return headerValue;
    }

    /// <summary>
    /// Gets the name of the header for the specified client type.
    /// </summary>
    public string? GetName(ClientType clientType) => GetClientHeader(clientType)?.Name ?? Name;

    /// <summary>
    /// Returns the hash code of this header.
    /// </summary>
    public override int GetHashCode() => (Unity, Flash, Value).GetHashCode();

    public override bool Equals(object? obj)
    {
        return
            obj is Header other &&
            Equals(other);
    }

    public bool Equals(Header other)
    {
        if (Unity != other.Unity ||
            Flash != other.Flash ||
            Value != other.Value)
        {
            return false;
        }

        if (Direction != Direction.Unknown &&
            other.Direction != Direction.Unknown &&
            Direction != other.Direction)
        {
            return false;
        }

        return true;
    }

    public string ToString(ClientType client)
    {
        ClientHeader? header = GetClientHeader(client);
        if (header is null) return ToString();

        StringBuilder sb = new();

        sb
            .Append(Direction switch {
                Direction.Incoming => "in:",
                Direction.Outgoing => "out:",
                _ => string.Empty
            })
            .Append(header.Name);

        if (header.Value >= 0)
        {
            sb
                .Append('[')
                .Append(header.Value)
                .Append(']');
        }

        return sb.ToString();
    }

    public override string ToString()
    {
        return new StringBuilder()
            .Append(Direction switch {
                Direction.Incoming => "in:",
                Direction.Outgoing => "out:",
                _ => string.Empty
            })
            .Append(Name ?? Value?.ToString() ?? "unknown")
            .ToString();
    }

    public static bool operator ==(Header a, Header b) => a.Equals(b);
    public static bool operator !=(Header a, Header b) => !(a == b);

    /// <summary>
    /// Creates an outgoing header with the specified explicit value.
    /// </summary>
    public static Header In(short value) => new(Direction.Incoming, value);

    /// <summary>
    /// Creates an incoming header with the specified explicit value.
    /// </summary>
    public static Header Out(short value) => new(Direction.Outgoing, value);

    /// <summary>
    /// Converts the specified tuple into an array of headers.
    /// </summary>
    public static Header[] FromTuple(ITuple tuple)
    {
        Header[] headers = new Header[tuple.Length];
        for (int i = 0; i < tuple.Length; i++)
        {
            if (tuple[i] is not Header header)
                throw new Exception($"All tuple values must be of type {typeof(Header).FullName}.");
            headers[i] = header;
        }
        return headers;
    }
}