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
        // This should generate a case in Replace<T> for TestParserComposer.
        // It should also generate a case in Read<T> for typeof(TestParserComposer)
        // because Replace needs to parse the structure first to obtain its length.
        p.Replace(new TestParserComposer());
    }

    [Fact]
    static void TestModifiable()
    {
        Packet p = new();
        p.Write(4);

        p.ModifyAt(0, (TestModifiable m) => new(m.Value * 3));
        Assert.Equal(12, p.ReadAt<int>(0));
    }
}