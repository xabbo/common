using System;
using System.Linq;

using Xabbo.Messages;
using Xabbo.Common.Tests.Fixtures;

namespace Xabbo.Common.Tests;

public class MessageTests(MessagesFixture fixture) : IClassFixture<MessagesFixture>
{
    private readonly IMessageManager Messages = fixture.Messages;

    [Fact(DisplayName = "Unity identifier resolves to correct header value")]
    public void TestResolveUnityIdentifier() => Assert.Equal(
        MessagesFixture.ItemAdd, Messages.Resolve((ClientType.Unity, Direction.In, "AddItem")).Value
    );

    [Fact(DisplayName = "Flash identifier resolves to correct header value")]
    public void TestResolveFlashIdentifier() => Assert.Equal(
        MessagesFixture.ItemAdd, Messages.Resolve((ClientType.Flash, Direction.In, "ItemAdd")).Value
    );

    [Fact(DisplayName = "Shockwave identifier resolves to correct header value")]
    public void TestResolveShockwaveIdentifier() => Assert.Equal(
        MessagesFixture.ItemAdd, Messages.Resolve((ClientType.Shockwave, Direction.In, "Items_2")).Value
    );

    [Fact(DisplayName = "Message identifiers are associated correctly")]
    public void TestMessageNameAssociation()
    {
        Assert.True(Messages.TryGetNames((ClientType.Flash, Direction.In, "ItemAdd"), out var names));
        Assert.Equal("AddItem", names.Unity, ignoreCase: true);
        Assert.Equal("ItemAdd", names.Flash, ignoreCase: true);
        Assert.Equal("Items_2", names.Shockwave, ignoreCase: true);
    }

    [Fact(DisplayName = "Associated identifiers resolve to same header")]
    public void TestResolveAssociatedIdentifiers() => Assert.Collection(
        Messages.Resolve([
            (ClientType.Unity, Direction.In, "AddItem"),
            (ClientType.Flash, Direction.In, "ItemAdd"),
            (ClientType.Shockwave, Direction.In, "Items_2"),
        ]),
        header =>
        {
            Assert.Equal(Direction.In, header.Direction);
            Assert.Equal(MessagesFixture.ItemAdd, header.Value);
        }
    );

    [Fact(DisplayName = "Multiple identifiers resolve to multiple headers")]
    public void TestResolveMultipleIdentifiers() => Assert.Collection(
        Messages.Resolve([
            (ClientType.Flash, Direction.In, "ItemAdd"),
            (ClientType.Flash, Direction.Out, "MoveAvatar"),
        ]).OrderBy(x => x.Value),
        header => Assert.Equal(header, (Direction.In, MessagesFixture.ItemAdd)),
        header => Assert.Equal(header, (Direction.Out, MessagesFixture.MoveAvatar))
    );

    [Fact(DisplayName = "Identifier with incorrect client fails to resolve")]
    public void TestWrongClientIdentifier() => Assert.Throws<UnresolvedIdentifiersException>(
        () => Messages.Resolve((ClientType.Shockwave, Direction.In, "AddItem"))
    );

    [Fact(DisplayName = "Attempt to resolve identifier with multiple clients fails")]
    public void TestResolveMultipleClientIdentifier() => Assert.ThrowsAny<Exception>(
        () => Messages.Resolve((ClientType.Unity | ClientType.Flash, Direction.Out, "Move"))
    );

    [Fact(DisplayName = "Unresolved identifier throws correct exception")]
    public void TestUnresolvedIdentifier() =>
        Assert.Throws<UnresolvedIdentifiersException>(() => Messages.Resolve((ClientType.Flash, Direction.In, "DisconnectReason")));

    [Fact(DisplayName = "Non-existent message in the message map file can still be resolved")]
    public void TestResolveNonexistentMessage() =>
        Assert.Equal(MessagesFixture.NonExistent, Messages.Resolve((ClientType.Flash, Direction.Out, "NonExistent")).Value);

    [Fact(DisplayName = "Message names should still be available from the message map for unresolved identifiers")]
    public void TestMessagesExistForUnresolvedIdentifier()
    {
        Assert.True(Messages.TryGetNames((ClientType.Flash, Direction.In, "DisconnectReason"), out MessageNames names));
        Assert.Equal("DisconnectionReason", names.Unity);
        Assert.Equal("DisconnectReason", names.Flash);
        Assert.Null(names.Shockwave);
    }

    [Fact(DisplayName = "Nonspecific identifier should resolve correctly")]
    public void TestNonspecificIdentifier()
    {
        Header header = Messages.Resolve(new Identifier(ClientType.None, Direction.In, "Chat"));
        Assert.Equal(MessagesFixture.Chat, header.Value);
    }

    [Fact(DisplayName = "Ambiguous identifier should throw AmbiguousIdentifierException")]
    public void TestAmbiguousIdentifier()
    {
        // Flash:Objects should resolve
        Header header = Messages.Resolve((ClientType.Flash, Direction.In, "Objects"));
        Assert.Equal(MessagesFixture.FlashObjects, header.Value);

        // Shockwave:Objects should not resolve
        Assert.Throws<UnresolvedIdentifiersException>(() =>
        {
            Messages.Resolve((ClientType.Shockwave, Direction.In, "Objects"));
        });

        // None:Objects should throw
        Assert.Throws<AmbiguousIdentifierException>(() =>
        {
            Messages.Resolve((Direction.In, "Objects"));
        });
    }
}
