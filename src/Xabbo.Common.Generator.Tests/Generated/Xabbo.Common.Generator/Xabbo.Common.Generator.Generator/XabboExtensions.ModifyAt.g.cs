internal static partial class XabboExtensions
{
    /// <summary>
    /// Modifies 9 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this global::Xabbo.Messages.IPacket p, int pos, 
        global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4, 
        global::System.Func<T5, T5> arg5, global::System.Func<T6, T6> arg6, global::System.Func<T7, T7> arg7, global::System.Func<T8, T8> arg8, 
        global::System.Func<T9, T9> arg9
    )
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p, ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
        Modify(in w, arg4);
        Modify(in w, arg5);
        Modify(in w, arg6);
        Modify(in w, arg7);
        Modify(in w, arg8);
        Modify(in w, arg9);
    }
}
