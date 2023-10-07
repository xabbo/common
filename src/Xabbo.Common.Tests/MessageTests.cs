using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class MessageTests : IClassFixture<MessagesFixture>
{
    private readonly IMessageManager Messages;

    public MessageTests(MessagesFixture messagesFixture)
    {
        Messages = messagesFixture.Messages;
    }

    [Fact(DisplayName = "Unity header value is read correctly")]
    public void CheckUnityHeaderValue() => Assert.Equal(3, Messages.In["Ok"].GetValue(ClientType.Unity));

    [Fact(DisplayName = "Flash header is merged correctly")]
    public void CheckFlashHeaderValue() => Assert.Equal(3, Messages.Out.Move.GetValue(ClientType.Flash));

    [Fact(DisplayName = "Property maps to correct header")]
    public void CheckPropertyMapping() => Assert.Equal(3, Messages.In.Ok.GetValue(ClientType.Unity));

    [Fact(DisplayName = "Property maps to same header as named indexer")]
    public void CheckPropertyIndexerMapping() => Assert.Equal(Messages.In.Ok, Messages.In["Ok"]);

    [Fact(DisplayName = "Flash / Unity names map to same header")]
    public void CheckUnityFlashNameMapping() => Assert.Equal(Messages.In["Ok"], Messages.In["AuthenticationOK"]);

    [Fact(DisplayName = "Named indexer is case insensitive")]
    public void CheckNameIndexerCaseInsensitive() => Assert.Equal(Messages.In["ok"], Messages.In["OK"]);

    [Fact(DisplayName = "Attempting to get value of unresolved header throws correct exception")]
    public void CheckUnresolvedHeader() => Assert.Throws<UnresolvedHeaderException>(
        () => Messages.In.DoorOut.GetValue(ClientType.Unity)
    );

    [Fact(DisplayName = "Attempting to get unknown header throws correct exception")]
    public void CheckUnknownHeader() => Assert.Throws<UnknownHeaderException>(
        () => Messages.In["UnknownHeader"]
    );

    [Fact(DisplayName = "Equivalent Unity/Flash messages access the same Header instance")]
    public void CheckHeaderEquivalence() => Assert.True(ReferenceEquals(Messages.In.Ok, Messages.In.AuthenticationOK));
}
