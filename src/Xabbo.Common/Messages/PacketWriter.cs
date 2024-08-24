using System;
using System.Buffers.Binary;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Xabbo.Messages;

public readonly ref struct PacketWriter
{
    private static string FormatFloatToString(float value)
        => value.ToString("0.0##############", CultureInfo.InvariantCulture);

    private readonly IPacket Packet;
    private readonly ref int Pos;
    public Header Header => Packet.Header;
    public ClientType Client => Packet.Header.Client;
    private Span<byte> Span => Packet.Buffer.Span;
    public int Length => Packet.Length;

    public PacketWriter(IPacket packet, ref int pos)
    {
        Packet = packet;
        Pos = ref pos;
    }

    public Span<byte> Alloc(int n)
    {
        Span<byte> buf = Packet.Buffer.Alloc(Pos, n);
        Pos += n;
        return buf;
    }

    private Span<byte> Resize(int pre, int post)
    {
        Span<byte> resized = Packet.Buffer.Resize(Pos..(Pos+pre), post);
        Pos += post;
        return resized;
    }

    public void Write(ReadOnlySpan<byte> span) => span.CopyTo(Alloc(span.Length));

    public void Write<T>(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        switch (value)
        {
            case IComposer v:
                v.Compose(in this);
                break;
            case bool v:
                Alloc(1)[0] = (byte)(v ? 1 : 0);
                break;
            case byte v:
                Alloc(1)[0] = v;
                break;
            case short v:
                if (Client == ClientType.Shockwave)
                    Write<B64>(v);
                else
                    BinaryPrimitives.WriteInt16BigEndian(Alloc(2), v);
                break;
            case int v:
                if (Client == ClientType.Shockwave)
                    Write<VL64>(v);
                else
                    BinaryPrimitives.WriteInt32BigEndian(Alloc(4), v);
                break;
            case long v:
                if (Client == ClientType.Flash || Client == ClientType.Shockwave)
                    throw new Exception($"Cannot write long on session: {Client}.");
                BinaryPrimitives.WriteInt64BigEndian(Alloc(8), v);
                break;
            case float v:
                if (Client == ClientType.Unity)
                    BinaryPrimitives.WriteSingleBigEndian(Alloc(4), v);
                else
                    Write(FormatFloatToString(v));
                break;
            case string v:
                int len = Encoding.UTF8.GetByteCount(v);
                if (Client == ClientType.Shockwave)
                {
                    if (Header.Direction == Direction.In)
                    {
                        Span<byte> span = Alloc(len+1);
                        Encoding.UTF8.GetBytes(v, span);
                        span[^1] = 0x02;
                        break;
                    }
                    else if (Header.Direction != Direction.Out)
                        throw new Exception("Unknown direction when writing string on Shockwave.");
                }
                Write((short)len);
                Encoding.UTF8.GetBytes(v, Alloc(len));
                break;
            case B64 v:
                if (Client != ClientType.Shockwave)
                    throw new Exception($"Cannot write B64 on session: {Client}.");
                B64.Encode(Alloc(2), v);
                break;
            case VL64 v:
                if (Client != ClientType.Shockwave)
                    throw new Exception($"Cannot write VL64 on session: {Client}.");
                VL64.Encode(Alloc(VL64.EncodeLength(v)), v);
                break;
            case Id v:
                switch (Client)
                {
                    case ClientType.Unity:
                        Write((long)v);
                        break;
                    case ClientType.Flash or ClientType.Shockwave:
                        Write((int)v);
                        break;
                    default:
                        throw new Exception($"Cannot write Id on session: {Client}.");
                }
                break;
            case Length v:
                switch (Client)
                {
                    case ClientType.Unity:
                        Write((short)v);
                        break;
                    default:
                        Write((int)v);
                        break;
                }
                break;
            case FloatString v:
                Write(FormatFloatToString(v.Value));
                break;
            case T[] v:
                Write<Length>(v.Length);
                for (int i = 0; i < v.Length; i++)
                    Write(v[i]);
                break;
            case IList<T> v:
                Write<Length>(v.Count);
                for (int i = 0; i < v.Count; i++)
                    Write(v[i]);
                break;
            case IEnumerable v:
                int start = Pos, n = 0;
                Write<Length>(0);
                foreach (object item in v)
                {
                    Write(item);
                    n++;
                }
                Packet.ReplaceAt<Length>(start, n);
                break;
            default:
                throw new Exception($"Cannot write {typeof(T)} to packet.");
        }
    }

    public void Replace<T>(T value)
    {
        ArgumentNullException.ThrowIfNull(value);

        switch (value)
        {
            case int v when Client == ClientType.Shockwave:
                Replace<VL64>(v);
                break;
            case float v when Client == ClientType.Flash || Client == ClientType.Shockwave:
                Replace(FormatFloatToString(v));
                break;
            case string v when Client == ClientType.Shockwave && Header.Direction == Direction.In:
                {
                    int end = Span[Pos..].IndexOf<byte>(2);
                    int newLen = Encoding.UTF8.GetByteCount(v);
                    if (end < 0) {
                        Encoding.UTF8.GetBytes(v, Resize(Packet.Buffer.Length - Pos, newLen));
                    } else {
                        Encoding.UTF8.GetBytes(v, Resize(end - Pos, newLen + 1)[..^1]);
                        Span[Pos-1] = 2;
                    }
                }
                break;
            case string v:
                {
                    int preLen = Packet.ReadAt<short>(Pos);
                    int postLen = Encoding.UTF8.GetByteCount(v);
                    Write((short)postLen);
                    Encoding.UTF8.GetBytes(v, Resize(preLen, postLen));
                }
                break;
            case VL64 v:
                VL64.Encode(Resize(VL64.DecodeLength(Span[Pos]), VL64.EncodeLength(v)), v);
                break;
            case Id v when Client == ClientType.Shockwave:
                Replace<VL64>((int)v);
                break;
            case Length v when Client == ClientType.Shockwave:
                Replace<VL64>((int)v);
                break;
            case IComposer:
                throw new Exception("Cannot replace an IComposer value.");
            default:
                Write(value);
                break;
        }
    }

    public void Modify<T>(Func<T, T> transform) => Replace(transform(Packet.ReadAt<T>(Pos)));
}