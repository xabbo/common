using System;
using System.Runtime.CompilerServices;

using Xabbo.Messages;
using Xabbo.Interceptor;
using Xabbo.Extension;

using Xabbo.Common.Tests.Fixtures;

namespace Xabbo.Common.Tests;

public class DispatcherTests : IClassFixture<MessagesFixture>
{
    private static class Out
    {
        private static Identifier _([CallerMemberName] string? name = null)
            => (ClientType.Flash, Direction.Out, name ?? "");
        public static Identifier MoveAvatar { get; } = _();
    }

    private static class In
    {
        private static Identifier _([CallerMemberName] string? name = null)
            => (ClientType.Flash, Direction.In, name ?? "");

        public static Identifier Chat { get; } = _();
        public static Identifier Shout { get; } = _();
        public static Identifier Whisper { get; } = _();
    }

    private readonly MessagesFixture _fixture;
    private IMessageManager Messages { get; }
    private IMessageDispatcher Dispatcher { get; }
    private IExtension Ext { get; }

    public DispatcherTests(MessagesFixture fixture)
    {
        _fixture = fixture;
        Messages = fixture.Messages;
        Dispatcher = new MessageDispatcher(Messages);

        var mockExt = new Mock<IExtension>();
        mockExt.Setup(x => x.Messages).Returns(Messages);
        mockExt.Setup(x => x.Dispatcher).Returns(Dispatcher);
        Ext = mockExt.Object;
    }

    [Fact(DisplayName = "Intercept should be correctly routed to registered handler")]
    public void TestIntercept()
    {
        var mockHandler = new Mock<Action<Intercept>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        var registration = Ext.Intercept(Out.MoveAvatar, mockHandler.Object);

        var packet = new Packet(Messages.Resolve(Out.MoveAvatar));
        var intercept = new Intercept(Ext, packet);

        Dispatcher.Dispatch(intercept);

        registration.Dispose();

        Dispatcher.Dispatch(intercept);
        Dispatcher.Dispatch(intercept);

        // Verify that the handler was invoked only once, and not after the handler was removed.
        mockHandler.Verify(e => e.Invoke(It.IsAny<Intercept>()), Times.Once);
    }

    [Fact(DisplayName = "Intercepting on Header.All should intercept all packets")]
    public void TestGlobalIntercept()
    {
        const int expectedCount = 5;

        var mockHandler = new Mock<Action<Intercept>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        var registration = Ext.Intercept(Header.All, mockHandler.Object);

        for (int i = 0; i < expectedCount; i++)
            Dispatcher.Dispatch(new(Ext, new Packet((ClientType.Flash, Direction.In, (short)i))));

        registration.Dispose();

        for (int i = 0; i < expectedCount; i++)
            Dispatcher.Dispatch(new(Ext, new Packet((ClientType.Flash, Direction.In, (short)i))));

        // Verify that the handler was invoked only once, and not after the handler was removed.
        mockHandler.Verify(e => e.Invoke(It.IsAny<Intercept>()), Times.Exactly(expectedCount));
    }

    [Fact(DisplayName = "Intercept successfully flags a packet as blocked")]
    public void TestBlock()
    {
        var mockHandler = new Mock<Action<Intercept>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        var mockInterceptor = new Mock<IInterceptor>();
        mockInterceptor.Setup(x => x.Dispatcher).Returns(Dispatcher);
        mockInterceptor.Setup(x => x.Messages).Returns(Messages);

        var ext = mockInterceptor.Object;

        ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
            string msg = e.Packet.Read<string>();
            if (msg.Contains("block me"))
                e.Block();
        });

        var pkt = new Packet(Messages.Resolve(In.Shout));
        pkt.Write("please don't block me");

        var intercept = new Intercept(ext, pkt);
        Dispatcher.Dispatch(intercept);

        Assert.True(intercept.IsBlocked);
    }


    [Fact(DisplayName = "Modify correctly modifies packet inside an intercept handler")]
    public void TestModify()
    {
        var mockHandler = new Mock<Action<Intercept>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        var mockInterceptor = new Mock<IInterceptor>();
        mockInterceptor.Setup(x => x.Dispatcher).Returns(Dispatcher);
        mockInterceptor.Setup(x => x.Messages).Returns(Messages);

        var ext = mockInterceptor.Object;

        ext.Intercept([In.Chat, In.Shout, In.Whisper], e => {
            e.Packet.Read<int>();
            e.Packet.Modify<string>(s => s.Replace("apple", "orange"));
        });

        var pkt = new Packet(ext.Messages.Resolve(In.Shout));
        pkt.Write(1234, "I like apples");

        var intercept = new Intercept(ext, pkt);
        ext.Dispatcher.Dispatch(intercept);

        pkt.Position = 0;
        pkt.Read<int>();

        Assert.Equal("I like oranges", pkt.Read<string>());
    }

    [Fact(DisplayName = "Dispatcher.Reset should clear intercept handleres")]
    public void TestDispatcherReset()
    {
        var mockHandler = new Mock<Action<Intercept>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        Ext.Intercept(Out.MoveAvatar, mockHandler.Object);

        var pkt = new Packet(Messages.Resolve(Out.MoveAvatar));
        Dispatcher.Dispatch(new Intercept(Ext, pkt));

        Dispatcher.Reset();

        Dispatcher.Dispatch(new Intercept(Ext, pkt));

        mockHandler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Exactly(1));
    }

    [Fact(DisplayName = "Persistent intercept should be reattached on reload of messages")]
    public void TestPersistentIntercept()
    {
        var mockHandler = new Mock<Action<Intercept>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        Ext.Intercept(Out.MoveAvatar, mockHandler.Object);

        var pkt = new Packet(Messages.Resolve(Out.MoveAvatar));
        Dispatcher.Dispatch(new Intercept(Ext, pkt));

        Dispatcher.Reset();
        _fixture.LoadMessages();

        Dispatcher.Dispatch(new Intercept(Ext, pkt));

        mockHandler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Exactly(2));
    }

    [Fact(DisplayName = "Transient intercept should not persist after reload of messages")]
    public void TestTransientIntercept()
    {
        var mockHandler = new Mock<Action<Intercept>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<Intercept>()));

        Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(Out.MoveAvatar, mockHandler.Object)
        ]) { Transient = true });

        var pkt = new Packet(Messages.Resolve(Out.MoveAvatar));
        Dispatcher.Dispatch(new Intercept(Ext, pkt));

        Dispatcher.Reset();
        _fixture.LoadMessages();

        Dispatcher.Dispatch(new Intercept(Ext, pkt));

        mockHandler.Verify(x => x.Invoke(It.IsAny<Intercept>()), Times.Exactly(1));
    }
}
