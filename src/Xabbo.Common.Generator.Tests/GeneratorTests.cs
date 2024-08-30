using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

public class GeneratorTests
{
    static void GenerateReadParser(IPacket p)
    {
        // This should generate a case in Read<T> for typeof(TestParser)
        p.Read<TestParser>();
    }

    static void GenerateReadArrayParser(IPacket p)
    {
        // This should generate cases in Read<T> for
        // typeof(TestParserArrayOnly[]) and typeof(TestParserArrayOnly)
        p.Read<TestParserArrayOnly[]>();
    }

    static void GenerateReplaceParserComposer(IPacket p)
    {
        // This should generate a case in Read<T> for typeof(TestParserComposer)
        // because Replace needs to parse the structure first to obtain its length
        p.Replace(new TestParserComposer());
    }
}