using System;
using System.Runtime.CompilerServices;

using Xabbo.Messages;
using Xabbo.Interceptor;
using Xabbo.Extension;

using Xabbo.Common.Tests.Fixtures;
using Xabbo.Connection;
using Xunit.Abstractions;

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

    private ITestOutputHelper Log { get; }
    private MessagesFixture Fixture { get; }
    private IMessageManager Messages { get; }
    private IMessageDispatcher Dispatcher { get; }
    private IExtension Ext { get; }

    private Mock<IExtension> ExtMock { get; }

    public DispatcherTests(ITestOutputHelper output, MessagesFixture fixture)
    {
        Log = output;
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
        ExtMock.Raise(x => x.Connected += null, new ConnectedEventArgs { Session = session });
    }

    private void SimulateReconnect(ClientType client = ClientType.Flash)
    {
        SimulateDisconnect();
        SimulateConnect(client);
    }

    private void DispatchN(Identifier identifier, int n)
    {
        IPacket packet = new Packet(Messages.Resolve(identifier));
        bool block = false;
        for (int i = 0; i < n; i++)
            Dispatch(ref packet, ref block);
        packet.Dispose();
    }

    private void Dispatch(Header header)
    {
        IPacket packet = new Packet(header);
        bool block = false;
        Dispatch(ref packet, ref block);
        packet.Dispose();
    }

    private void Dispatch(Identifier identifier)
    {
        IPacket packet = new Packet(Messages.Resolve(identifier));
        bool block = false;
        Dispatch(ref packet, ref block);
        packet.Dispose();
    }

    private Intercept Dispatch(ref IPacket packet, ref bool block)
    {
        Intercept intercept = new(Ext, ref packet, ref block);
        Dispatcher.Dispatch(intercept);
        return intercept;
    }

    [Fact(DisplayName = "Intercept should be correctly routed to registered handler")]
    public void TestIntercept()
    {
        int invocations = 0;
        void callback(Intercept e) => invocations++;

        var registration = Ext.Intercept(In.Chat, callback);

        Dispatch(In.Chat);

        registration.Dispose();

        Dispatch(In.Chat);

        // Verify that the handler was invoked only once, and not after the handler was removed.
        Assert.Equal(1, invocations);
    }

    [Fact(DisplayName = "Intercepting on Header.All should intercept all packets")]
    public void TestGlobalIntercept()
    {
        const int expectedInvocations = 5;

        int actualInvocations = 0;
        void callback(Intercept e) => actualInvocations++;

        var registration = Ext.Intercept(Header.All, callback);

        for (int i = 0; i < expectedInvocations; i++)
            Dispatch((Direction.In, (short)i));

        registration.Dispose();

        for (int i = 0; i < expectedInvocations; i++)
            Dispatch((Direction.In, (short)i));

        Assert.Equal(actualInvocations, expectedInvocations);
    }

    [Fact(DisplayName = "Intercept successfully flags a packet as blocked")]
    public void TestBlock()
    {
        Ext.Intercept([In.Chat, In.Shout, In.Whisper], e =>
        {
            string msg = e.Packet.Read<string>();
            if (msg.Contains("block me"))
                e.Block();
        });

        IPacket packet = new Packet(Messages.Resolve(In.Shout));
        packet.Write("please don't block me");

        bool block = false;
        Dispatch(ref packet, ref block);

        Assert.True(block);
    }

    [Fact(DisplayName = "Modify correctly modifies packet inside an intercept handler")]
    public void TestModify()
    {
        Ext.Intercept([In.Chat, In.Shout, In.Whisper], e =>
        {
            e.Packet.Read<int>();
            e.Packet.Modify<string>(s => s.Replace("apple", "orange"));
        });

        IPacket packet = new Packet(Messages.Resolve(In.Shout));
        packet.Write(1234, "I like apples");

        bool block = false;
        var intercept = Dispatch(ref packet, ref block);

        packet.Position = 0;
        packet.Read<int>();

        Assert.Equal("I like oranges", packet.Read<string>());
    }

    [Fact(DisplayName = "Dispatcher.Reset should clear intercept handleres")]
    public void TestDispatcherReset()
    {
        int expectedInvocations = 1, actualInvocations = 0;
        void callback(Intercept e) => actualInvocations++;

        Ext.Intercept(In.Chat, callback);

        Dispatch(In.Chat);

        Dispatcher.Reset();

        Dispatch(In.Chat);

        Assert.Equal(expectedInvocations, actualInvocations);
    }

    [Fact(DisplayName = "Persistent intercept should be reattached on reconnect")]
    public void TestPersistentIntercept()
    {
        int expectedInvocations = 5, actualInvocations = 0;
        void callback(Intercept e) => actualInvocations++;

        Ext.Intercept(In.Chat, callback);

        DispatchN(In.Chat, 1); // o

        SimulateDisconnect();

        DispatchN(In.Chat, 2); // x

        SimulateConnect();

        DispatchN(In.Chat, 4); // o

        Assert.Equal(expectedInvocations, actualInvocations);
    }

    [Fact(DisplayName = "Persistent intercept should detach immediately on disposal")]
    public void TestPersistentInterceptDisposable()
    {
        int expectedInvocations = 1, actualInvocations = 0;
        void callback(Intercept e) => actualInvocations++;

        var persistentIntercept = Ext.Intercept(In.Chat, callback);

        DispatchN(In.Chat, 1); // o

        persistentIntercept.Dispose();

        DispatchN(In.Chat, 2); // x

        SimulateDisconnect();

        DispatchN(In.Chat, 4); // x

        SimulateConnect();

        DispatchN(In.Chat, 8); // x

        Assert.Equal(expectedInvocations, actualInvocations);
    }

    [Fact(DisplayName = "Transient intercept should not persist after reconnect")]
    public void TestTransientIntercept()
    {
        SimulateConnect(ClientType.Flash);

        int expectedInvocations = 1, actualInvocations = 0;
        void callback(Intercept e) => actualInvocations++;

        Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(In.Chat, callback)
        ]));

        DispatchN(In.Chat, 1);

        SimulateReconnect();

        DispatchN(In.Chat, 2);

        Assert.Equal(actualInvocations, expectedInvocations);
    }

    [Fact(DisplayName = "Only targeted handlers should be attached")]
    public void TestTargetedIntercept()
    {
        SimulateConnect(ClientType.Flash);

        int expectedTargetedInvocations = 2, actualTargetedInvocations = 0;
        void targetedCallback(Intercept e) => actualTargetedInvocations++;

        int expectedUntargetedInvocations = 0, actualUntargetedInvocations = 0;
        void untargetedCallback(Intercept e) => actualTargetedInvocations++;

        Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(In.Chat, targetedCallback) { Target = ClientType.Flash },
            new InterceptHandler(In.Shout, targetedCallback) { Target = ClientType.Flash | ClientType.Shockwave },
            new InterceptHandler(In.Whisper, untargetedCallback) { Target = ClientType.Unity },
        ]));

        Dispatch(In.Chat);
        Dispatch(In.Shout);
        Dispatch(In.Whisper);

        Assert.Equal(expectedTargetedInvocations, actualTargetedInvocations);
        Assert.Equal(expectedUntargetedInvocations, actualUntargetedInvocations);
    }

    [Fact(DisplayName = "Transient intercepts should throw when registered while disconnected")]
    public void TestTransientInterceptWhileDisonnected()
    {
        SimulateConnect(ClientType.Flash);

        Dispatcher.Register([new(In.Chat, _ => { })]);

        SimulateDisconnect();

        Assert.Throws<InvalidOperationException>(() =>
        {
            Dispatcher.Register([new(In.Chat, _ => { })]);
        });
    }

    public class MoveMsg(int x, int y) : IMessage<MoveMsg>
    {
        static Identifier IMessage<MoveMsg>.Identifier => Out.MoveAvatar;
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        void IComposer.Compose(in PacketWriter p)
        {
            if (p.Client == ClientType.Shockwave)
            {
                p.WriteB64((B64)X);
                p.WriteB64((B64)Y);
            }
            else
            {
                p.WriteInt(X);
                p.WriteInt(Y);
            }
        }
        static MoveMsg IParser<MoveMsg>.Parse(in PacketReader p)
        {
            int x, y;
            if (p.Client == ClientType.Shockwave)
            {
                x = p.ReadB64();
                y = p.ReadB64();
            }
            else
            {
                x = p.ReadInt();
                y = p.ReadInt();
            }
            return new MoveMsg(x, y);
        }
    }

    [Fact]
    public void TestIMessageImplementation()
    {
        var msg = new MoveMsg(3, 4);

        Header expectedHeader = Messages.Resolve(((IMessage<MoveMsg>)msg).GetIdentifier(ClientType.Flash));
        int expectedLength = 8;

        Ext.Send(msg);

        ExtMock.Verify(x => x.Send(It.IsAny<IPacket>()), Times.Once);
        ExtMock.Verify(x => x.Send(
            It.Is<IPacket>(p =>
                p.Header == expectedHeader &&
                p.Length == expectedLength
            ))
        );
    }
}
