using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Provides primitive packet write operations.
/// </summary>
public readonly ref struct PacketWriter(IPacket packet, ref int pos, IParserContext? context = null)
{
    private readonly IPacket Packet = packet;
    public readonly ref int Pos = ref pos;
    public readonly IParserContext? Context = context;
    public Header Header => Packet.Header;
    public ClientType Client => Packet.Client;
    public Span<byte> Span => Packet.Buffer.Span;
    public int Length => Packet.Length;
    public Encoding Encoding => Client is ClientType.Shockwave ? Encoding.Latin1 : Encoding.UTF8;

    public PacketWriter(IPacket packet) : this(packet, ref packet.Position) { }

    public PacketReader Reader() => new(Packet, ref Pos, Context);
    public PacketReader ReaderAt(ref int pos) => new(Packet, ref pos, Context);
    public PacketWriter WriterAt(ref int pos) => new(Packet, ref pos, Context);

    /// <summary>
    /// Allocates the specified number of bytes from the current position
    /// and returns the allocated range as a <see cref="Span{T}"/> of bytes.
    /// </summary>
    public Span<byte> Allocate(int n)
    {
        Span<byte> buf = Packet.Buffer.Allocate(Pos, n);
        Pos += n;
        return buf;
    }

    /// <summary>
    /// Resizes a range of bytes from the current position of length `<paramref name="pre"/>` to length `<paramref name="post"/>`
    /// and returns the resized range as a <see cref="Span{T}"/> of bytes.
    /// </summary>
    /// <param name="pre">The length to resize from.</param>
    /// <param name="post">The length to resize to</param>
    /// <returns>The resized range as a <see cref="Span{T}"/> of bytes.</returns>
    public Span<byte> Resize(int pre, int post)
    {
        Span<byte> resized = Packet.Buffer.Resize(Pos..(Pos + pre), post);
        Pos += post;
        return resized;
    }

    /// <summary>
    /// Writes the specified <see cref="Span{T}"/> of bytes to the current position.
    /// </summary>
    public void WriteSpan(ReadOnlySpan<byte> span) => span.CopyTo(Allocate(span.Length));

    /// <summary>
    /// Writes the specified <see cref="bool"/> value to the current position and advances it.
    /// <para/>
    /// Encoded as a <see cref="VL64"/> on Shockwave, otherwise as a <see cref="byte"/> .
    /// </summary>
    public void WriteBool(bool value)
    {
        if (Client == ClientType.Shockwave)
            WriteVL64(value ? 1 : 0);
        else
            WriteByte((byte)(value ? 1 : 0));
    }

    /// <summary>
    /// Writes the specified <see cref="byte"/> value to the current position and advances it.
    /// <para/>
    /// Not supported on Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the <see cref="Client"/> is <see cref="ClientType.Shockwave"/>.</exception>
    public void WriteByte(byte value)
    {
        if (Client == ClientType.Shockwave)
            throw new UnsupportedClientException(Client);
        Allocate(1)[0] = value;
    }

    /// <summary>
    /// Writes the specified <see cref="short"/> value to the current position and advances it.
    /// <para/>
    /// Encoded as a <see cref="B64"/> on Shockwave, otherwise as a 16-bit integer.
    /// </summary>
    public void WriteShort(short value)
    {
        if (Client == ClientType.Shockwave)
            WriteB64((B64)value);
        else
            BinaryPrimitives.WriteInt16BigEndian(Allocate(2), value);
    }

    /// <summary>
    /// Writes a short array to the current position and advances it.
    /// </summary>
    public void WriteShortArray(IEnumerable<short> values)
    {
        short[] array = (values as short[]) ?? [.. values];
        WriteLength((Length)array.Length);
        foreach (short value in array)
            WriteShort(value);
    }

    /// <summary>
    /// Writes the specified <see cref="int"/> value to the current position and advances it.
    /// <para/>
    /// Encoded as a <see cref="VL64"/> on Shockwave, otherwise as a 32-bit integer.
    /// </summary>
    public void WriteInt(int value)
    {
        if (Client == ClientType.Shockwave)
            WriteVL64(value);
        else
            BinaryPrimitives.WriteInt32BigEndian(Allocate(4), value);
    }

    /// <summary>
    /// Writes an int array to the current position and advances it.
    /// </summary>
    public void WriteIntArray(IEnumerable<int> values)
    {
        int[] array = (values as int[]) ?? [.. values];
        WriteLength((Length)array.Length);
        foreach (int value in array)
            WriteInt(value);
    }

    /// <summary>
    /// Writes the specified <see cref="float"/> value to the current position and advances it.
    /// <para/>
    /// Written as a <see cref="string"/> on Flash and Shockwave, otherwise encoded as a 32-bit floating point number.
    /// </summary>
    public void WriteFloat(float value)
    {
        switch (Client)
        {
            case ClientType.Flash or ClientType.Shockwave:
                WriteString((FloatString)value);
                break;
            default:
                BinaryPrimitives.WriteSingleBigEndian(Allocate(4), value);
                break;
        }
    }

    /// <summary>
    /// Writes the specified <see cref="long"/> value to the current position and advances it.
    /// <para/>
    /// Encoded as a 64-bit integer. Not supported on Flash or Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">
    /// If the <see cref="Client"/> is <see cref="ClientType.Flash"/> or <see cref="ClientType.Shockwave"/>.
    /// </exception>
    public void WriteLong(long value)
    {
        switch (Client)
        {
            case ClientType.Flash or ClientType.Shockwave:
                throw new UnsupportedClientException(Client);
            default:
                BinaryPrimitives.WriteInt64BigEndian(Allocate(8), value);
                break;
        }
    }

    /// <summary>
    /// Writes the specified <see cref="string"/> value to the current position and advances it.
    /// <para/>
    /// On Shockwave, when the header direction is incoming, it is encoded as a sequence of characters terminated by a <c>0x02</c> byte.
    /// <para/>
    /// Otherwise, it is encoded as a <see cref="short"/> length-prefixed UTF-8 <see cref="string"/>.
    /// </summary>
    /// <exception cref="ArgumentException">If the string length exceeds the maximum value of an unsigned 16-bit integer.</exception>
    /// <exception cref="ArgumentNullException">If the string is null.</exception>
    public void WriteString(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        int len = Encoding.GetByteCount(value);
        if (Client is ClientType.Shockwave)
        {
            if (len > B64.MaxValue)
                throw new ArgumentException($"String byte length ({len}) exceeds the maximum value ({B64.MaxValue}) of a {nameof(B64)}.", nameof(value));
        }
        else
        {
            if (len > ushort.MaxValue)
                throw new ArgumentException($"String byte length ({len}) exceeds the maximum value ({ushort.MaxValue}) of a {nameof(UInt32)}.", nameof(value));
        }

        Span<byte> span;
        if (Client == ClientType.Shockwave &&
            Header.Direction == Direction.In)
        {
            span = Allocate(len + 1);
            span[^1] = 0x02;
            span = span[..^1];
        }
        else
        {
            WriteShort((short)len);
            span = Allocate(len);
        }

        Encoding.GetBytes(value, span);
    }

    /// <summary>
    /// Writes a string array to the current position and advances it.
    /// </summary>
    public void WriteStringArray(IEnumerable<string> values)
    {
        string[] array = (values as string[]) ?? [.. values];
        WriteLength((Length)array.Length);
        foreach (string value in array)
            WriteString(value);
    }


    /// <summary>
    /// Writes the specified <see cref="Id"/> value to the current position and advances it.
    /// <para/>
    /// Written as a <see cref="long"/> on Unity, or an <see cref="int"/> on Flash and Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the client type is invalid.</exception>
    public void WriteId(Id value)
    {
        switch (Client)
        {
            case ClientType.Unity:
                WriteLong(value);
                break;
            case ClientType.Flash or ClientType.Shockwave:
                WriteInt((int)value);
                break;
            default:
                throw new UnsupportedClientException(Client);
        }
    }

    /// <summary>
    /// Writes an Id array to the current position and advances it.
    /// </summary>
    public void WriteIdArray(IEnumerable<Id> values)
    {
        Id[] array = (values as Id[]) ?? [.. values];
        WriteLength((Length)array.Length);
        foreach (Id value in array)
            WriteId(value);
    }

    /// <summary>
    /// Writes the specified <see cref="Length"/> value to the current position and advances it.
    /// <para/>
    /// Written as a <see cref="short"/> on Unity, or an <see cref="int"/> on Flash and Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the client type is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If the value is negative.</exception>
    public void WriteLength(Length value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative((int)value);

        switch (Client)
        {
            case ClientType.Unity:
                WriteShort((short)(ushort)value);
                break;
            default:
                WriteInt(value);
                break;
        }
    }

    /// <summary>
    /// Writes the specified <see cref="B64"/> value to the current position and advances it.
    /// <para/>
    /// Not supported on Unity or Flash.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the <see cref="Client"/> is <see cref="ClientType.Unity"/> or <see cref="ClientType.Flash"/>.</exception>
    public void WriteB64(B64 value)
    {
        UnsupportedClientException.ThrowIf(Client, ~ClientType.Shockwave);
        B64.Encode(Allocate(2), value);
    }

    /// <summary>
    /// Writes the specified <see cref="VL64"/> value to the current position and advances it.
    /// <para/>
    /// Not supported on Unity or Flash.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the <see cref="Client"/> is <see cref="ClientType.Unity"/> or <see cref="ClientType.Flash"/>.</exception>
    public void WriteVL64(VL64 value)
    {
        UnsupportedClientException.ThrowIf(Client, ~ClientType.Shockwave);
        VL64.Encode(Allocate(VL64.EncodeLength(value)), value);
    }

    /// <summary>
    /// Replaces the packet buffer with the specified content
    /// and advances the position to the end of the packet.
    /// The position must be at the start of the packet.
    /// <para/>
    /// Only supported on Shockwave.
    /// </summary>
    public void WriteContent(string content)
    {
        UnsupportedClientException.ThrowIfNoneOr(Client, ~ClientType.Shockwave);
        if (Pos != 0)
            throw new Exception("Cannot write content: position must be at the start of the packet.");
        Encoding.GetBytes(content, Resize(Length, Encoding.GetByteCount(content)));
    }

    /// <summary>
    /// Composes the specified <typeparamref name="T"/> to the current position and advances it.
    /// </summary>
    public void Compose<T>(T value) where T : IComposer => value.Compose(in this);

    /// <summary>
    /// Composes the specified array of <typeparamref name="T"/> to the current position and advances it.
    /// </summary>
    public void ComposeArray<T>(IEnumerable<T> values) where T : IComposer
    {
        T[] array = (values as T[]) ?? [.. values];
        WriteLength((Length)array.Length);
        foreach (T value in array)
            Compose(value);
    }

    public void ReplaceBool(bool value)
    {
        if (Client == ClientType.Shockwave)
            ReplaceVL64(value ? 1 : 0);
        else
            WriteBool(value);
    }

    public void ReplaceByte(byte value) => WriteByte(value);

    public void ReplaceShort(short value) => WriteShort(value);

    public void ReplaceInt(int value)
    {
        if (Client == ClientType.Shockwave)
            ReplaceVL64(value);
        else
            WriteInt(value);
    }

    public void ReplaceFloat(float value)
    {
        if (Client is ClientType.Flash or ClientType.Shockwave)
            ReplaceString((FloatString)value);
        else
            WriteFloat(value);
    }

    public void ReplaceLong(long value) => WriteLong(value);

    public void ReplaceString(string value)
    {
        if (Client is ClientType.Shockwave &&
            Header.Direction is Direction.In)
        {
            if (Pos >= Span.Length)
                throw new IndexOutOfRangeException("Attempted to replace string at the end of the packet.");
            int end = Span[Pos..].IndexOf<byte>(0x02);
            int newLen = Encoding.GetByteCount(value);
            if (end < 0)
                throw new IndexOutOfRangeException("Attempted to replace an unterminated string.");
            Encoding.GetBytes(value, Resize(end + 1, newLen + 1)[..^1]);
            Span[Pos - 1] = 0x02;
        }
        else
        {
            int start = Pos;
            int preLen = Reader().ReadShort();
            int postLen = Encoding.GetByteCount(value);
            Pos = start;
            WriteShort((short)postLen);
            Encoding.GetBytes(value, Resize(preLen, postLen));
        }
    }

    public void ReplaceLength(Length value)
    {
        if (Client is ClientType.Shockwave)
            ReplaceVL64((int)value);
        else
            WriteLength(value);
    }

    public void ReplaceId(Id value)
    {
        if (Client is ClientType.Shockwave)
            ReplaceVL64((int)value);
        else
            WriteId(value);
    }

    public void ReplaceB64(B64 value) => WriteB64(value);

    public void ReplaceVL64(VL64 value)
    {
        UnsupportedClientException.ThrowIf(Client, ~ClientType.Shockwave);
        VL64.Encode(Resize(VL64.DecodeLength(Span[Pos]), VL64.EncodeLength(value)), value);
    }

    public void ReplaceStruct<T>(T value) where T : IParserComposer<T>
    {
        // Save the start position.
        int start = Pos, end = Pos;
        // Parse the existing value to find the end position.
        ReaderAt(ref end).Parse<T>();
        // Now we have the size of the existing struct, which we can resize.
        int preSize = end - start;
        // Borrow the end of the buffer to compose the new value.
        start = Length; end = Length;
        WriterAt(ref end).Compose(value);
        int postSize = end - start;
        // Copy the new value into the resized range in place of the previous value.
        Span<byte> resized = Resize(preSize, postSize);
        Span[^postSize..].CopyTo(resized);
        // Resize the borrowed tail of the buffer back to zero.
        Packet.Buffer.Resize(^postSize.., 0);
    }
}