using System;
using System.Text;

using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class EncodingTests
{
    public static readonly object[] VL64TestCases = [
        new object[] { -1, "M" },
        new object[] { 0, "H" },
        new object[] { 1, "I" },
        new object[] { 2, "J" },
        new object[] { 3, "K" },
        new object[] { 4, "PA" },
        new object[] { 64, "PP" },
        new object[] { 250, "R~" },
        new object[] { 256, "X@A" },
        new object[] { -256, "\\@A" },
        new object[] { 1024, "X@D" },
        new object[] { 16384, "`@@A" },
        new object[] { -16384, "d@@A" },
        new object[] { 1048576, "h@@@A" },
        new object[] { -1048576, "l@@@A" },
    ];

    public static readonly object[] B64TestCases = [
        new object[] { 0, "@@" },
        new object[] { 1, "@A" },
        new object[] { 16, "@P" },
        new object[] { 256, "D@" },
        new object[] { 1337, "Ty" },
        new object[] { 2048, "`@" },
        new object[] { 4000, "~`" },
    ];

    public static readonly object[] VL64LengthTestCases = [
        new object[] { 0, 1 },
        new object[] { 1, 1 },
        new object[] { -1, 1 },
        new object[] { 2, 1 },
        new object[] { -2, 1 },
        new object[] { 3, 1 },
        new object[] { -3, 1 },
        new object[] { 4, 2 },
        new object[] { -4, 2 },
        new object[] { 128, 2 },
        new object[] { -128, 2 },
        new object[] { 255, 2 },
        new object[] { -255, 2 },
        new object[] { 256, 3 },
        new object[] { -256, 3 },
        new object[] { 8192, 3 },
        new object[] { -8192, 3 },
        new object[] { 16383, 3 },
        new object[] { -16383, 3 },
        new object[] { 16384, 4 },
        new object[] { -16384, 4 },
        new object[] { 1048575, 4 },
        new object[] { -1048575, 4 },
        new object[] { 1048576, 5 },
        new object[] { -1048576, 5 },
    ];

    [Theory]
    [MemberData(nameof(VL64LengthTestCases))]
    public void TestVL64Length(int value, int expectedLength)
    {
        Assert.Equal(expectedLength, VL64.EncodeLength(value));
    }

    [Theory]
    [MemberData(nameof(VL64TestCases))]
    public void TestVL64Encode(int value, string encoded)
    {
        int n = VL64.EncodeLength(value);
        Span<byte> buf = stackalloc byte[n];
        VL64.Encode(buf, value);

        Assert.Equal(encoded, Encoding.UTF8.GetString(buf));
    }

    [Theory]
    [MemberData(nameof(VL64TestCases))]
    public void TestVL64Decode(int value, string encoded)
    {
        Span<byte> buf = Encoding.UTF8.GetBytes(encoded);
        Assert.Equal(value, (int)VL64.Decode(buf));
    }

    [Theory]
    [MemberData(nameof(B64TestCases))]
    public void TestB64Encode(short value, string encoded)
    {
        Span<byte> buf = stackalloc byte[2];
        B64.Encode(buf, (B64)value);

        Assert.Equal(encoded, Encoding.UTF8.GetString(buf));
    }

    [Theory]
    [MemberData(nameof(B64TestCases))]
    public void TestB64Decode(short value, string encoded)
    {
        Span<byte> buf = Encoding.UTF8.GetBytes(encoded);
        Assert.Equal(value, (short)(ushort)B64.Decode(buf));
    }
}