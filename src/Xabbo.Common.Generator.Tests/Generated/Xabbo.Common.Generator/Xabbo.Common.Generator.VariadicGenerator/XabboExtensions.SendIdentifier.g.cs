﻿internal static partial class XabboExtensions
{
    /// <summary>
    /// Sends a value of the specified type with the specified message identifier.
    /// </summary>
    public static void Send<T>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T arg)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 2 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 3 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 4 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3, T4>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        c.Send(p);
    }
}