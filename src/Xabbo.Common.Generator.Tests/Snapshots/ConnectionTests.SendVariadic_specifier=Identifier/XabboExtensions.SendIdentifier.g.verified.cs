//HintName: XabboExtensions.SendIdentifier.g.cs
internal static partial class XabboExtensions
{
    /// <summary>
    /// Sends a value of the specified type with the specified message identifier.
    /// </summary>
    public static void Send<T>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T arg)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
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
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
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
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
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
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 5 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 6 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 7 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 8 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write(in w, arg1);
        Write(in w, arg2);
        Write(in w, arg3);
        Write(in w, arg4);
        Write(in w, arg5);
        Write(in w, arg6);
        Write(in w, arg7);
        Write(in w, arg8);
        c.Send(p);
    }
    
    /// <summary>
    /// Sends 9 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
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
    
    /// <summary>
    /// Sends 10 values of the specified types with the specified message identifier.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header, c.Session.Client.Type) { Context = c };
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
        Write(in w, arg10);
        c.Send(p);
    }
}
