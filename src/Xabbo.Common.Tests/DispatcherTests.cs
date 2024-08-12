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
            => (Clients.Flash, Direction.Out, name ?? "");
        public static Identifier MoveAvatar { get; } = _();
    }

    private static class In
    {
        private static Identifier _([CallerMemberName] string? name = null)
            => (Clients.Flash, Direction.In, name ?? "");

        public static Identifier Chat { get; } = _();
        public static Identifier Shout { get; } = _();
        public static Identifier Whisper { get; } = _();
    }

    private IMessageManager Messages { get; }
    private IMessageDispatcher Dispatcher { get; }
    private IExtension Ext { get; }

    public DispatcherTests(MessagesFixture fixture)
    {
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
            Dispatcher.Dispatch(new(Ext, new Packet((Clients.Flash, Direction.In, (short)i))));

        registration.Dispose();

        for (int i = 0; i < expectedCount; i++)
            Dispatcher.Dispatch(new(Ext, new Packet((Clients.Flash, Direction.In, (short)i))));

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

    /// <summary>
    /// Asserts that messages are correctly routed to message handlers.
    /// </summary>
    [Fact]
    public void Bind()
    {
        // var mockHandler = new Mock<TestHandler>();
        // mockHandler.Setup(x => x.OnMove(It.IsAny<Intercept>()));
        //
        // var packet = new Packet(Messages.Out.Move, ClientType.Flash);
        // var interceptArgs = new Intercept(Mock.Of<IInterceptor>(), Direction.Outgoing, packet);
        //
        // Dispatcher.Attach(mockHandler.Object, ClientType.Flash);
        //
        // Dispatcher.DispatchIntercept(interceptArgs);
        //
        // Dispatcher.Detach(mockHandler.Object);
        //
        // Dispatcher.DispatchIntercept(interceptArgs);
        // Dispatcher.DispatchIntercept(interceptArgs);
        //
        // // Verify that the handler was invoked only once, and not after the handler was released.
        // mockHandler.Verify(e => e.OnMove(It.IsAny<Intercept>()), Times.Once);
    }
}
