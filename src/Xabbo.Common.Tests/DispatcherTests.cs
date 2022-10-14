using System;

using Xabbo.Interceptor;
using Xabbo.Messages;
using Xabbo.Messages.Dispatcher;

namespace Xabbo.Common.Tests;

public class DispatcherTests : IClassFixture<MessagesFixture>
{
    public class TestHandler : IMessageHandler
    {
        [InterceptOut(nameof(Outgoing.Move))]
        public virtual void OnMove(InterceptArgs e) { }
    }

    private IMessageManager Messages { get; }
    private IMessageDispatcher Dispatcher { get; }

    public DispatcherTests(MessagesFixture fixture)
    {
        Messages = fixture.Messages;
        Dispatcher = new MessageDispatcher(Messages);
    }

    /// <summary>
    /// Asserts that messages are correctly routed to intercept callbacks.
    /// </summary>
    [Fact]
    public void AddIntercept()
    {
        var mockHandler = new Mock<Action<InterceptArgs>>();
        mockHandler.Setup(x => x.Invoke(It.IsAny<InterceptArgs>()));

        Dispatcher.AddIntercept(Messages.Out.Move, mockHandler.Object, ClientType.Flash);

        var packet = new Packet(Messages.Out["MoveAvatar"], ClientType.Flash);
        var interceptArgs = new InterceptArgs(Mock.Of<IInterceptor>(), Destination.Server, packet);

        Dispatcher.DispatchIntercept(interceptArgs);

        Dispatcher.RemoveIntercept(Messages.Out.Move, mockHandler.Object);

        Dispatcher.DispatchIntercept(interceptArgs);
        Dispatcher.DispatchIntercept(interceptArgs);

        // Verify that the handler was invoked only once, and not after the handler was removed.
        mockHandler.Verify(e => e.Invoke(It.IsAny<InterceptArgs>()), Times.Once);
    }

    /// <summary>
    /// Asserts that messages are correctly routed to message handlers.
    /// </summary>
    [Fact]
    public void Bind()
    {
        var mockHandler = new Mock<TestHandler>();
        mockHandler.Setup(x => x.OnMove(It.IsAny<InterceptArgs>()));

        var packet = new Packet(Messages.Out.Move, ClientType.Flash);
        var interceptArgs = new InterceptArgs(Mock.Of<IInterceptor>(), Destination.Server, packet);

        Dispatcher.Bind(mockHandler.Object, ClientType.Flash);

        Dispatcher.DispatchIntercept(interceptArgs);

        Dispatcher.Release(mockHandler.Object);

        Dispatcher.DispatchIntercept(interceptArgs);
        Dispatcher.DispatchIntercept(interceptArgs);

        // Verify that the handler was invoked only once, and not after the handler was released.
        mockHandler.Verify(e => e.OnMove(It.IsAny<InterceptArgs>()), Times.Once);
    }
}
