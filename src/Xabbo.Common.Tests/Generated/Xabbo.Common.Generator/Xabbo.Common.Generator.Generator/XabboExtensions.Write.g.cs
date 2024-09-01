internal static partial class XabboExtensions
{
    /// <summary>
    /// Writes 2 values of the specified types to the current position in the packet.
    /// </summary>
    public static void Write<T1, T2>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p);
        Write(in w, arg1);
        Write(in w, arg2);
    }
    
    /// <summary>
    /// Writes 6 values of the specified types to the current position in the packet.
    /// </summary>
    public static void Write<T1, T2, T3, T4, T5, T6>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
    }
    
    /// <summary>
    /// Writes 8 values of the specified types to the current position in the packet.
    /// </summary>
    public static void Write<T1, T2, T3, T4, T5, T6, T7, T8>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p);
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
        Write(in w, arg8);
    }
}
