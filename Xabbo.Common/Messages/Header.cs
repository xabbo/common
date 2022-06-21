using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

using Xabbo.Common;

namespace Xabbo.Messages;

/// <summary>
/// Specifies message header information for multiple client types.
/// </summary>
public class Header
{
    /// <summary>
    /// Represents an unknown header.
    /// </summary>
    public static readonly Header Unknown = new();

    /// <summary>
    /// Gets the destination of this header.
    /// </summary>
    public Destination Destination { get; init; }

    /// <summary>
    /// Gets if this is an incoming header.
    /// </summary>
    public bool IsIncoming => Destination == Destination.Client;

    /// <summary>
    /// Gets if this is an outgoing header.
    /// </summary>
    public bool IsOutgoing => Destination == Destination.Server;

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

    private Header() { Destination = Destination.Unknown; }

    public Header(Destination destination, ClientHeader? unityHeader, ClientHeader? flashHeader)
    {
        Destination = destination;
        Unity = unityHeader;
        Flash = flashHeader;

        Name = Unity?.Name ?? Flash?.Name;
    }

    /// <summary>
    /// Creates a new header with the specified destination and explicit value.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Header(Destination destination, short value = -1, string? name = null)
    {
        Destination = destination;
        Value = value;
        Name = name;

        Unity = new ClientHeader
        {
            Client = ClientType.Unity,
            Destination = destination,
            Name = name ?? value.ToString(),
            Value = value
        };

        Flash = new ClientHeader
        {
            Client = ClientType.Flash,
            Destination = destination,
            Name = name ?? value.ToString(),
            Value = value
        };
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
        if (Value.HasValue)
        {
            return Value.Value;
        }
        else
        {
            return clientType switch
            {
                ClientType.Flash => Flash?.Value ?? -1,
                ClientType.Unity => Unity?.Value ?? -1,
                _ => throw new Exception("Invalid client type specified.")
            };
        }
    }

    /// <summary>
    /// Gets the name of the header for the specified client type.
    /// </summary>
    public string? GetName(ClientType clientType) => GetClientHeader(clientType)?.Name;

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

        if (Destination != Destination.Unknown &&
            other.Destination != Destination.Unknown &&
            Destination != other.Destination)
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
            .Append(Destination switch {
                Destination.Client => "in:",
                Destination.Server => "out:",
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
            .Append(Destination switch {
                Destination.Client => "in:",
                Destination.Server => "out:",
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
    public static Header In(short value) => new(Destination.Client, value);

    /// <summary>
    /// Creates an incoming header with the specified explicit value.
    /// </summary>
    public static Header Out(short value) => new(Destination.Server, value);

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