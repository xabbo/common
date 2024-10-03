//HintName: XabboExtensions.ModifyAt.g.cs
internal static partial class XabboExtensions
{
    /// <summary>
    /// Modifies 2 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2>(this global::Xabbo.Messages.IPacket p, int pos, global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
    }
    
    /// <summary>
    /// Modifies 3 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3>(this global::Xabbo.Messages.IPacket p, int pos, global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
    }
    
    /// <summary>
    /// Modifies 4 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4>(this global::Xabbo.Messages.IPacket p, int pos, global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
        Modify(in w, arg4);
    }
    
    /// <summary>
    /// Modifies 5 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4, T5>(this global::Xabbo.Messages.IPacket p, int pos, 
        global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4, 
        global::System.Func<T5, T5> arg5
    )
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
        Modify(in w, arg4);
        Modify(in w, arg5);
    }
    
    /// <summary>
    /// Modifies 6 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4, T5, T6>(this global::Xabbo.Messages.IPacket p, int pos, 
        global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4, 
        global::System.Func<T5, T5> arg5, global::System.Func<T6, T6> arg6
    )
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
        Modify(in w, arg4);
        Modify(in w, arg5);
        Modify(in w, arg6);
    }
    
    /// <summary>
    /// Modifies 7 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4, T5, T6, T7>(this global::Xabbo.Messages.IPacket p, int pos, 
        global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4, 
        global::System.Func<T5, T5> arg5, global::System.Func<T6, T6> arg6, global::System.Func<T7, T7> arg7
    )
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
        Modify(in w, arg4);
        Modify(in w, arg5);
        Modify(in w, arg6);
        Modify(in w, arg7);
    }
    
    /// <summary>
    /// Modifies 8 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4, T5, T6, T7, T8>(this global::Xabbo.Messages.IPacket p, int pos, 
        global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4, 
        global::System.Func<T5, T5> arg5, global::System.Func<T6, T6> arg6, global::System.Func<T7, T7> arg7, global::System.Func<T8, T8> arg8
    )
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
        Modify(in w, arg4);
        Modify(in w, arg5);
        Modify(in w, arg6);
        Modify(in w, arg7);
        Modify(in w, arg8);
    }
    
    /// <summary>
    /// Modifies 9 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this global::Xabbo.Messages.IPacket p, int pos, 
        global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4, 
        global::System.Func<T5, T5> arg5, global::System.Func<T6, T6> arg6, global::System.Func<T7, T7> arg7, global::System.Func<T8, T8> arg8, 
        global::System.Func<T9, T9> arg9
    )
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
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
    
    /// <summary>
    /// Modifies 10 values of the specified types at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this global::Xabbo.Messages.IPacket p, int pos, 
        global::System.Func<T1, T1> arg1, global::System.Func<T2, T2> arg2, global::System.Func<T3, T3> arg3, global::System.Func<T4, T4> arg4, 
        global::System.Func<T5, T5> arg5, global::System.Func<T6, T6> arg6, global::System.Func<T7, T7> arg7, global::System.Func<T8, T8> arg8, 
        global::System.Func<T9, T9> arg9, global::System.Func<T10, T10> arg10
    )
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify(in w, arg1);
        Modify(in w, arg2);
        Modify(in w, arg3);
        Modify(in w, arg4);
        Modify(in w, arg5);
        Modify(in w, arg6);
        Modify(in w, arg7);
        Modify(in w, arg8);
        Modify(in w, arg9);
        Modify(in w, arg10);
    }
}
