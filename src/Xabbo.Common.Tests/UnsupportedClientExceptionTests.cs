namespace Xabbo.Common.Tests;

public class UnsupportedClientExceptionTests
{
    [Theory]
    [InlineData(ClientType.None, ~ClientType.Unity, false)]
    [InlineData(ClientType.None, ~ClientType.Flash, false)]
    [InlineData(ClientType.None, ~ClientType.Shockwave, false)]
    [InlineData(ClientType.Unity, ClientType.Unity, true)]
    [InlineData(ClientType.Unity, ClientType.Flash, false)]
    [InlineData(ClientType.Unity, ClientType.Shockwave, false)]
    [InlineData(ClientType.Unity, ~ClientType.Unity, false)]
    [InlineData(ClientType.Unity, ~ClientType.Flash, true)]
    [InlineData(ClientType.Unity, ~ClientType.Shockwave, true)]
    [InlineData(ClientType.Flash, ClientType.Unity, false)]
    [InlineData(ClientType.Flash, ClientType.Flash, true)]
    [InlineData(ClientType.Flash, ClientType.Shockwave, false)]
    [InlineData(ClientType.Flash, ~ClientType.Unity, true)]
    [InlineData(ClientType.Flash, ~ClientType.Flash, false)]
    [InlineData(ClientType.Flash, ~ClientType.Shockwave, true)]
    [InlineData(ClientType.Shockwave, ClientType.Unity, false)]
    [InlineData(ClientType.Shockwave, ClientType.Flash, false)]
    [InlineData(ClientType.Shockwave, ClientType.Shockwave, true)]
    [InlineData(ClientType.Shockwave, ~ClientType.Unity, true)]
    [InlineData(ClientType.Shockwave, ~ClientType.Flash, true)]
    [InlineData(ClientType.Shockwave, ~ClientType.Shockwave, false)]
    public void TestThrowIf(ClientType client, ClientType clients, bool shouldThrow)
    {
        void action() => UnsupportedClientException.ThrowIf(client, clients);

        if (shouldThrow)
        {
            Assert.Throws<UnsupportedClientException>(action);
        }
        else
        {
            action();
        }
    }
}
