//HintName: XabboExtensions.WriteAt.g.cs
internal static partial class XabboExtensions
{
    /// <summary>
    /// Writes 2 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
    }
    
    /// <summary>
    /// Writes 3 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
    }
    
    /// <summary>
    /// Writes 4 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3, T4>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
    }
    
    /// <summary>
    /// Writes 5 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3, T4, T5>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
    }
    
    /// <summary>
    /// Writes 6 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3, T4, T5, T6>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
    }
    
    /// <summary>
    /// Writes 7 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3, T4, T5, T6, T7>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
    }
    
    /// <summary>
    /// Writes 8 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3, T4, T5, T6, T7, T8>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
        Write(in w, arg8);
    }
    
    /// <summary>
    /// Writes 9 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
        Write(in w, arg8);
        Write(in w, arg9);
    }
    
    /// <summary>
    /// Writes 10 values of the specified types to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this global::Xabbo.Messages.IPacket p, int pos, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
        Write(in w, arg8);
        Write(in w, arg9);
        Write(in w, arg10);
    }
}
