using System.Globalization;

namespace Xabbo.Common.Tests;

public class FloatStringTest
{
    [Theory]
    [ClassData(typeof(LocaleData))]
    public void ParseFloatString(CultureInfo cultureInfo)
    {
        CultureInfo.CurrentCulture = cultureInfo;
        Assert.Equal(1.0f, (float)(FloatString)"1.0");
    }
}