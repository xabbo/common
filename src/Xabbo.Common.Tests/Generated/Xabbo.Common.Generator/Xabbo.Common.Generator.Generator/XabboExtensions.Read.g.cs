internal static partial class XabboExtensions
{
    /// <summary>
    /// Reads 8 values of the specified types from the current position in the packet.
    /// </summary>
    public static (T1, T2, T3, T4, T5, T6, T7, T8) Read<T1, T2, T3, T4, T5, T6, T7, T8>(this global::Xabbo.Messages.IPacket p)
    {
        global::Xabbo.Messages.PacketReader r = p.Reader();
        return (
            Read<T1>(in r), Read<T2>(in r), Read<T3>(in r), Read<T4>(in r), Read<T5>(in r), 
            Read<T6>(in r), Read<T7>(in r), Read<T8>(in r)
        );
    }
}
