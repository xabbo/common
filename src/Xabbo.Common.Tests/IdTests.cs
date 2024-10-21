using System;

namespace Xabbo.Common.Tests;

public class IdTests
{
    [Fact]
    public void TestEquality()
    {
        Id a = 5;
        Id b = 5;

        Assert.True(a.Equals(b));
        Assert.True(a == b);
        Assert.False(a != b);
        Assert.Equal(a, b);
    }

    [Fact]
    public void TestComparability()
    {
        Id a = 5;
        Id b = 10;

        Assert.True(a < b);
        Assert.True(b > a);
        Assert.True(a.CompareTo(b) < 0);
        Assert.True(b.CompareTo(a) > 0);
        Assert.Throws<ArgumentException>(() => a.CompareTo("x"));
    }
}
