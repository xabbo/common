using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

public readonly record struct TestParserComposer(int Value) : IParserComposer<TestParserComposer>
{
    static TestParserComposer IParser<TestParserComposer>.Parse(in PacketReader p)
    {
        return new TestParserComposer(p.ReadInt());
    }

    void IComposer.Compose(in PacketWriter p)
    {
        p.WriteInt(Value);
    }
}