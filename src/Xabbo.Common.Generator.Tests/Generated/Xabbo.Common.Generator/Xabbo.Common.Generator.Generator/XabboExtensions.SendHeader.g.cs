internal static partial class XabboExtensions
{
    /// <summary>
    /// Sends a value of the specified type with the specified message header.
    /// </summary>
    public static void Send<T>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Header header, T arg)
    {
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 2 values of the specified types with the specified message header.
    /// </summary>
    public static void Send<T1, T2>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Header header, T1 arg1, T2 arg2)
    {
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 3 values of the specified types with the specified message header.
    /// </summary>
    public static void Send<T1, T2, T3>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3)
    {
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 4 values of the specified types with the specified message header.
    /// </summary>
    public static void Send<T1, T2, T3, T4>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 5 values of the specified types with the specified message header.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 9 values of the specified types with the specified message header.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
        Write(in w, arg8);
        Write(in w, arg9);
        c.Send(p);
    }
}
