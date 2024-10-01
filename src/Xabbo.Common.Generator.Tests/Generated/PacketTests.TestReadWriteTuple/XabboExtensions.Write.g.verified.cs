//HintName: XabboExtensions.Write.g.cs
internal static partial class XabboExtensions
{
    /// <summary>
    /// Writes 2 values of the specified types to the current position in the packet.
    /// </summary>
    public static void Write<T1, T2>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
    }
}
