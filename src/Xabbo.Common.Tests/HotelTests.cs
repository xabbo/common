namespace Xabbo.Common.Tests;

public class HotelTests
{
    [Theory]
    [InlineData("us")]
    [InlineData("es")]
    [InlineData("fi")]
    [InlineData("it")]
    [InlineData("nl")]
    [InlineData("de")]
    [InlineData("fr")]
    [InlineData("br")]
    [InlineData("tr")]
    [InlineData("s2")]
    public void IdentifierExists(string identifier)
    {
        Assert.NotNull(Hotel.FromIdentifier(identifier));
    }
}
