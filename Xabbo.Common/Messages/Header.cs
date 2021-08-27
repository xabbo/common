using System;
using System.ComponentModel;

namespace Xabbo.Messages
{
    /// <summary>
    /// Specifies header information for multiple client types.
    /// </summary>
    public class Header
    {
        /// <summary>
        /// Represents an unknown header.
        /// </summary>
        public static readonly Header Unknown = new Header();

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
        /// Get the explicit value of this header.
        /// If this value set, it overrides client header information.
        /// It should only be used when a dynamic header from the message manager is unavailable.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public short? Value { get; init; }

        /// <summary>
        /// Creates a new header.
        /// </summary>
        public Header()
        {
            Destination = Destination.Unknown;
        }

        /// <summary>
        /// Creates a new header with the specified destination and explicit value.
        /// </summary>
        public Header(Destination destination, short value)
        {
            Destination = destination;
            Value = value;
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
        /// If an explicit value is set, that value will be returned regardless of the client type.
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
                    ClientType.Flash => Flash?.Value ?? throw new Exception("Unknown Flash header."),
                    ClientType.Unity => Unity?.Value ?? throw new Exception("Unknown Unity header."),
                    _ => throw new Exception("Invalid client type specified.")
                };
            }
        }

        /// <summary>
        /// Gets the name of the header for the specified client type.
        /// If an explicit name is set, that name will be returned regardless of the client type.
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

        public override string ToString() => Unity?.Name ?? Flash?.Name ?? Value?.ToString() ?? "unknown";

        public static implicit operator Header(short value) => new Header() { Value = value };
        public static bool operator ==(Header a, Header b) => a.Equals(b);
        public static bool operator !=(Header a, Header b) => !(a == b);

        /// <summary>
        /// Creates an outgoing header with the specified explicit value.
        /// </summary>
        public static Header In(short value) => new Header
        {
            Destination = Destination.Client,
            Value = value
        };

        /// <summary>
        /// Creates an incoming header with the specified explicit value.
        /// </summary>
        public static Header Out(short value) => new Header
        {
            Destination = Destination.Server,
            Value = value
        };
    }
}