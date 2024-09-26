using System;
using System.Buffers;

namespace Xabbo.Messages;

/// <summary>
/// Represents a packet of data with a message header.
/// </summary>
public sealed class Packet(Header header, PacketBuffer buffer) : IPacket
{
    public Header Header { get; set; } = header;
    public PacketBuffer Buffer { get; } = buffer;
    public ClientType Client => Header.Client;
    public IParserContext? Context { get; set; }

    private int _position;
    public ref int Position => ref _position;
    public int Length => Buffer.Length;
    public int Available => Buffer.Length - Position;

    /// <summary>
    /// Constructs a new packet with the specified header and initial capacity.
    /// </summary>
    /// <param name="header">The message header.</param>
    /// <param name="capacity">The initial capacity in bytes.</param>
    public Packet(Header header, int capacity)
        : this(header, new PacketBuffer(capacity))
    { }

    /// <summary>
    /// Constructs a new packet with the specified header and data.
    /// </summary>
    public Packet(Header header, ReadOnlySpan<byte> data)
        : this(header, new PacketBuffer(data))
    { }

    /// <summary>
    /// Constructs a new packet with the specified header and data.
    /// </summary>
    public Packet(Header header, in ReadOnlySequence<byte> data)
        : this(header, new PacketBuffer(in data))
    { }

    /// <summary>
    /// Constructs a new packet with the specified header.
    /// </summary>
    /// <param name="header">The message header.</param>
    public Packet(Header header)
        : this(header, new PacketBuffer())
    { }

    /// <summary>
    /// Constructs a new packet.
    /// </summary>
    public Packet()
        : this(Header.Unknown, new PacketBuffer())
    { }

    public Packet Copy() => new(Header, Buffer.Copy());
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
