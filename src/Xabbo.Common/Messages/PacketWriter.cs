using System;
using System.Buffers.Binary;
using System.Collections.Generic;
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
    private readonly ref int Pos = ref pos;
    public Header Header => Packet.Header;
    public ClientType Client => Packet.Header.Client;
    private Span<byte> Span => Packet.Buffer.Span;
    public int Length => Packet.Length;

    public PacketWriter(IPacket packet) : this(packet, ref packet.Position) { }

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
}