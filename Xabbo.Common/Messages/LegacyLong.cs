using System;

namespace Xabbo.Messages;

public struct LegacyLong
{
    public long Value { get; }

    public LegacyLong(long value)
    {
        Value = value;
    }

    public static implicit operator LegacyLong(long value) => new LegacyLong(value);
    public static implicit operator long(LegacyLong legacyLong) => legacyLong.Value;
}
