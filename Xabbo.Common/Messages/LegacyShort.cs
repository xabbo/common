using System;

namespace Xabbo.Messages
{
    public struct LegacyShort
    {
        public short Value { get; }

        public LegacyShort(short value)
        {
            Value = value;
        }

        public static implicit operator LegacyShort(short value) => new LegacyShort(value);
        public static implicit operator short(LegacyShort legacyShort) => legacyShort.Value;
    }
}
