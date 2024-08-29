using System;
using System.Linq;
using System.Text;

using Xunit.Abstractions;

using Xabbo.Messages;
using Xabbo.Common.Tests.Data;

namespace Xabbo.Common.Tests;

public class PacketTests(ITestOutputHelper testOutputHelper)
{
    const int Canary = 0x02f8a284;

    private readonly ITestOutputHelper _logger = testOutputHelper;

    [Fact]
    public void Clear()
    {
        var packet = new Packet((ClientType.Flash, Direction.Out, 0));

        for (int i = 0; i < 3; i++)
        {
            packet.Write(1, 2, 3, 4, 5, 6);

            Assert.Equal(24, packet.Length);
            Assert.Equal(24, packet.Buffer.Length);
            Assert.Equal(24, packet.Position);

            packet.Clear();

            Assert.Equal(0, packet.Length);
            Assert.Equal(0, packet.Position);
        }
    }
    
    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestReadWriteString(ClientType client, Direction direction)
    {
        var packet = new Packet(new Header(client, direction, 0));
        packet.Write("hello, world");
        packet.Position = 0;
        Assert.Equal("hello, world", packet.Read<string>());
    }
    
    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestReadWriteStruct(ClientType client, Direction direction)
    {
        var packet = new Packet(new Header(client, direction, 0));
        Tile tile = new(1, 2, 3.4f);
        packet.Write(tile);
        packet.Position = 0;
        Assert.Equal(tile, packet.Read<Tile>());
    }

    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestReadWrite(ClientType client, Direction direction)
    {
        var packet = new Packet(new Header(client, direction, 0));

        packet.Write(true);
        packet.Write(false);
        if (client != ClientType.Shockwave)
            packet.Write<byte>(254);
        packet.Write<short>(1337);
        packet.Write<int>(-123456789);
        packet.Write<float>(3.14f);
        if (client == ClientType.Unity)
            packet.Write<long>(9876543210L);
        packet.Write<string>("hello, world");

        packet.Position = 0;
        
        _logger.WriteLine(packet.Buffer.Span.HexDump());

        Assert.True(packet.Read<bool>());
        Assert.False(packet.Read<bool>());
        if (client != ClientType.Shockwave)
            Assert.Equal(254, packet.Read<byte>());
        Assert.Equal(1337, packet.Read<short>());
        Assert.Equal(-123456789, packet.Read<int>());
        Assert.Equal(3.14f, packet.Read<float>());
        if (client == ClientType.Unity)
            Assert.Equal(9876543210, packet.Read<long>());
        Assert.Equal("hello, world", packet.Read<string>());
    }

    [Fact]
    public void Read_Write_Generic()
    {
        IPacket packet = new Packet((ClientType.Unity, Direction.Out, 0));

        packet.Write(true, false, (byte)254, (short)31337, -123456789, 3.14f, 9876543210, "hello, world");

        packet.Position = 0;

        Assert.Equal(
            (true, false, (byte)254, (short)31337, -123456789, 3.14f, 9876543210, "hello, world"),
            packet.Read<bool, bool, byte, short, int, float, long, string>()
        );
    }
    
    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestParserComposer(ClientType client, Direction direction)
    {
        Packet packet = new(new Header(client, direction, 0));        

        packet.Write(new Tile(1, 2, 3.4f));
        
        packet.Position = 0;
        var tile = packet.Read<Tile>();
        Assert.Equal(1, tile.X);
        Assert.Equal(2, tile.Y);
        Assert.Equal(3.4f, tile.Z);
    }

    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions, StringReplacements>))]
    public void String_Replacement(ClientType client, Direction direction, string value, string replacement)
    {
        int valueByteCount = Encoding.UTF8.GetByteCount(value);
        int replacementByteCount = Encoding.UTF8.GetByteCount(replacement);

        var packet = new Packet((client, direction, 0));

        packet.Write(Canary);
        packet.Write(value);
        packet.Write(Canary);

        int previousLength = packet.Length;

        packet.Position = 0;
        packet.Read<int>();
        packet.Replace(replacement);

        // The packet length has been adjusted
        Assert.Equal(previousLength + (replacementByteCount - valueByteCount), packet.Length);

        // The values read back from the packet are correct
        packet.Position = 0;
        Assert.Equal(Canary, packet.Read<int>());
        Assert.Equal(replacement, packet.Read<string>());
        Assert.Equal(Canary, packet.Read<int>());

        // There is no more data in the packet
        Assert.Equal(packet.Length, packet.Position);
    }
    
    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestReplaceStruct(ClientType client, Direction direction)
    {
        Packet p = new(new Header(client, direction, 0));

        p.Write(Canary);
        p.Write(new Tile(1, 2, 3));
        p.Write(Canary);
        
        p.Position = 0;
        p.Read<int>();
        p.Writer().ReplaceStruct(new Tile(4, 5, 6));
        
        p.Position = 0;
        Assert.Equal(Canary, p.Read<int>());
        Tile actual = p.Read<Tile>();
        Assert.Equal(Canary, p.Read<int>());
        
        Assert.Equal(4, actual.X);
        Assert.Equal(5, actual.Y);
        Assert.Equal(6, actual.Z);
        Assert.Equal(p.Length, p.Position);
    }

    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestReadWritePrimitiveArray(ClientType client, Direction direction)
    {
        int[] writeArray = Enumerable.Range(1, 10).ToArray();

        var packet = new Packet(new Header(client, direction, 0));
        packet.Write(writeArray);

        int[] readArray = packet.ReadAt<int[]>(0);
        
        Assert.True(writeArray.SequenceEqual(readArray));
    }
    
    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestReadWriteStructArray(ClientType client, Direction direction)
    {
        Tile[] array1 = [new(1,2,3), new(4,5,6), new(7,8,9)];

        var packet = new Packet(new Header(client, direction, 0));
        packet.Write(array1);

        packet.Position = 0;
        Tile[] array2 = packet.Read<Tile[]>();
        
        Assert.True(array1.SequenceEqual(array2));
    }

    [Theory]
    [ClassData(typeof(Clients))]
    public void TestReadWriteLength(ClientType client)
    {
        var packet = new Packet(Header.Unknown with { Client = client });
        packet.Write<Length>(0);

        int expectedBytes = client switch {
            ClientType.Unity => 2, // short
            ClientType.Flash => 4, // int
            ClientType.Shockwave => 1, // VL64
            _ => throw new Exception("Invalid client"),
        };

        // Wrote the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Length);

        packet.Position = 0;
        packet.Read<Length>();

        // Read the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Position);
    }

    [Theory]
    [ClassData(typeof(Clients))]
    public void TestReadWriteId(ClientType client)
    {
        var packet = new Packet(Header.Unknown with { Client = client });
        packet.Write<Id>(0);

        int expectedBytes = client switch {
            ClientType.Unity => 8, // long
            ClientType.Flash => 4, // int
            ClientType.Shockwave => 1, // VL64
            _ => throw new Exception("Invalid client"),
        };

        // Wrote the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Length);

        packet.Position = 0;
        packet.Read<Id>();

        // Read the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Position);
    }

    [Fact]
    public void TestShockwaveContent()
    {
        Packet packet = new((ClientType.Shockwave, Direction.None, 0))
        {
            Content = "hello, world"
        };

        Assert.Equal(packet.Length, packet.Position);
        Assert.Equal("hello, world", packet.Content);
        Assert.Equal(12, packet.Length);
        
        packet.Content = "this is a test";
        Assert.Equal("this is a test", packet.Content);
        Assert.Equal(14, packet.Length);

        packet.Content = "";
        Assert.Equal("", packet.Content);
        Assert.Equal(0, packet.Length);
        
        packet.Header = packet.Header with { Client = ClientType.Flash };
        Assert.Throws<UnsupportedClientException>(() => packet.Content);
        Assert.Throws<UnsupportedClientException>(() => packet.Content = "test");
    }
    
}