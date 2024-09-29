[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
internal static partial class XabboExtensions
{
    private static partial T Read<T>(in global::Xabbo.Messages.PacketReader r);

    private static T[] ReadArray<T>(in global::Xabbo.Messages.PacketReader r)
    {
        T[] array = new T[r.ReadLength()];
        for (int i = 0; i < array.Length; i++)
            array[i] = Read<T>(in r);
        return array;
    }

    private static void Write<T>(in global::Xabbo.Messages.PacketWriter w, T value)
    {
        switch (value)
        {
            case bool v: w.WriteBool(v); break;
            case byte v: w.WriteByte(v); break;
            case short v: w.WriteShort(v); break;
            case int v: w.WriteInt(v); break;
            case float v: w.WriteFloat(v); break;
            case long v: w.WriteLong(v); break;
            case string v: w.WriteString(v); break;
            case global::Xabbo.Length v: w.WriteLength(v); break;
            case global::Xabbo.Id v: w.WriteId(v); break;
            case global::Xabbo.Messages.B64 v: w.WriteB64(v); break;
            case global::Xabbo.Messages.VL64 v: w.WriteVL64(v); break;
            case global::Xabbo.Messages.IComposer v: w.Compose(v); break;
            case global::System.Collections.IEnumerable values:
                int start = w.Pos;
                w.WriteLength(0);
                int n = 0;
                foreach (object v in values)
                {
                    Write<object>(in w, v);
                    n++;
                }
                checked { w.WriterAt(ref start).ReplaceLength((global::Xabbo.Length)n); }
                break;
            default: throw new global::System.NotSupportedException($"Cannot write value of type '{typeof(T)}'.");
        }
    }

    private static partial void Replace<T>(in global::Xabbo.Messages.PacketWriter w, T value);

    private static void Modify<T>(in global::Xabbo.Messages.PacketWriter w, global::System.Func<T, T> modifier)
    {
        int pos = w.Pos;
        Replace<T>(in w, modifier(Read<T>(w.ReaderAt(ref pos))));
    }

    /// <summary>
    /// Reads a value of the specified type from the current position in the packet.
    /// </summary>
    public static T Read<T>(this global::Xabbo.Messages.IPacket p)
    {
        global::Xabbo.Messages.PacketReader r = p.Reader();
        return Read<T>(in r);
    }

    /// <summary>
    /// Reads a value of the specified type from the specified position in the packet.
    /// </summary>
    public static T ReadAt<T>(this global::Xabbo.Messages.IPacket p, int pos)
    {
        global::Xabbo.Messages.PacketReader r = p.ReaderAt(ref pos);
        return Read<T>(in r);
    }

    /// <summary>
    /// Writes a value of the specified type to the current position in the packet.
    /// </summary>
    public static void Write<T>(this global::Xabbo.Messages.IPacket p, T value)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Write<T>(in w, value);
    }

    /// <summary>
    /// Writes a value of the specified type to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T>(this global::Xabbo.Messages.IPacket p, int pos, T value)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Write<T>(in w, value);
    }

    /// <summary>
    /// Replaces a value of the specified type at the current position in the packet.
    /// </summary>
    public static void Replace<T>(this global::Xabbo.Messages.IPacket p, T value)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Replace<T>(in w, value);
    }

    /// <summary>
    /// Replaces a value of the specified type at the specified position in the packet.
    /// </summary>
    public static void ReplaceAt<T>(this global::Xabbo.Messages.IPacket p, int pos, T value)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Replace<T>(in w, value);
    }

    /// <summary>
    /// Modifies a value of the specified type at the current position in the packet.
    /// </summary>
    public static void Modify<T>(this global::Xabbo.Messages.IPacket p, global::System.Func<T, T> modifier)
    {
        global::Xabbo.Messages.PacketWriter w = p.Writer();
        Modify<T>(in w, modifier);
    }

    /// <summary>
    /// Modifies a value of the specified type at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T>(this global::Xabbo.Messages.IPacket p, int pos, global::System.Func<T, T> modifier)
    {
        global::Xabbo.Messages.PacketWriter w = p.WriterAt(ref pos);
        Modify<T>(in w, modifier);
    }
}