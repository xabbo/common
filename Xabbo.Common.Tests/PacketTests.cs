using System.Text;
using System.Linq;
using System.Collections.Generic;

using Xunit;

using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class PacketTests
{
    public PacketTests() { }

    [Fact]
    public void Read_Write()
    {
        IPacket packet = new Packet();

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
        IPacket packet = new Packet { Protocol = ClientType.Unity };

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

        IPacket packet = new Packet();

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
        int[] array = new[] { 1, 2, 3, 4 };

        IPacket packet = new Packet { Protocol = ClientType.Flash };

        packet.Write(array);

        packet.Position = 0;
        Assert.Equal(array.Length, packet.ReadInt());
        for (int i = 0; i < array.Length; i++)
            Assert.Equal(array[i], packet.ReadInt());
    }

    [Fact]
    public void Write_Enumerable()
    {
        static int transform(int n) => n * 2;

        IEnumerable<int> enumerable = Enumerable.Range(1, 10).Select(transform);

        IPacket packet = new Packet { Protocol = ClientType.Flash };
        packet.Write(enumerable);

        packet.Position = 0;
        Assert.Equal(enumerable.Count(), packet.ReadInt());
        foreach (int n in enumerable)
            Assert.Equal(n, packet.ReadInt());
    }

    [Fact]
    public void Read_List()
    {
        List<int> source = new() { 2, 4, 6, 8 };

        IPacket packet = new Packet { Protocol = ClientType.Flash };
        packet.WriteCollection(source);

        packet.Position = 0;
        List<int> list = packet.ReadList<int>();

        Assert.Equal(source, list);
    }
}