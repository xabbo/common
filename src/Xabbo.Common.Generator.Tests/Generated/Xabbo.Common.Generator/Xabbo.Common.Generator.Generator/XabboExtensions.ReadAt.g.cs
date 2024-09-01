﻿internal static partial class XabboExtensions
{
    /// <summary>
    /// Reads 2 values of the specified types from the specified position in the packet.
    /// </summary>
    public static (T1, T2) ReadAt<T1, T2>(this global::Xabbo.Messages.IPacket p, int pos)
    {
        global::Xabbo.Messages.PacketReader r = new global::Xabbo.Messages.PacketReader(p, ref pos);
        return (Read<T1>(in r), Read<T2>(in r));
    }
    
    /// <summary>
    /// Reads 3 values of the specified types from the specified position in the packet.
    /// </summary>
    public static (T1, T2, T3) ReadAt<T1, T2, T3>(this global::Xabbo.Messages.IPacket p, int pos)
    {
        global::Xabbo.Messages.PacketReader r = new global::Xabbo.Messages.PacketReader(p, ref pos);
        return (Read<T1>(in r), Read<T2>(in r), Read<T3>(in r));
    }
    
    /// <summary>
    /// Reads 4 values of the specified types from the specified position in the packet.
    /// </summary>
    public static (T1, T2, T3, T4) ReadAt<T1, T2, T3, T4>(this global::Xabbo.Messages.IPacket p, int pos)
    {
        global::Xabbo.Messages.PacketReader r = new global::Xabbo.Messages.PacketReader(p, ref pos);
        return (Read<T1>(in r), Read<T2>(in r), Read<T3>(in r), Read<T4>(in r));
    }
    
    /// <summary>
    /// Reads 9 values of the specified types from the specified position in the packet.
    /// </summary>
    public static (T1, T2, T3, T4, T5, T6, T7, T8, T9) ReadAt<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this global::Xabbo.Messages.IPacket p, int pos)
    {
        global::Xabbo.Messages.PacketReader r = new global::Xabbo.Messages.PacketReader(p, ref pos);
        return (
            Read<T1>(in r), Read<T2>(in r), Read<T3>(in r), Read<T4>(in r), Read<T5>(in r), 
            Read<T6>(in r), Read<T7>(in r), Read<T8>(in r), Read<T9>(in r)
        );
    }
}
