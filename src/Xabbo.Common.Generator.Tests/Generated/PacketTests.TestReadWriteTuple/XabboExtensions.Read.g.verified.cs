//HintName: XabboExtensions.Read.g.cs
internal static partial class XabboExtensions
{
    /// <summary>
    /// Reads 2 values of the specified types from the current position in the packet.
    /// </summary>
    public static (T1, T2) Read<T1, T2>(this global::Xabbo.Messages.IPacket p)
    {
        global::Xabbo.Messages.PacketReader r = p.Reader();
        return (Read<T1>(in r), Read<T2>(in r));
    }
}
