using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class PacketTests
{
    public static readonly object[] ClientTypes = {
        new object[] { ClientType.Flash },
        new object[] { ClientType.Unity }
    };

    public PacketTests() { }

    [Fact]
    public void Read_Write()
    {
        IPacket packet = new Packet(Header.Unknown);

        packet.WriteBool(true);
        packet.WriteBool(false);
        packet.WriteByte(254);
        packet.WriteShort(31337);
        packet.WriteInt(-123456789);
        packet.WriteFloat(3.14f);
        packet.WriteLong(9876543210);
        packet.WriteString("hello, world");

        packet.Position = 0;

        Assert.True(packet.ReadBool());
        Assert.False(packet.ReadBool());
        Assert.Equal(254, packet.ReadByte());
        Assert.Equal(31337, packet.ReadShort());
        Assert.Equal(-123456789, packet.ReadInt());
        Assert.Equal(3.14f, packet.ReadFloat());
        Assert.Equal(9876543210, packet.ReadLong());
        Assert.Equal("hello, world", packet.ReadString());
    }

    [Fact]
    public void Read_Write_Generic()
    {
        IPacket packet = new Packet(Header.Unknown, ClientType.Unity);

        packet.Write(true, false, (byte)254, (short)31337, -123456789, 3.14f, 9876543210, "hello, world");

        packet.Position = 0;

        Assert.Equal(
            (true, false, (byte)254, (short)31337, -123456789, 3.14f, 9876543210, "hello, world"),
            packet.Read<bool, bool, byte, short, int, float, long, string>()
        );
    }

    [Theory]
    [InlineData("hello", "world")]
    [InlineData("hello", "universe")]
    [InlineData("hello", "bye")]
    public void String_Replacement(string value, string replacement)
    {
        int valueByteCount = Encoding.UTF8.GetByteCount(value);
        int replacementByteCount = Encoding.UTF8.GetByteCount(replacement);

        IPacket packet = new Packet(Header.Unknown);

        packet.WriteInt(1234);
        packet.WriteString(value);
        packet.WriteInt(5678);

        int previousLength = packet.Length;

        packet.Position = 4;
        packet.ReplaceString(replacement);

        // The packet length has been adjusted
        Assert.Equal(previousLength + (replacementByteCount - valueByteCount), packet.Length);

        // The values read back from the packet are correct
        packet.Position = 0;
        Assert.Equal(1234, packet.ReadInt());
        Assert.Equal(replacement, packet.ReadString());
        Assert.Equal(5678, packet.ReadInt());

        // There is no more data in the packet
        Assert.Equal(0, packet.Available);
    }

    [Fact]
    public void Write_Array()
    {
        int[] array = Enumerable.Range(1, 10).ToArray();

        IPacket packet = new Packet(Header.Unknown, ClientType.Flash);

        packet.Write(array);

        packet.Position = 0;
        Assert.Equal(array.Length, packet.ReadInt());
        for (int i = 0; i < array.Length; i++)
            Assert.Equal(array[i], packet.ReadInt());
    }

    [Theory]
    [MemberData(nameof(ClientTypes))]
    public void Read_Write_LegacyShort(ClientType client)
    {
        IPacket packet = new Packet(Header.Unknown, client);
        packet.WriteLegacyShort(0);

        int expectedBytes = client == ClientType.Flash ? 4 : 2;

        // Wrote the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Length);

        packet.Position = 0;
        packet.ReadLegacyShort();

        // Read the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Position);
    }

    [Theory]
    [MemberData(nameof(ClientTypes))]
    public void Read_Write_LegacyLong(ClientType client)
    {
        IPacket packet = new Packet(Header.Unknown, client);
        packet.WriteLegacyLong(0);

        int expectedBytes = client == ClientType.Flash ? 4 : 8;

        // Wrote the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Length);

        packet.Position = 0;
        packet.ReadLegacyLong();

        // Read the correct number of bytes.
        Assert.Equal(expectedBytes, packet.Position);
    }

    [Theory(DisplayName = "Packet.Write(IEnumerable<int>)")]
    [MemberData(nameof(ClientTypes))]
    public void Read_Write_Enumerable(ClientType client)
    {
        IEnumerable<int> e = Enumerable.Range(1, 10);

        IPacket packet = new Packet(Header.Unknown, client);
        packet.Write((IEnumerable)e);

        packet.Position = 0;
        Assert.Equal(e.Count(), packet.ReadLegacyShort());
        foreach (int n in e)
            Assert.Equal(n, packet.ReadInt());
    }

    [Theory]
    [MemberData(nameof(ClientTypes))]
    public void Read_Write_Collection(ClientType client)
    {
        List<int> source = new() { 2, 4, 6, 8 };

        IPacket packet = new Packet(Header.Unknown, client);
        packet.WriteCollection(source);

        packet.Position = 0;
        List<int> list = packet.ReadList<int>();

        Assert.Equal(source, list);
    }
}