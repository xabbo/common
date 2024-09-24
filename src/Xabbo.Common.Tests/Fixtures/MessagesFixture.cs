using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;

namespace Xabbo.Common.Tests.Fixtures;

public class MessagesFixture : IAsyncLifetime
{
    public IMessageManager Messages { get; }

    public const short ItemAdd = 10;
    public const short MoveAvatar = 11;

    public const short Chat = 20;
    public const short Whisper = 21;
    public const short Shout = 22;

    public const short NonExistent = -1;

    public const short FlashObjects = 100;

    public MessagesFixture()
    {
        Messages = new MessageManager(@"./Resources/test_messages.ini") { Fetch = false };
    }

    public async Task InitializeAsync()
    {
        await Messages.InitializeAsync(CancellationToken.None);
        LoadMessages();
    }

    public void LoadMessages()
    {
        Messages.LoadMessages([
            // u:AddItem f:ItemAdd s:Items_2
            (ClientType.Flash, Direction.In, ItemAdd, "ItemAdd"),
            // us:move f:moveavatar
            (ClientType.Flash, Direction.Out, MoveAvatar, "MoveAvatar"),
            // chat messages
            (ClientType.Flash, Direction.In, Chat, "Chat"),
            (ClientType.Flash, Direction.In, Whisper, "Whisper"),
            (ClientType.Flash, Direction.In, Shout, "Shout"),
            // client message not defined in the message map file
            (ClientType.Flash, Direction.Out, NonExistent, "NonExistent"),
            // test identifier conflicts between f:Objects and s:Objects
            (ClientType.Flash, Direction.In, FlashObjects, "Objects"),
        ]);
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
