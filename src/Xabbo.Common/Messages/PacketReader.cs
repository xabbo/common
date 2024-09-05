using System;
using System.Buffers.Binary;
using System.Text;

namespace Xabbo.Messages;

/// <summary>
/// Provides primitive packet read operations.
/// </summary>
public readonly ref struct PacketReader(IPacket packet, ref int pos, IParserContext? context = null)
{
    private readonly IPacket Packet = packet;
    public readonly ref int Pos = ref pos;
    public IParserContext? Context => context;
    public Header Header => Packet.Header;
    public ClientType Client => Packet.Header.Client;
    public ReadOnlySpan<byte> Span => Packet.Buffer.Span;
    public int Length => Packet.Length;
    public int Available => Packet.Length - Pos;

    public PacketReader(IPacket packet) : this(packet, ref packet.Position) { }

    /// <summary>
    /// Gets the contents of the packet as a string.
    /// <para/>
    /// Only supported on Shockwave.
    /// </summary>
    public string Content
    {
        get
        {
            UnsupportedClientException.ThrowIfNoneOr(Header.Client, ~ClientType.Shockwave);
            return Encoding.UTF8.GetString(Packet.Buffer.Span);
        }
    }

    /// <summary>
    /// Reads a <see cref="Span{T}"/> of bytes of length <paramref name="n"/> from the current position and advances it.
    /// </summary>
    /// <param name="n">The number of bytes to read.</param>
    /// <exception cref="IndexOutOfRangeException">If the current position plus the specified length exceeds the length of the packet.</exception>
    public ReadOnlySpan<byte> ReadSpan(int n)
    {
        if (Pos + n > Span.Length)
            throw new IndexOutOfRangeException($"Cannot read past the packet length. Attempted to read {n} bytes from position {Pos} when length is {Length}.");
        Pos += n;
        return Span[(Pos - n)..Pos];
    }

    /// <summary>
    /// Reads a <see cref="bool"/> from the current position and advances it.
    /// <para/>
    /// Decoded as a <see cref="VL64"/> on Shockwave, otherwise as a <see cref="byte"/> .
    /// </summary>
    public bool ReadBool() => Client switch
    {
        ClientType.Shockwave => ReadVL64() != 0,
        _ => ReadSpan(1)[0] != 0
    };

    /// <summary>
    /// Reads a <see cref="byte"/> from the current position and advances it.
    /// <para/>
    /// Not supported on Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the <see cref="Client"/> is <see cref="ClientType.Shockwave"/>.</exception>
    public byte ReadByte() => Client switch
    {
        ClientType.Shockwave => throw new UnsupportedClientException(Client),
        _ => ReadSpan(1)[0]
    };

    /// <summary>
    /// Reads a <see cref="short"/> from the current position and advances it.
    /// <para/>
    /// Decoded as a <see cref="B64"/> on Shockwave, otherwise as a 16-bit integer.
    /// </summary>
    public short ReadShort() => Client switch
    {
        ClientType.Shockwave => ReadB64(),
        _ => BinaryPrimitives.ReadInt16BigEndian(ReadSpan(2)),
    };

    /// <summary>
    /// Reads a short array from the current position and advances it.
    /// </summary>
    public short[] ReadShortArray()
    {
        short[] array = new short[ReadLength()];
        for (int i = 0; i < array.Length; i++)
            array[i] = ReadShort();
        return array;
    }

    /// <summary>
    /// Reads an <see cref="int"/> from the current position and advances it.
    /// <para/>
    /// Decoded as a <see cref="VL64"/> on Shockwave, otherwise as a 32-bit integer.
    /// </summary>
    public int ReadInt() => Client switch
    {
        ClientType.Shockwave => ReadVL64(),
        _ => BinaryPrimitives.ReadInt32BigEndian(ReadSpan(4)),
    };

    /// <summary>
    /// Reads an int array from the current position and advances it.
    /// </summary>
    public int[] ReadIntArray()
    {
        int[] array = new int[ReadLength()];
        for (int i = 0; i < array.Length; i++)
            array[i] = ReadInt();
        return array;
    }

    /// <summary>
    /// Reads a <see cref="float"/> from the current position and advances it.
    /// <para/>
    /// Read as a <see cref="string"/> on Flash and Shockwave, otherwise decoded as a 32-bit floating point number.
    /// </summary>
    public float ReadFloat() => Client switch
    {
        ClientType.Flash or ClientType.Shockwave => float.Parse(ReadString()),
        _ => BinaryPrimitives.ReadSingleBigEndian(ReadSpan(4)),
    };

    /// <summary>
    /// Reads a <see cref="long"/> from the current position and advances it.
    /// <para/>
    /// Decoded as a 64-bit integer. Not supported on Flash or Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">
    /// If the <see cref="Client"/> is <see cref="ClientType.Flash"/> or <see cref="ClientType.Shockwave"/>.
    /// </exception>
    public long ReadLong() => Client switch
    {
        ClientType.Flash or ClientType.Shockwave => throw new UnsupportedClientException(Client),
        _ => BinaryPrimitives.ReadInt64BigEndian(ReadSpan(8)),
    };

    /// <summary>
    /// Reads a <see cref="string"/> from the current position and advances it.
    /// <para/>
    /// On Shockwave, when the header direction is incoming, it is decoded as a sequence of characters terminated by a <c>0x02</c> byte.
    /// <para/>
    /// Otherwise, it is decoded as a <see cref="short"/> length-prefixed UTF-8 <see cref="string"/>.
    /// </summary>
    public string ReadString()
    {
        if (Client == ClientType.Shockwave &&
            Header.Direction == Direction.In)
        {
            if (Pos >= Span.Length)
                throw new IndexOutOfRangeException("Attempted to read string at the end of the packet.");
            int start = Pos;
            int end = Span[Pos..].IndexOf<byte>(2);
            if (end == -1)
                throw new IndexOutOfRangeException("Attempted to read an unterminated string.");
            Pos += end + 1;
            return Encoding.UTF8.GetString(Span[start..(Pos - 1)]);
        }
        return Encoding.UTF8.GetString(ReadSpan(ReadShort()));
    }

    /// <summary>
    /// Reads a string array from the current position and advances it.
    /// </summary>
    public string[] ReadStringArray()
    {
        string[] array = new string[ReadLength()];
        for (int i = 0; i < array.Length; i++)
            array[i] = ReadString();
        return array;
    }

    /// <summary>
    /// Reads an <see cref="Id"/> from the current position and advances it.
    /// <para/>
    /// Read as a <see cref="long"/> on Unity, or an <see cref="int"/> on Flash and Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the client type is invalid.</exception>
    public Id ReadId() => Client switch
    {
        ClientType.Unity => ReadLong(),
        ClientType.Flash or ClientType.Shockwave => ReadInt(),
        _ => throw new UnsupportedClientException(Client),
    };

    /// <summary>
    /// Reads a <see cref="Length"/> from the current position and advances it.
    /// <para/>
    /// Read as a <see cref="short"/> on Unity, or an <see cref="int"/> on Flash and Shockwave.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the client type is invalid.</exception>
    public Length ReadLength() => Client switch
    {
        ClientType.Unity => ReadShort(),
        ClientType.Flash or ClientType.Shockwave => ReadInt(),
        _ => throw new UnsupportedClientException(Client),
    };

    /// <summary>
    /// Reads a <see cref="B64"/> from the current position and advances it.
    /// <para/>
    /// Not supported on Unity or Flash.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the <see cref="Client"/> is <see cref="ClientType.Unity"/> or <see cref="ClientType.Flash"/>.</exception>
    public B64 ReadB64() => Client switch
    {
        ClientType.Unity or ClientType.Flash => throw new UnsupportedClientException(Client),
        _ => B64.Decode(ReadSpan(2)),
    };

    /// <summary>
    /// Reads a <see cref="VL64"/> from the current position and advances it.
    /// <para/>
    /// Not supported on Unity or Flash.
    /// </summary>
    /// <exception cref="UnsupportedClientException">If the <see cref="Client"/> is <see cref="ClientType.Unity"/> or <see cref="ClientType.Flash"/>.</exception>
    public VL64 ReadVL64() => Client switch
    {
        ClientType.Unity or ClientType.Flash => throw new UnsupportedClientException(Client),
        _ => VL64.Decode(ReadSpan(VL64.DecodeLength(Span[Pos]))),
    };

    /// <summary>
    /// Parses a <typeparamref name="T"/> from the current position and advances it.
    /// </summary>
    public T Parse<T>() where T : IParser<T> => T.Parse(in this);

    /// <summary>
    /// Parses an array of <typeparamref name="T"/> from the current position and advances it.
    /// </summary>
    public T[] ParseArray<T>() where T : IParser<T>
    {
        T[] array = new T[ReadLength()];
        for (int i = 0; i < array.Length; i++)
            array[i] = Parse<T>();
        return array;
    }
}