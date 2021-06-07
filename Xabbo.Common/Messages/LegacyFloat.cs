using System;

namespace Xabbo.Messages
{
    public struct LegacyFloat
    {
        public float Value { get; }

        public LegacyFloat(float value)
        {
            Value = value;
        }

        public static implicit operator LegacyFloat(float value) => new LegacyFloat(value);
        public static implicit operator float(LegacyFloat legacyFloat) => legacyFloat.Value;
    }
}
