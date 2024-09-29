using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents a packet of data with a message header.
/// </summary>
public sealed class Packet(Header header, ClientType client = ClientType.None, PacketBuffer? buffer = null) : IPacket
{
    public Header Header { get; set; } = header;
    public ClientType Client { get; set; } = client;
    public PacketBuffer Buffer { get; } = buffer ?? new PacketBuffer();
    public IParserContext? Context { get; set; }

    private int _position;
    public ref int Position => ref _position;
    public int Length => Buffer.Length;
    public int Available => Buffer.Length - Position;

    public Packet Copy() => new(Header, Client, Buffer.Copy());
    IPacket IPacket.Copy() => Copy();

    public void Clear()
    {
        Position = 0;
        Buffer.Clear();
    }

    /// <summary>
    /// Disposes of this packet's buffer.
    /// </summary>
    public void Dispose() => Buffer.Dispose();

    public PacketReader Reader() => new(this, ref _position, Context);
    public PacketReader ReaderAt(ref int pos) => new(this, ref pos, Context);
    public PacketWriter Writer() => new(this, ref _position, Context);
    public PacketWriter WriterAt(ref int pos) => new(this, ref pos, Context);

    public Span<byte> Allocate(int n) => Writer().Allocate(n);
    public ReadOnlySpan<byte> ReadSpan(int n) => Reader().ReadSpan(n);
    public void WriteSpan(ReadOnlySpan<byte> span) => Writer().WriteSpan(span);
    public string ReadContent() => Reader().ReadContent();
    public void WriteContent(string content) => Writer().WriteContent(content);
}
