using Xabbo.Connection;
using Xabbo.Messages;
using Xabbo.Common.Tests.Data;
using Xabbo.Common.Tests.Fixtures;

namespace Xabbo.Common.Tests;

[Intercept]
partial class Asdf
{
    [Intercept]
    IMessage? Test(TestMsg msg)
    {
        return null;

    }
}

class TestMsg : IMessage<TestMsg>
{
    public static Identifier Identifier => throw new System.NotImplementedException();
    public static TestMsg Parse(in PacketReader p) => throw new System.NotImplementedException();
    public void Compose(in PacketWriter p) => throw new System.NotImplementedException();
}

public class ConnectionTests
{
    [Theory]
    [ClassData(typeof(Matrix<Clients, Directions>))]
    public void TestSend(ClientType client, Direction direction)
    {
        var mockExt = new Mock<IConnection>();
        mockExt.Setup(x => x.Session).Returns(new Session(Hotel.None, new Client(client)));

        mockExt.Setup(x => x.Send(It.IsAny<IPacket>())).Callback((IPacket p) => {
            Assert.Equal(direction, p.Header.Direction);
            Assert.Equal(MessagesFixture.Chat, p.Header.Value);
            p.Position = 0;
            Assert.False(p.Read<bool>()); // VL64: 1 byte
            Assert.Equal(1, p.Read<short>()); // B64: 2 bytes
            Assert.Equal(2, p.Read<int>()); // VL64: 1 byte
            Assert.Equal("3", p.Read<string>()); // Incoming: 2 bytes, Outgoing: 3 bytes
            Assert.Equal(4, (int)p.Read<Length>()); // VL64: 2 bytes
            Assert.Equal(5, p.Read<Id>()); // VL64: 2 bytes
            Assert.Equal(p.Client, client);
            Assert.Equal(p.Client switch
            {
                ClientType.Unity => 20,
                ClientType.Flash => 18,
                ClientType.Shockwave => p.Header.Direction switch
                {
                    Direction.In => 10,
                    Direction.Out => 11,
                    _ => -1
                },
                _ => -1
            }, p.Length);
        });

        Header header = new(direction, MessagesFixture.Chat);
        mockExt.Object.Send(header, false, (short)1, (int)2, "3", (Length)4, (Id)5);

        mockExt.Verify(x => x.Send(It.Is<IPacket>(p =>
            p.Header.Value == MessagesFixture.Chat
        )));
    }

}