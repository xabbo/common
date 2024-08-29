internal static partial class XabboExtensions
{
    private static T Read<T>(in global::Xabbo.Messages.PacketReader r)
    {
        if (typeof(T) == typeof(bool)) return (T)(object)r.ReadBool();
        if (typeof(T) == typeof(byte)) return (T)(object)r.ReadByte();
        if (typeof(T) == typeof(short)) return (T)(object)r.ReadShort();
        if (typeof(T) == typeof(int)) return (T)(object)r.ReadInt();
        if (typeof(T) == typeof(float)) return (T)(object)r.ReadFloat();
        if (typeof(T) == typeof(long)) return (T)(object)r.ReadLong();
        if (typeof(T) == typeof(string)) return (T)(object)r.ReadString();
        if (typeof(T) == typeof(global::Xabbo.Id)) return (T)(object)r.ReadId();
        if (typeof(T) == typeof(global::Xabbo.Length)) return (T)(object)r.ReadLength();
        /* Generated 4 cases */
        if (typeof(T) == typeof(global::Xabbo.Common.Generator.Tests.TestParser))
            return (T)(object)r.Parse<global::Xabbo.Common.Generator.Tests.TestParser>();
        if (typeof(T) == typeof(global::Xabbo.Common.Generator.Tests.TestParserArrayOnly[]))
            return (T)(object)ReadArray<global::Xabbo.Common.Generator.Tests.TestParserArrayOnly>(in r);
        if (typeof(T) == typeof(global::Xabbo.Common.Generator.Tests.TestParserArrayOnly))
            return (T)(object)r.Parse<global::Xabbo.Common.Generator.Tests.TestParserArrayOnly>();
        if (typeof(T) == typeof(global::Xabbo.Common.Generator.Tests.TestParserComposer))
            return (T)(object)r.Parse<global::Xabbo.Common.Generator.Tests.TestParserComposer>();
        throw new global::System.NotSupportedException($"Cannot read value of type '{typeof(T)}'.");
    }
}
