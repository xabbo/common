using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

public readonly record struct TestParser(int Value) : IParser<TestParser>
{
    static TestParser IParser<TestParser>.Parse(in PacketReader p)
    {
        return new TestParser(p.ReadInt());
    }
}