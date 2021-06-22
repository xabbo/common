using System;

namespace Xabbo.Messages
{
    /// <summary>
    /// Specifies header information for multiple client types.
    /// </summary>
    public class Header
    {
        public static readonly Header Unknown = new Header();

        /// <summary>
        /// Gets the destination of this header.
        /// </summary>
        public Destination Destination { get; init; }
        public bool IsIncoming => Destination == Destination.Client;
        public bool IsOutgoing => Destination == Destination.Server;
        /// <summary>
        /// Gets the header information for the Flash client.
        /// </summary>
        public ClientHeader? Flash { get; init; }
        /// <summary>
        /// Gets the header information for the Unity client.
        /// </summary>
        public ClientHeader? Unity { get; init; }
        /// <summary>
        /// Gets the name of this header.
        /// </summary>
        public string? Name { get; init; }
        /// <summary>
        /// Get the absolute value of this header.
        /// </summary>
        public short? Value { get; init; }

        public Header()
        {
            Destination = Destination.Unknown;
        }

        public Header(Destination destination, short value)
        {
            Destination = destination;
            Value = value;
        }

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

        public ClientHeader? GetClientHeader(ClientType clientType)
        {
            return clientType switch
            {
                ClientType.Flash => Flash,
                ClientType.Unity => Unity,
                _ => null
            };
        }

        public string? GetName(ClientType clientType) => GetClientHeader(clientType)?.Name;

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
        /// Creates an outgoing header with the specified absolute value.
        /// </summary>
        public static Header In(short value) => new Header
        {
            Destination = Destination.Client,
            Value = value
        };

        /// <summary>
        /// Creates an incoming header with the specified absolute value.
        /// </summary>
        public static Header Out(short value) => new Header
        {
            Destination = Destination.Server,
            Value = value
        };
    }
}