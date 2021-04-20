using System;

namespace Xabbo.Messages
{
    public class Header
    {
        public static readonly Header Unknown = new Header(Destination.Unknown, -1, null);

        public Destination Destination { get; }
        public bool IsIncoming => Destination == Destination.Client;
        public bool IsOutgoing => Destination == Destination.Server;
        public short Value { get; }
        public string? Name { get; }

        public Header(Destination destination, short value, string? name)
        {
            Destination = destination;
            Value = value;
            Name = name;
        }

        public override int GetHashCode() => Value;

        public override bool Equals(object? obj)
        {
            return
                obj is Header other &&
                Equals(other);
        }

        public bool Equals(Header other)
        {
            if (Value != other.Value) return false;

            if (Destination != Destination.Unknown &&
                other.Destination != Destination.Unknown &&
                Destination != other.Destination)
            {
                return false;
            }

            if (Name is not null &&
                other.Name is not null &&
                !Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }

        public static implicit operator short(Header header) => header.Value;
        public static implicit operator Header(short value) => new Header(Destination.Unknown, value, null);
        public static bool operator ==(Header a, Header b) => a.Equals(b);
        public static bool operator !=(Header a, Header b) => !(a == b);
    }
}
