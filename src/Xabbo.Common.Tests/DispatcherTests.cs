using System;
using System.Runtime.CompilerServices;

using Xabbo.Messages;
using Xabbo.Interceptor;
using Xabbo.Extension;

using Xabbo.Common.Tests.Fixtures;
using Xabbo.Connection;

namespace Xabbo.Common.Tests;

public class DispatcherTests : IClassFixture<MessagesFixture>
{
    private static class Out
    {
        private static Identifier _([CallerMemberName] string? name = null)
            => (ClientType.Flash, Direction.Out, name ?? "");
        public static readonly Identifier MoveAvatar = _();
        public static readonly Identifier Nonexistent = _();
    }

    private static class In
    {
        private static Identifier _([CallerMemberName] string? name = null)
            => (ClientType.Flash, Direction.In, name ?? "");

        public static readonly Identifier Unresolved = _();

        public static readonly Identifier Chat = _();
        public static readonly Identifier Shout = _();
        public static readonly Identifier Whisper = _();
    }

    private MessagesFixture Fixture { get; }
    private IMessageManager Messages { get; }
    private IMessageDispatcher Dispatcher { get; }
    private IExtension Ext { get; }

    private Mock<IExtension> ExtMock { get; }

    public DispatcherTests(MessagesFixture fixture)
    {
        Fixture = fixture;
        Messages = fixture.Messages;

        var mockExt = new Mock<IExtension>();
        mockExt.Setup(x => x.Messages).Returns(Messages);
        mockExt.Setup(x => x.Dispatcher).Returns(() => Dispatcher!);
        ExtMock = mockExt;
        Ext = mockExt.Object;

        Dispatcher = new MessageDispatcher(Ext);

        SimulateConnect();
    }

    private void SimulateDisconnect()
    {
        ExtMock.Raise(x => x.Disconnected += null); // disconnect
    }

    private void SimulateConnect(ClientType client = ClientType.Flash)
    {
        Session session = new(Hotel.None, new Client(client, "", ""));
        ExtMock.Setup(x => x.Session).Returns(session);
        ExtMock.Raise(x => x.Connected += null, new GameConnectedArgs { Session = session });
    }

    private void SimulateReconnect(ClientType client = ClientType.Flash)
    {
        SimulateDisconnect();
        SimulateConnect(client);
    }

    private void DispatchN(Identifier identifier, int n)
    {
        for (int i = 0; i < n; i++)
            Dispatch(identifier);
    }

    private Intercept Dispatch(Identifier identifier) => Dispatch(new Packet(Messages.Resolve(identifier)));

    private Intercept Dispatch(IPacket packet)
    {
        Intercept intercept = new(Ext, packet);
        Dispatcher.Dispatch(intercept);
        return intercept;
    }

    [Fact(DisplayName = "Intercept should be correctly routed to registered handler")]
    public void TestIntercept()
    {
        var handler = new Mock<Action<Intercept>>();

        var registration = Ext.Intercept(In.Chat, handler.Object);

        Dispatch(In.Chat);

        registration.Dispose();

        Dispatch(In.Chat);

        // Verify that the handler was invoked only once, and not after the handler was removed.
        handler.Verify(e => e.Invoke(It.IsAny<Intercept>()), Times.Once);
    }

    [Fact(DisplayName = "Intercepting on Header.All should intercept all packets")]
    public void TestGlobalIntercept()
    {
        const int expectedCount = 5;

        var handler = new Mock<Action<Intercept>>();

        var registration = Ext.Intercept(Header.All, handler.Object);

        for (int i = 0; i < expectedCount; i++)
            Dispatcher.Dispatch(new(Ext, new Packet((ClientType.Flash, Direction.In, (short)i))));

        registration.Dispose();

        for (int i = 0; i < expectedCount; i++)
            Dispatcher.Dispatch(new(Ext, new Packet((ClientType.Flash, Direction.In, (short)i))));

        handler.Verify(e => e.Invoke(It.IsAny<Intercept>()), Times.Exactly(expectedCount));
    }

    [Fact(DisplayName = "Intercept successfully flags a packet as blocked")]
    public void TestBlock()
    {
        var handler = new Mock<Action<Intercept>>();

        Ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
            string msg = e.Packet.Read<string>();
            if (msg.Contains("block me"))
                e.Block();
        });

        var pkt = new Packet(Messages.Resolve(In.Shout));
        pkt.Write("please don't block me");

        var intercept = Dispatch(pkt);

        Assert.True(intercept.IsBlocked);
    }

    [Fact(DisplayName = "Modify correctly modifies packet inside an intercept handler")]
    public void TestModify()
    {
        var mockHandler = new Mock<Action<Intercept>>();

        Ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
            e.Packet.Read<int>();
            e.Packet.Modify<string>(s => s.Replace("apple", "orange"));
        });

        IPacket pkt = new Packet(Messages.Resolve(In.Shout));
        pkt.Write(1234, "I like apples");

        var intercept = Dispatch(pkt);
        pkt = intercept.Packet;

        pkt.Position = 0;
        pkt.Read<int>();

        Assert.Equal("I like oranges", pkt.Read<string>());
    }

    [Fact(DisplayName = "Dispatcher.Reset should clear intercept handleres")]
    public void TestDispatcherReset()
    {
        var handler = new Mock<Action<Intercept>>();

        Ext.Intercept(In.Chat, handler.Object);

        Dispatch(In.Chat);

        Dispatcher.Reset();

        Dispatch(In.Chat);

        handler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Once);
    }

    [Fact(DisplayName = "Persistent intercept should be reattached on reconnect")]
    public void TestPersistentIntercept()
    {
        var handler = new Mock<Action<Intercept>>();

        Ext.Intercept(In.Chat, handler.Object);

        DispatchN(In.Chat, 1); // o

        SimulateDisconnect();

        DispatchN(In.Chat, 2); // x

        SimulateConnect();

        DispatchN(In.Chat, 4); // o

        handler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Exactly(5));
    }

    [Fact(DisplayName = "Persistent intercept should detach immediately on disposal")]
    public void TestPersistentInterceptDisposable()
    {
        var handler = new Mock<Action<Intercept>>();

        var persistentIntercept = Ext.Intercept(In.Chat, handler.Object);

        DispatchN(In.Chat, 1); // o

        persistentIntercept.Dispose();

        DispatchN(In.Chat, 2); // x

        SimulateDisconnect();

        DispatchN(In.Chat, 4); // x

        SimulateConnect();

        DispatchN(In.Chat, 8); // x

        handler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Once);
    }

    [Fact(DisplayName = "Transient intercept should not persist after reconnect")]
    public void TestTransientIntercept()
    {
        SimulateConnect(ClientType.Flash);

        var handler = new Mock<Action<Intercept>>();

        Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(In.Chat, handler.Object)
        ]) { Transient = true });

        DispatchN(In.Chat, 1);

        SimulateReconnect();

        DispatchN(In.Chat, 2);

        handler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Once);
    }

    [Fact(DisplayName = "Unresolved required intercept should throw UnresolvedIdentifiersException")]
    public void TestRequiredIntercept()
    {
        SimulateConnect(ClientType.Flash);
        Assert.Throws<UnresolvedIdentifiersException>(() => {
            Dispatcher.Register(new InterceptGroup([
                new InterceptHandler(In.Chat, Mock.Of<Action<Intercept>>()),
                new InterceptHandler(In.Unresolved, Mock.Of<Action<Intercept>>()) {
                    Required = ClientType.Flash,
                },
            ]));
        });
    }

    [Fact(DisplayName = "Unresolved non-required intercept should be ignored")]
    public void TestUnrequiredIntercept()
    {
        SimulateConnect(ClientType.Flash);

        var handler = new Mock<Action<Intercept>>();

        Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(In.Chat, handler.Object),
            new InterceptHandler(In.Unresolved, handler.Object) { Required = ClientType.None },
            new InterceptHandler(In.Unresolved, handler.Object) { Required = ClientType.Shockwave },
        ]));

        Dispatch(In.Chat);

        handler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Once);
    }

    [Fact(DisplayName = "Only targeted handlers should be attached")]
    public void TestTargetedIntercept()
    {
        SimulateConnect(ClientType.Flash);

        var targetedHandler = new Mock<Action<Intercept>>();
        targetedHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        var untargetedHandler = new Mock<Action<Intercept>>();
        untargetedHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(In.Chat, targetedHandler.Object) { Target = ClientType.Flash },
            new InterceptHandler(In.Shout, targetedHandler.Object) { Target = ClientType.Flash | ClientType.Shockwave },
            new InterceptHandler(In.Whisper, untargetedHandler.Object) { Target = ClientType.Unity },
        ]));

        Dispatch(In.Chat);
        Dispatch(In.Shout);
        Dispatch(In.Whisper);

        targetedHandler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Exactly(2));
        untargetedHandler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Never);
    }
}
