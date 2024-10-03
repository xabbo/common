namespace Xabbo.Common.Generator.Tests;

public class TestConstants
{
    public const string ComposerClass = @"
        class Composer : IComposer
        {
            public void Compose(in PacketWriter p) { }
        }
    ";

    public const string ParserClass = @"
        class Parser : IParser<Parser>
        {
            public static Parser Parse(in PacketReader p) => new();
        }
    ";

    public const string ParserComposerClass = @"
        class ParserComposer : IParserComposer<ParserComposer>
        {
            public static ParserComposer Parse(in PacketReader p) => new();
            public void Compose(in PacketWriter p) { }
        }
    ";

    public const string MsgClass =  @"
        class Msg : IMessage<Msg>
        {
            static Identifier IMessage<Msg>.Identifier => default;
            static Msg IParser<Msg>.Parse(in PacketReader p) => throw new NotImplementedException();
            void IComposer.Compose(in PacketWriter p) { }
        }
    ";
}
