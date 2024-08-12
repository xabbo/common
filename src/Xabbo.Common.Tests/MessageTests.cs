using System;
using System.Linq;

using Xabbo.Messages;
using Xabbo.Common.Tests.Fixtures;

namespace Xabbo.Common.Tests;

public class MessageTests(MessagesFixture fixture) : IClassFixture<MessagesFixture>
{
    private readonly IMessageManager Messages = fixture.Messages;

    [Fact(DisplayName = "Identifier resolves to header with correct client type")]
    public void TestResolveClient() => Assert.Equal(
        Clients.Flash, Messages.Resolve((Clients.Unity, Direction.In, "AddItem")).Client
    );

    [Fact(DisplayName = "Unity identifier resolves to correct header value")]
    public void TestResolveUnityIdentifier() => Assert.Equal(
        MessagesFixture.ItemAdd, Messages.Resolve((Clients.Unity, Direction.In, "AddItem")).Value
    );

    [Fact(DisplayName = "Flash identifier resolves to correct header value")]
    public void TestResolveFlashIdentifier() => Assert.Equal(
        MessagesFixture.ItemAdd, Messages.Resolve((Clients.Flash, Direction.In, "ItemAdd")).Value
    );

    [Fact(DisplayName = "Shockwave identifier resolves to correct header value")]
    public void TestResolveShockwaveIdentifier() => Assert.Equal(
        MessagesFixture.ItemAdd, Messages.Resolve((Clients.Shockwave, Direction.In, "Items_2")).Value
    );

    [Fact(DisplayName = "Message identifiers are associated correctly")]
    public void TestMessageNameAssociation()
    {
        Assert.True(Messages.TryGetNames((Clients.Flash, Direction.In, "ItemAdd"), out var names));
        Assert.Equal("AddItem", names.Unity, ignoreCase: true);
        Assert.Equal("ItemAdd", names.Flash, ignoreCase: true);
        Assert.Equal("Items_2", names.Shockwave, ignoreCase: true);
    }

    [Fact(DisplayName = "Associated identifiers resolve to same header")]
    public void TestResolveAssociatedIdentifiers() => Assert.Collection(
        Messages.Resolve([
            (Clients.Unity, Direction.In, "AddItem"),
            (Clients.Flash, Direction.In, "ItemAdd"),
            (Clients.Shockwave, Direction.In, "Items_2"),
        ]),
        header => {
            Assert.Equal(Clients.Flash, header.Client);
            Assert.Equal(Direction.In, header.Direction);
            Assert.Equal(MessagesFixture.ItemAdd, header.Value);
        }
    );

    [Fact(DisplayName = "Multiple identifiers resolve to multiple headers")]
    public void TestResolveMultipleIdentifiers() => Assert.Collection(
        Messages.Resolve([
            (Clients.Flash, Direction.In, "ItemAdd"),
            (Clients.Flash, Direction.Out, "MoveAvatar"),
        ]).OrderBy(x => x.Value),
        header => Assert.Equal(header, (Clients.Flash, Direction.In, MessagesFixture.ItemAdd)),
        header => Assert.Equal(header, (Clients.Flash, Direction.Out, MessagesFixture.MoveAvatar))
    );

    [Fact(DisplayName = "Identifier with incorrect client fails to resolve")]
    public void TestWrongClientIdentifier() => Assert.Throws<UnresolvedIdentifiersException>(
        () => Messages.Resolve((Clients.Shockwave, Direction.In, "AddItem"))
    );

    [Fact(DisplayName = "Attempt to resolve identifier with multiple clients fails")]
    public void TestResolveMultipleClientIdentifier() => Assert.ThrowsAny<Exception>(
        () => Messages.Resolve((Clients.Unity | Clients.Flash, Direction.Out, "Move"))
    );

    [Fact(DisplayName = "Unresolved identifier throws correct exception")]
    public void TestUnresolvedIdentifier() =>
        Assert.Throws<UnresolvedIdentifiersException>(() => Messages.Resolve((Clients.Flash, Direction.In, "DisconnectReason")));

    [Fact(DisplayName = "Non-existent message in the message map file can still be resolved")]
    public void TestResolveNonexistentMessage() =>
        Assert.Equal(MessagesFixture.NonExistent, Messages.Resolve((Clients.Flash, Direction.Out, "NonExistent")).Value);

    [Fact(DisplayName = "Message names should still be available from the message map for unresolved identifiers")]
    public void TestMessagesExistForUnresolvedIdentifier()
    {
        Assert.True(Messages.TryGetNames((Clients.Flash, Direction.In, "DisconnectReason"), out MessageNames names));
        Assert.Equal("DisconnectionReason", names.Unity);
        Assert.Equal("DisconnectReason", names.Flash);
        Assert.Null(names.Shockwave);
    }
}
