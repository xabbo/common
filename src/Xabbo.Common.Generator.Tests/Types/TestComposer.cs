
using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

public readonly record struct TestComposer(int Value) : IComposer
{
    void IComposer.Compose(in PacketWriter p)
    {
        p.WriteInt(Value);
    }
}