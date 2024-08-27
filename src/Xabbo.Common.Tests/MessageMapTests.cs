using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class MessageMapTests
{
    const string Xxxx = "x";
    const string Yyyy = "y";
    const string Zzzz = "z";

    [Theory]
    [InlineData(null, null, null, "")]
    // 1 name
    [InlineData(null, null, Xxxx, "s:x")]
    [InlineData(null, Xxxx, null, "f:x")]
    [InlineData(null, Xxxx, Xxxx, "fs:x")]
    [InlineData(Xxxx, null, null, "u:x")]
    [InlineData(Xxxx, null, Xxxx, "us:x")]
    [InlineData(Xxxx, Xxxx, null, "uf:x")]
    [InlineData(Xxxx, Xxxx, Xxxx, "ufs:x")]
    // 2 names
    [InlineData(Xxxx, Yyyy, null, "u:x f:y")]
    [InlineData(Xxxx, null, Yyyy, "u:x s:y")]
    [InlineData(null, Xxxx, Yyyy, "f:x s:y")]
    [InlineData(Xxxx, Xxxx, Yyyy, "uf:x s:y")]
    [InlineData(Xxxx, Yyyy, Xxxx, "us:x f:y")]
    [InlineData(Xxxx, Yyyy, Yyyy, "u:x fs:y")]
    // 3 names
    [InlineData(Xxxx, Yyyy, Zzzz, "u:x f:y s:z")]
    public void TestMessageNames(string? unity, string? flash, string? shockwave, string result)
    {
        var names = new MessageNames(unity, flash, shockwave);
        Assert.Equal(result, names.ToString());
    }
}