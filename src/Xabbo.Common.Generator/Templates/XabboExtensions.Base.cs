[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
internal static partial class XabboExtensions
{
    // * To be generated *
    // private static T Read<T>(in PacketReader r);
    
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
                w.WriterAt(ref start).ReplaceLength(n);
                break;
            default: throw new global::System.NotSupportedException($"Cannot write value of type '{typeof(T)}'.");
        }
    }

    private static void Replace<T>(in global::Xabbo.Messages.PacketWriter w, T value)
    {
        switch (value)
        {
            case bool v: w.ReplaceBool(v); break;
            case byte v: w.ReplaceByte(v); break;
            case short v: w.ReplaceShort(v); break;
            case int v: w.ReplaceInt(v); break;
            case float v: w.ReplaceFloat(v); break;
            case long v: w.ReplaceLong(v); break;
            case string v: w.ReplaceString(v); break;
            case global::Xabbo.Length v: w.ReplaceLength(v); break;
            case global::Xabbo.Id v: w.ReplaceId(v); break;
            case global::Xabbo.Messages.B64 v: w.ReplaceB64(v); break;
            case global::Xabbo.Messages.VL64 v: w.ReplaceVL64(v); break;
            default: throw new global::System.NotSupportedException($"Cannot replace value of type '{typeof(T)}'.");
        }
    }
    
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
        global::Xabbo.Messages.PacketReader r = new global::Xabbo.Messages.PacketReader(p);
        return Read<T>(in r);
    }

    /// <summary>
    /// Reads a value of the specified type from the specified position in the packet.
    /// </summary>
    public static T ReadAt<T>(this global::Xabbo.Messages.IPacket p, int pos)
    {
        global::Xabbo.Messages.PacketReader r = new global::Xabbo.Messages.PacketReader(p, ref pos);
        return Read<T>(in r);
    }

    /// <summary>
    /// Writes a value of the specified type to the current position in the packet.
    /// </summary>
    public static void Write<T>(this global::Xabbo.Messages.IPacket p, T value)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p);
        Write<T>(in w, value);
    }
    
    /// <summary>
    /// Writes a value of the specified type to the specified position in the packet.
    /// </summary>
    public static void WriteAt<T>(this global::Xabbo.Messages.IPacket p, int pos, T value)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p, ref pos);
        Write<T>(in w, value);
    }
    
    /// <summary>
    /// Replaces a value of the specified type at the current position in the packet.
    /// </summary>
    public static void Replace<T>(this global::Xabbo.Messages.IPacket p, T value)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p);
        Replace<T>(in w, value);
    }

    /// <summary>
    /// Replaces a value of the specified type at the specified position in the packet.
    /// </summary>
    public static void ReplaceAt<T>(this global::Xabbo.Messages.IPacket p, int pos, T value)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p, ref pos);
        Replace<T>(in w, value);
    }

    /// <summary>
    /// Modifies a value of the specified type at the current position in the packet.
    /// </summary>
    public static void Modify<T>(this global::Xabbo.Messages.IPacket p, global::System.Func<T, T> modifier)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p);
        Modify<T>(in w, modifier);
    }

    /// <summary>
    /// Modifies a value of the specified type at the specified position in the packet.
    /// </summary>
    public static void ModifyAt<T>(this global::Xabbo.Messages.IPacket p, int pos, global::System.Func<T, T> modifier)
    {
        global::Xabbo.Messages.PacketWriter w = new global::Xabbo.Messages.PacketWriter(p, ref pos);
        Modify<T>(in w, modifier);
    }

    /// <summary>
    /// Sends an empty packet with the specified message header.
    /// </summary>
    public static void Send(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Header header)
    {
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        c.Send(p);
    }

    /// <summary>
    /// Sends an empty packet with the specified message identifier.
    /// </summary>
    public static void Send(this global::Xabbo.Connection.IConnection c, global::Xabbo.Messages.Identifier identifier)
    {
        global::Xabbo.Messages.Header header = c.Messages.Resolve(identifier);
        using global::Xabbo.Messages.Packet p = new global::Xabbo.Messages.Packet(header);
        c.Send(p);
    }
}