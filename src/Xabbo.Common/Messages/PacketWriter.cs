using System;
using System.Buffers.Binary;
using System.Globalization;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Provides primitive packet write operations.
/// </summary>
public readonly ref struct PacketWriter(IPacket packet, ref int pos)
{
    public static string FormatFloat(float value) => value.ToString("0.0##############", CultureInfo.InvariantCulture);

    private readonly IPacket Packet = packet;
    public readonly ref int Pos = ref pos;
    public Header Header => Packet.Header;
    public ClientType Client => Packet.Header.Client;
    public Span<byte> Span => Packet.Buffer.Span;
    public int Length => Packet.Length;

    public PacketWriter(IPacket packet) : this(packet, ref packet.Position) { }

    public PacketReader Reader() => new(Packet, ref Pos);
    public PacketReader ReaderAt(ref int pos) => new(Packet, ref pos);
    public PacketWriter Writer() => new(Packet, ref Pos);
    public PacketWriter WriterAt(ref int pos) => new(Packet, ref pos);

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
    private Span<byte> Resize(int pre, int post)
    {
        Span<byte> resized = Packet.Buffer.Resize(Pos..(Pos+pre), post);
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
            WriteB64(value);
        else
            BinaryPrimitives.WriteInt16BigEndian(Allocate(2), value);
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
    /// Writes the specified <see cref="float"/> value to the current position and advances it.
    /// <para/>
    /// Written as a <see cref="string"/> on Flash and Shockwave, otherwise encoded as a 32-bit floating point number.
    /// </summary>
    public void WriteFloat(float value)
    {
        switch (Client)
        {
            case ClientType.Flash or ClientType.Shockwave:
                WriteString(FormatFloat(value));
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

        int len = Encoding.UTF8.GetByteCount(value);
        if (len > ushort.MaxValue)
            throw new ArgumentException("Cannot write string: length is too long.", nameof(value));

        Span<byte> span;
        if (Client == ClientType.Shockwave &&
            Header.Direction == Direction.In)
        {
            span = Allocate(len+1);
            span[^1] = 0x02;
            span = span[..^1];
        }
        else
        {
            WriteShort((short)len);
            span = Allocate(len);
        }

        Encoding.UTF8.GetBytes(value, span);
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
                if (value > ushort.MaxValue)
                    throw new Exception("Cannot write Length on Unity: exceeds ushort.MaxValue");
                WriteShort((short)value);
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
        switch (Client)
        {
            case ClientType.Unity or ClientType.Flash:
                throw new UnsupportedClientException(Client);
            default:
                B64.Encode(Allocate(2), value);
                break;
        }
    }

    /// <summary>
    /// Writes the specified <see cref="VL64"/> value to the current position and advances it.
    /// <para/>
    /// Not supported on Unity or Flash.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the <see cref="Client"/> is <see cref="ClientType.Unity"/> or <see cref="ClientType.Flash"/>.</exception>
    public void WriteVL64(VL64 value)
    {
        switch (Client)
        {
            case ClientType.Unity or ClientType.Flash:
                throw new UnsupportedClientException(Client);
            default:
                VL64.Encode(Allocate(VL64.EncodeLength(value)), value);
                break;
        }
    }

    /// <summary>
    /// Writes the specified value of type <typeparamref name="T"/> to the current position and advances it.
    /// </summary>
    public void Compose<T>(T value) where T : IComposer => value.Compose(in this);

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
            ReplaceString(FormatFloat(value));
        else
            WriteFloat(value);
    }

    public void ReplaceLong(long value) => WriteLong(value);

    public void ReplaceString(string value)
    {
        if (Client is ClientType.Shockwave &&
            Header.Direction is Direction.In)
        {
            int end = Span[Pos..].IndexOf<byte>(0x02);
            int newLen = Encoding.UTF8.GetByteCount(value);
            if (end < 0) {
                Encoding.UTF8.GetBytes(value, Resize(Packet.Buffer.Length - Pos, newLen));
            } else {
                Encoding.UTF8.GetBytes(value, Resize(end - Pos, newLen + 1)[..^1]);
                Span[Pos-1] = 0x02;
            }
        }
        else
        {
            int start = Pos;
            int preLen = Reader().ReadShort();
            int postLen = Encoding.UTF8.GetByteCount(value);
            Pos = start;
            WriteShort((short)postLen);
            Encoding.UTF8.GetBytes(value, Resize(preLen, postLen));
        }
    }

    public void ReplaceLength(Length value)
    {
        if (Client is ClientType.Unity)
            WriteShort((short)value);
        else
            ReplaceInt(value);
    }

    public void ReplaceId(Id value)
    {
        if (Client is ClientType.Unity)
            WriteLong(value);
        else
            ReplaceInt((int)value);
    }

    public void ReplaceB64(B64 value) => WriteB64(value);

    public void ReplaceVL64(VL64 value) => VL64.Encode(Resize(VL64.DecodeLength(Span[Pos]), VL64.EncodeLength(value)), value);
    
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
        // Copy the new value into the resized range in place of the previous value.
        Span[start..end].CopyTo(Resize(preSize, end - start));
        // Resize the borrowed tail of the buffer back to zero.
        Packet.Buffer.Resize(start..end, 0);
    }
}