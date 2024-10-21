using System;

namespace Xabbo.Common.Tests;

public class LengthTests
{
    [Fact]
    public void TestEquality()
    {
        Length a = 5;
        Length b = 5;

        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a, b);
    }

    [Fact]
    public void TestComparability()
    {
        Length a = 5;
        Length b = 10;

        Assert.True(a < b);
        Assert.True(b > a);
        Assert.True(a.CompareTo(b) < 0);
        Assert.True(b.CompareTo(a) > 0);
        Assert.Throws<ArgumentException>(() => a.CompareTo("x"));
    }
}
