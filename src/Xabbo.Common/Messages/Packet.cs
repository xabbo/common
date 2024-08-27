using System;
using System.Buffers;
using System.Collections.Generic;

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
    /// Constructs a new packet with the specified protocol and message header.
    /// </summary>
    /// <param name="header">The message header.</param>
    public Packet(Header header)
        : this(header, new PacketBuffer())
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

    private PacketReader Reader(ref int pos) => new(this, ref pos);
    private PacketWriter Writer(ref int pos) => new(this, ref pos);

    public T Read<T>() => Reader(ref _position).Read<T>();
    public T ReadAt<T>(int pos) => Reader(ref pos).Read<T>();

    public T[] ReadArray<T>() => Reader(ref _position).ReadArray<T>();
    public T[] ReadArrayAt<T>(int pos) => Reader(ref pos).ReadArray<T>();

    public Span<byte> Allocate(int n) => Writer(ref _position).Allocate(n);

    public void Write(ReadOnlySpan<byte> span) => Writer(ref _position).Write(span);
    public void Write<T>(T value) => Writer(ref _position).Write(value);
    public void WriteAt<T>(int pos, T value) => Writer(ref pos).Write(value);

    public void Compose<T>(T value) where T : IComposer => Writer(ref _position).Compose(value);
    public void ComposeAt<T>(int pos, T value) where T : IComposer => Writer(ref pos).Compose(value);
    public void ComposeAll<T>(IEnumerable<T> values) where T : IComposer, IManyComposer<T> => Writer(ref _position).ComposeAll(values);
    public void ComposeAllAt<T>(int pos, IEnumerable<T> values) where T : IComposer, IManyComposer<T> => Writer(ref pos).ComposeAll(values);

    public void Replace<T>(T value) => Writer(ref _position).Replace(value);
    public void ReplaceAt<T>(int pos, T value) => Writer(ref pos).Replace(value);

    public void Modify<T>(Func<T, T> transform) => Writer(ref _position).Modify(transform);
    public void ModifyAt<T>(int pos, Func<T, T> transform) => Writer(ref pos).Modify(transform);

    public T Parse<T>() where T : IParser<T> => Reader(ref _position).Parse<T>();
    public T ParseAt<T>(int pos) where T : IParser<T> => Reader(ref pos).Parse<T>();

    public T[] ParseArray<T>() where T : IParser<T> => Reader(ref _position).ParseArray<T>();
    public T[] ParseArrayAt<T>(int pos) where T : IParser<T> => Reader(ref pos).ParseArray<T>();

    public T[] ParseAll<T>() where T : IParser<T>, IManyParser<T> => Reader(ref _position).ParseAll<T>();
    public T[] ParseAllAt<T>(int pos) where T : IParser<T>, IManyParser<T> => Reader(ref pos).ParseAll<T>();
}
