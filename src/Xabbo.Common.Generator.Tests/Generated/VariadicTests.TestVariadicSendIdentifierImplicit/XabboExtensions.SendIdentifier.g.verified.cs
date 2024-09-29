//HintName: XabboExtensions.SendIdentifier.g.cs
internal static partial class XabboExtensions
{
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
}
