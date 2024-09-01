using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

public readonly record struct TestModifiable(int Value) : IParserComposer<TestModifiable>
{
    static TestModifiable IParser<TestModifiable>.Parse(in PacketReader p)
    {
        return new TestModifiable(p.ReadInt());
    }

    void IComposer.Compose(in PacketWriter p)
    {
        p.WriteInt(Value);
    }
}