using System;
using System.Buffers.Binary;
using System.Linq;
using System.Text;

namespace Xabbo.Messages;

public readonly ref struct PacketReader
{
    private readonly IPacket Packet;
    private readonly ref int Pos;
    public Header Header => Packet.Header;
    public ClientType Client => Packet.Header.Client;
    private Span<byte> Span => Packet.Buffer.Span;
    public int Length => Packet.Length;
    public int Available => Packet.Length - Pos;

    public PacketReader(IPacket packet, ref int pos)
    {
        Packet = packet;
        Pos = ref pos;
    }

    public ReadOnlySpan<byte> ReadSpan(int n)
    {
        if (Pos + n > Span.Length)
            throw new Exception("Not enough bytes to read value.");
        Pos += n;
        return Span[(Pos - n)..Pos];
    }

    public T Read<T>()
    {
        if (typeof(T) == typeof(bool))
        {
            return (T)(object)(ReadSpan(1)[0] != 0);
        }
        else if (typeof(T) == typeof(byte))
        {
            return (T)(object)ReadSpan(1)[0];
        }
        else if (typeof(T) == typeof(short))
        {
            return Client switch
            {
                ClientType.Shockwave => (T)(object)(short)Read<B64>(),
                _ => (T)(object)BinaryPrimitives.ReadInt16BigEndian(ReadSpan(2)),
            };
        }
        else if (typeof(T) == typeof(int))
        {
            return Client switch
            {
                ClientType.Shockwave => (T)(object)(int)Read<VL64>(),
                _ => (T)(object)BinaryPrimitives.ReadInt32BigEndian(ReadSpan(4)),
            };
        }
        else if (typeof(T) == typeof(long))
        {
            if (Client == ClientType.Flash || Client == ClientType.Shockwave)
                throw new Exception($"Cannot read long on a {Enum.GetName(Client)} session.");
            return (T)(object)BinaryPrimitives.ReadInt64BigEndian(ReadSpan(8));
        }
        else if (typeof(T) == typeof(float))
        {
            return Client switch
            {
                ClientType.Unity => (T)(object)BinaryPrimitives.ReadSingleBigEndian(ReadSpan(4)),
                _ => (T)(object)float.Parse(Read<string>()),
            };
        }
        else if (typeof(T) == typeof(string))
        {
            if (Client == ClientType.Shockwave)
            {
                if (Header.Direction == Direction.In)
                {
                    if (Pos >= Span.Length)
                        throw new Exception("Not enough bytes to read value.");
                    int start = Pos, end;
                    int terminator = Span[Pos..].IndexOf<byte>(2);
                    if (terminator == -1)
                    {
                        Pos = end = Span.Length;
                    }
                    else
                    {
                        Pos = Pos + terminator + 1;
                        end = Pos - 1;
                    }
                    return (T)(object)Encoding.UTF8.GetString(Span[start..end]);
                }
                else if (Header.Direction != Direction.Out)
                {
                    throw new Exception("Unknown direction when reading string on Shockwave.");
                }
            }
            return (T)(object)Encoding.UTF8.GetString(ReadSpan(Read<short>()));
        }
        else if (typeof(T) == typeof(B64))
        {
            if (Client != ClientType.Shockwave)
                throw new Exception($"Cannot read B64 on session: {Client}.");
            return (T)(object)B64.Decode(ReadSpan(2));
        }
        else if (typeof(T) == typeof(VL64))
        {
            if (Client != ClientType.Shockwave)
                throw new Exception($"Cannot read VL64 on session: {Client}.");
            return (T)(object)VL64.Decode(ReadSpan(VL64.DecodeLength(Span[Pos])));
        }
        else if (typeof(T) == typeof(Id)) return Client switch
        {
            ClientType.Unity => (T)(object)(Id)Read<long>(),
            ClientType.Flash or ClientType.Shockwave => (T)(object)(Id)Read<int>(),
            _ => throw new Exception($"Cannot read Id on session: {Client}."),
        };
        else if (typeof(T) == typeof(Length)) return Client switch
        {
            ClientType.Unity => (T)(object)(Length)Read<short>(),
            _ => (T)(object)(Length)Read<int>(),
        };
        else if (typeof(T) == typeof(FloatString))
        {
            return (T)(object)(FloatString)float.Parse(Read<string>());
        }
        /*
         * if we could do this we wouldn't need a separate Parse<T>
         *
        else if (typeof(T) == typeof(IParser<T>)
        {
            return (T)(object)T.Parse(in this);
        }
        */
        else
        {
            throw new Exception($"Cannot read {typeof(T)} from packet.");
        }
    }

    public T[] ReadArray<T>()
    {
        T[] array = new T[Read<Length>()];
        for (int i = 0; i < array.Length; i++)
            array[i] = Read<T>();
        return array;
    }

    public T Parse<T>() where T : IParser<T> => T.Parse(in this);

    public T[] ParseArray<T>() where T : IParser<T>
    {
        T[] array = new T[Read<Length>()];
        for (int i = 0; i < array.Length; i++)
            array[i] = Parse<T>();
        return array;
    }

    public T[] ParseAll<T>() where T : IParser<T>, IManyParser<T> => T.ParseAll(in this).ToArray();
}