internal static partial class XabboExtensions
{
    /// <summary>
    /// Modifies 3 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3>(this global::Xabbo.Messages.IPacket p, int pos, global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p, ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
    }
}
