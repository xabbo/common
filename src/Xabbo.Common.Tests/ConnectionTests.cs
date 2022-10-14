using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xabbo.Connection;
using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class ConnectionTests
{
    /// <summary>
    /// Asserts that the generic Send extension method
    /// routes to <see cref="IConnection.Send(IReadOnlyPacket)"/>
    /// with the correctly constructed packet.
    /// </summary>
    [Fact]
    public void SendGenericExtension()
    {
        var referencePacket = new Packet(Header.Out(7), ClientType.Flash)
            .WriteInt(2)
            .WriteString("Hello")
            .WriteString("World");

        var mock = new Mock<IConnection>(MockBehavior.Strict);
        mock.Setup(x => x.Client).Returns(referencePacket.Protocol);
        mock.Setup(x => x.Send(It.IsAny<IReadOnlyPacket>()))
            .Callback<IReadOnlyPacket>(p =>
            {
                // Assert that the packet inherits the connection's client as its protocol.
                Assert.Equal(referencePacket.Protocol, p.Protocol);
                // Assert that the packet header and length matches the reference packet.
                Assert.Equal(referencePacket.Header, p.Header);
                Assert.Equal(referencePacket.Length, p.Length);
            });

        IConnection connection = mock.Object;

        connection.Send(referencePacket.Header, 2, "Hello", "World");

        mock.Verify(c => c.Send(It.IsAny<IReadOnlyPacket>()), Times.Once);
    }

    /// <summary>
    /// Asserts that the generic SendAsync extension method
    /// routes to <see cref="IConnection.SendAsync(IReadOnlyPacket)"/>
    /// with the correctly constructed packet.
    /// </summary>
    [Fact]
    public async Task SendAsyncGenericExtension()
    {
        var referencePacket = new Packet(Header.Out(7), ClientType.Flash)
            .WriteInt(2)
            .WriteString("Hello")
            .WriteString("World");

        var mock = new Mock<IConnection>(MockBehavior.Strict);
        mock.Setup(x => x.Client).Returns(referencePacket.Protocol);
        mock.Setup(x => x.SendAsync(It.IsAny<IReadOnlyPacket>()))
            .Returns<IReadOnlyPacket>(p =>
            {
                // Assert that the packet inherits the connection's client as its protocol.
                Assert.Equal(referencePacket.Protocol, p.Protocol);
                // Assert that the packet header and length matches the reference packet.
                Assert.Equal(referencePacket.Header, p.Header);
                Assert.Equal(referencePacket.Length, p.Length);

                return ValueTask.CompletedTask;
            });

        IConnection connection = mock.Object;

        await connection.SendAsync(referencePacket.Header, 2, "Hello", "World");

        mock.Verify(c => c.SendAsync(It.IsAny<IReadOnlyPacket>()), Times.Once);
    }
}
