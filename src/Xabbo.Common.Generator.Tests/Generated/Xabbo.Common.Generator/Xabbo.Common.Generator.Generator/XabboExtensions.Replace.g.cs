﻿internal static partial class XabboExtensions
{
    /// <summary>
    /// Replaces 2 values of the specified types at the current position in the packet.
    /// </summary>
    public static void Replace<T1, T2>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Replace(in w, arg1);
        Replace(in w, arg2);
    }
    
    /// <summary>
    /// Replaces 3 values of the specified types at the current position in the packet.
    /// </summary>
    public static void Replace<T1, T2, T3>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2, T3 arg3)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Replace(in w, arg1);
        Replace(in w, arg2);
        Replace(in w, arg3);
    }
    
    /// <summary>
    /// Replaces 4 values of the specified types at the current position in the packet.
    /// </summary>
    public static void Replace<T1, T2, T3, T4>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Replace(in w, arg1);
        Replace(in w, arg2);
        Replace(in w, arg3);
        Replace(in w, arg4);
    }
    
    /// <summary>
    /// Replaces 9 values of the specified types at the current position in the packet.
    /// </summary>
    public static void Replace<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this global::Xabbo.Messages.IPacket p, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Replace(in w, arg1);
        Replace(in w, arg2);
        Replace(in w, arg3);
        Replace(in w, arg4);
        Replace(in w, arg5);
        Replace(in w, arg6);
        Replace(in w, arg7);
        Replace(in w, arg8);
        Replace(in w, arg9);
    }
}
