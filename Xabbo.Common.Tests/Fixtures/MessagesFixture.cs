using System.Threading;
using System.Threading.Tasks;

using Xabbo.Messages;
using Xabbo.Messages.Dispatcher;

namespace Xabbo.Common.Tests;

public class MessagesFixture : IAsyncLifetime
{
    public IMessageManager Messages { get; }

    public MessagesFixture()
    {
        Messages = new UnifiedMessageManager(@"resources\test_messages.ini") { AutoFetch = false };
    }

    public async Task InitializeAsync()
    {
        await Messages.InitializeAsync(CancellationToken.None);

        Messages.LoadMessages(new ClientMessageInfo[]
        {
            new()
            {
                Client = ClientType.Flash,
                Direction = Direction.Incoming,
                Header = 1,
                Name = "AuthenticationOK"
            },
            new()
            {
                Client = ClientType.Flash,
                Direction = Direction.Outgoing,
                Header = 2,
                Name = "InfoRetrieve"
            },
            new()
            {
                Client = ClientType.Flash,
                Direction = Direction.Outgoing,
                Header = 3,
                Name = "MoveAvatar"
            }
        });
    }

    public Task DisposeAsync() => Task.CompletedTask;
}
