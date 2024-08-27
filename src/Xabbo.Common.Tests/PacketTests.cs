using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xunit.Abstractions;

using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class PacketTests(ITestOutputHelper testOutputHelper)
{
    public static readonly object[] ClientTypes = [
        new object[] { ClientType.Unity },
        new object[] { ClientType.Flash },
        new object[] { ClientType.Shockwave },
    ];

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

    [Fact]
    public void TestReadWrite()
    {
        var packet = new Packet(Header.Unknown);

        packet.Write(true);
        packet.Write(false);
        packet.Write<byte>(254);
        packet.Write<short>(31337);
        packet.Write<int>(-123456789);
        packet.Write<float>(3.14f);
        packet.Write<long>(9876543210L);
        packet.Write<string>("hello, world");

        packet.Position = 0;

        Assert.True(packet.Read<bool>());
        Assert.False(packet.Read<bool>());
        Assert.Equal(254, packet.Read<byte>());
        Assert.Equal(31337, packet.Read<short>());
        Assert.Equal(-123456789, packet.Read<int>());
        Assert.Equal(3.14f, packet.Read<float>());
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
    [InlineData("hello", "world")]
    [InlineData("hello", "universe")]
    [InlineData("hello", "bye")]
    public void String_Replacement(string value, string replacement)
    {
        _logger.WriteLine($"Replacing \"{value}\" -> \"{replacement}\"");

        int valueByteCount = Encoding.UTF8.GetByteCount(value);
        int replacementByteCount = Encoding.UTF8.GetByteCount(replacement);

        var packet = new Packet((ClientType.Flash, Direction.In, 0));

        packet.Write(0x01020304);
        packet.Write(value);
        packet.Write(0x05060708);

        string hex = "";
        for (int i = 0; i < packet.Buffer.Length; i++)
            hex += $"{packet.Buffer.Span[i]:x02} ";
        _logger.WriteLine(hex);

        int previousLength = packet.Length;

        packet.Position = 4;
        packet.Replace(replacement);

        // The packet length has been adjusted
        Assert.Equal(previousLength + (replacementByteCount - valueByteCount), packet.Length);

        hex = "";
        for (int i = 0; i < packet.Buffer.Length; i++)
            hex += $"{packet.Buffer.Span[i]:x02} ";
        _logger.WriteLine(hex);

        // The values read back from the packet are correct
        packet.Position = 0;
        Assert.Equal(0x01020304, packet.Read<int>());
        Assert.Equal(replacement, packet.Read<string>());
        Assert.Equal(0x05060708, packet.Read<int>());

        // There is no more data in the packet
        Assert.Equal(packet.Length, packet.Position);
    }

    [Theory]
    [MemberData(nameof(ClientTypes))]
    public void TestWriteArray(ClientType client)
    {
        int[] array = Enumerable.Range(1, 10).ToArray();

        var packet = new Packet(Header.Unknown with { Client = client });

        packet.Write(array);

        packet.Position = 0;
        Assert.Equal(array.Length, (int)packet.Read<Length>());
        for (int i = 0; i < array.Length; i++)
            Assert.Equal(array[i], packet.Read<int>());
    }

    [Theory]
    [MemberData(nameof(ClientTypes))]
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
    [MemberData(nameof(ClientTypes))]
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
}