using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

// This parser should only be referenced by an invocation to Read<TestParserArrayOnly[]>()
public readonly record struct TestParserArrayOnly : IParser<TestParserArrayOnly>
{
    public static TestParserArrayOnly Parse(in PacketReader p)
    {
        throw new NotImplementedException();
    }
}