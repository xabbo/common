using System;
using System.Buffers;
using System.Text;

namespace Xabbo.Messages;

public sealed class Packet(Header header, PacketBuffer buffer) : IPacket, IDisposable
{
    public Header Header { get; set; } = header;
    public PacketBuffer Buffer { get; } = buffer;
    public ClientType Client => Header.Client;

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

    public PacketReader Reader() => new(this, ref _position);
    public PacketReader ReaderAt(ref int pos) => new(this, ref pos);
    public PacketWriter Writer() => new(this, ref _position);
    public PacketWriter WriterAt(ref int pos) => new(this, ref pos);

    public string Content
    {
        get
        {
            UnsupportedClientException.ThrowIfNoneOr(Header.Client, ~ClientType.Shockwave);
            return Encoding.UTF8.GetString(Buffer.Span);
        }

        set
        {
            UnsupportedClientException.ThrowIfNoneOr(Header.Client, ~ClientType.Shockwave);
            Position = 0;
            Encoding.UTF8.GetBytes(value, Writer().Resize(Length, Encoding.UTF8.GetByteCount(value)));
        }
    }

    public ReadOnlySpan<byte> ReadSpan(int n) => Reader().ReadSpan(n);
    public void WriteSpan(ReadOnlySpan<byte> span) => Writer().WriteSpan(span);
    public Span<byte> Allocate(int n) => Writer().Allocate(n);
}
