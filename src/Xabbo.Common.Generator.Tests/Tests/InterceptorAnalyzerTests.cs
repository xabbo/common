using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class InterceptorAnalyzerTests
{
    [Theory]
    [InlineData("Test", false)]
    [InlineData("Test123", false)]
    [InlineData("Test_123", false)]
    [InlineData("123Test", true)]
    [InlineData("Test!", true)]
    public void ValidateIdentifierNames(string identifier, bool shouldEmit) => V.Source(
        @$"
            [Intercept]
            partial class Interceptor
            {{
                [InterceptOut(""Control"", ""{identifier}"")]
                void Validatior(Intercept e) {{ }}
            }}
        ",
        testType: TestType.Diagnostics,
        severity: shouldEmit ? DiagnosticSeverity.Error : null,
        args: [identifier]
    );

    [Theory]
    [InlineData("u", true)]
    [InlineData("f", true)]
    [InlineData("s", true)]
    [InlineData("x", false)]
    [InlineData("ufs", false)]
    [InlineData("", false)]
    public void ValidateClientIdentifier(string client, bool isValid) => V.DiagnosticClass(
        @$"
        [Intercept]
        partial class Interceptor
        {{
            [InterceptIn(""{client}:Test"")]
            void Test(Intercept e) {{ }}
        }}
        ",
        expectedSeverity: isValid ? null : DiagnosticSeverity.Error,
        args: [client]
    );

    [Theory]
    [InlineData(ClientType.None, true)]
    [InlineData(ClientType.Unity, false)]
    [InlineData(ClientType.Flash, false)]
    [InlineData(ClientType.Shockwave, false)]
    public void ValidateClientTypes(ClientType client, bool shouldEmit) => V.DiagnosticClass(
        @$"
        [Intercept]
        partial class Interceptor
        {{
            [Intercept(ClientType.{client})]
            [InterceptIn(""Test"")]
            void Test(Intercept e) {{ }}
        }}
        ",
        expectedSeverity: shouldEmit ? DiagnosticSeverity.Warning : null,
        args: [client]
    );

    [Fact]
    public void EmptyInterceptAttributeOnDirectionalIntercept() => V.DiagnosticClass(
        @$"
        [Intercept]
        partial class Interceptor
        {{
            [Intercept]
            [InterceptIn(""Test"")]
            void Test(Intercept e) {{ }}
        }}
        ",
        expectedSeverity: DiagnosticSeverity.Warning
    );

    [Fact]
    public void NoneInterceptAttributeOnDirectionalIntercept() => V.DiagnosticClass(
        @$"
        [Intercept]
        partial class Interceptor
        {{
            [Intercept(ClientType.None)]
            [InterceptIn(""Test"")]
            void Test(Intercept e) {{ }}
        }}
        ",
        expectedSeverity: DiagnosticSeverity.Warning
    );

    [Fact]
    public void IncompatibleClientTypesOnInterceptAttributes() => V.DiagnosticClass(
        @$"
        [Intercept(ClientType.Flash)]
        partial class Interceptor
        {{
            // Flash & Shockwave = ClientType.None
            [Intercept(ClientType.Shockwave)]
            [InterceptIn(""Test"")]
            void Test(Intercept e) {{ }}
        }}
        ",
        expectedSeverity: DiagnosticSeverity.Warning
    );

    [Fact]
    public void ClientInterceptOnMessageHandler() => V.DiagnosticClass(
        @$"
        [Intercept]
        partial class Interceptor
        {{
            [Intercept(ClientType.Shockwave)]
            void Test(Msg msg) {{ }}
        }}

        class Msg : IMessage<Msg>
        {{
            public static Identifier Identifier => throw new NotImplementedException();
            public static Msg Parse(in PacketReader p) => throw new NotImplementedException();
            public void Compose(in PacketWriter p) => throw new NotImplementedException();
        }}
        ",
        expectedSeverity: DiagnosticSeverity.Error
    );


    [Theory]
    [InlineData("InterceptCallback", "void Fn(Intercept e) { }", null)]
    [InlineData("InterceptCallback{T}", "void Fn(Intercept e, Msg msg) { }", DiagnosticSeverity.Error)]
    [InlineData("MessageCallback", "void Fn(Msg msg) { }", DiagnosticSeverity.Error)]
    [InlineData("InterceptMessageCallback", "void Fn(Intercept e, Msg msg) { }", DiagnosticSeverity.Error)]
    [InlineData("ModifyMessageCallback", "IMessage? Fn(Msg e) => null;", DiagnosticSeverity.Error)]
    public void ValidateInterceptHandlerSignatures(string paramText, string signature, DiagnosticSeverity? expectedSeverity) => V.DiagnosticClass(
        @$"
        [Intercept]
        partial class Interceptor
        {{
            [InterceptIn(""Test"")]
            [InterceptOut(""Test"")]
            {signature}
        }}

        class Msg : IMessage<Msg>
        {{
            public static Identifier Identifier => throw new NotImplementedException();
            public static Msg Parse(in PacketReader p) => throw new NotImplementedException();
            public void Compose(in PacketWriter p) => throw new NotImplementedException();
        }}
        ",
        expectedSeverity: expectedSeverity,
        paramText: paramText
    );

    [Theory]
    [InlineData("InterceptCallback", "void Fn(Intercept e) { }", DiagnosticSeverity.Error)]
    [InlineData("InterceptCallback{T}", "void Fn(Intercept e, Msg msg) { }", null)]
    [InlineData("MessageCallback", "void Fn(Msg msg) { }", null)]
    [InlineData("InterceptMessageCallback", "void Fn(Intercept e, Msg msg) { }", null)]
    [InlineData("ModifyMessageCallback", "IMessage? Fn(Msg e) => null;", null)]
    public void ValidateMessageHandlerSignatures(string paramText, string signature, DiagnosticSeverity? expectedSeverity) => V.DiagnosticClass(
        @$"
        [Intercept]
        partial class Interceptor
        {{
            [Intercept]
            {signature}
        }}

        class Msg : IMessage<Msg>
        {{
            public static Identifier Identifier => throw new NotImplementedException();
            public static Msg Parse(in PacketReader p) => throw new NotImplementedException();
            public void Compose(in PacketWriter p) => throw new NotImplementedException();
        }}
        ",
        expectedSeverity: expectedSeverity,
        paramText: paramText
    );
}