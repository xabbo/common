using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

public class TestMessage : IMessage<TestMessage>
{
    public static Identifier Identifier => throw new NotImplementedException();

    public static TestMessage Parse(in PacketReader p)
    {
        throw new NotImplementedException();
    }

    public void Compose(in PacketWriter p)
    {
        throw new NotImplementedException();
    }
}