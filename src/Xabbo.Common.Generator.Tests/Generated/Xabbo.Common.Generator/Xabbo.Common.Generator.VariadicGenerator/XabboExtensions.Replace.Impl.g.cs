internal static partial class XabboExtensions
{
    private static void Replace<T>(in global::Xabbo.Messages.PacketWriter w, T value)
    {
        switch (value)
        {
            case bool v: w.ReplaceBool(v); break;
            case byte v: w.ReplaceByte(v); break;
            case short v: w.ReplaceShort(v); break;
            case int v: w.ReplaceInt(v); break;
            case float v: w.ReplaceFloat(v); break;
            case long v: w.ReplaceLong(v); break;
            case string v: w.ReplaceString(v); break;
            case global::Xabbo.Length v: w.ReplaceLength(v); break;
            case global::Xabbo.Id v: w.ReplaceId(v); break;
            case global::Xabbo.Messages.B64 v: w.ReplaceB64(v); break;
            case global::Xabbo.Messages.VL64 v: w.ReplaceVL64(v); break;
            case global::Xabbo.Common.Generator.Tests.TestParserComposer v: w.ReplaceStruct(v); break;
            case global::Xabbo.Common.Generator.Tests.TestModifiable v: w.ReplaceStruct(v); break;
            default: throw new global::System.NotSupportedException($"Cannot replace value of type '{typeof(T)}'.");
        }
    }
}
