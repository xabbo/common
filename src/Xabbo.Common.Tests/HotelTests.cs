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

    [Theory]
    [InlineData("com")]
    [InlineData("es")]
    [InlineData("fi")]
    [InlineData("it")]
    [InlineData("nl")]
    [InlineData("de")]
    [InlineData("fr")]
    [InlineData("com.br")]
    [InlineData("com.tr")]
    public void DomainExists(string domain)
    {
        Assert.NotNull(Hotel.FromDomain(domain));
    }

    [Theory]
    [InlineData("habbo.com")]
    [InlineData("www.habbo.com")]
    [InlineData("www.habbo.es")]
    [InlineData("www.habbo.fi")]
    [InlineData("www.habbo.it")]
    [InlineData("www.habbo.nl")]
    [InlineData("www.habbo.de")]
    [InlineData("www.habbo.fr")]
    [InlineData("www.habbo.com.br")]
    [InlineData("www.habbo.com.tr")]
    [InlineData("sandbox.habbo.com")]
    public void HostExists(string host)
    {
        Assert.NotNull(Hotel.FromHost(host));
    }

    [Theory]
    [InlineData("game-es.habbo.com")]
    [InlineData("game-fi.habbo.com")]
    [InlineData("game-it.habbo.com")]
    [InlineData("game-nl.habbo.com")]
    [InlineData("game-de.habbo.com")]
    [InlineData("game-fr.habbo.com")]
    [InlineData("game-br.habbo.com")]
    [InlineData("game-tr.habbo.com")]
    [InlineData("game-s2.habbo.com")]
    public void GameHostExists(string gameHost)
    {
        Assert.NotNull(Hotel.FromGameHost(gameHost));
    }
}
