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

    public MessagesFixture()
    {
        Messages = new MessageManager(@"Resources/test_messages.ini") { Fetch = false };
    }

    public async Task InitializeAsync()
    {
        await Messages.InitializeAsync(CancellationToken.None);

        Messages.LoadMessages([
            // u:AddItem f:ItemAdd s:Items_2
            (Clients.Flash, Direction.In, ItemAdd, "ItemAdd"),
            // us:move f:moveavatar
            (Clients.Flash, Direction.Out, MoveAvatar, "MoveAvatar"),
            // chat messages
            (Clients.Flash, Direction.In, Chat, "Chat"),
            (Clients.Flash, Direction.In, Whisper, "Whisper"),
            (Clients.Flash, Direction.In, Shout, "Shout"),
            // client message not defined in the message map file
            (Clients.Flash, Direction.Out, NonExistent, "NonExistent"),
        ]);
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
