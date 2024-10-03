using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class InterceptorContextTests
{
    private static void VerifyContext(
        string source, DiagnosticSeverity? severity = null,
        TestType testType = TestType.Diagnostics,
        bool isAsync = false,
        string additionalContent = "",
        params object?[] args
    ) => V.Source(
        @$"
            new Context().Test();

            [Intercept]
            partial class Context : IInterceptorContext
            {{
                IInterceptor IInterceptorContext.Interceptor => null;

                public {(isAsync ? "async Task" : "void")} Test()
                {{
                    {source}
                }}
            }}

            {additionalContent}
        ",
        severity: severity,
        isScript: true,
        testType: testType,
        args: args
    );

    [Fact]
    public void SendHeader() => VerifyContext(
        @"
            Header header = default;
            Send(header, 1, 2, 3);
        "
    );

    [Fact]
    public void SendImplicitHeader() => VerifyContext(
        @"
            Send((Direction.Out, 0), 1, 2, 3);
        "
    );

    [Fact]
    public void SendIdentifier() => VerifyContext(
        @"
            Identifier id = default;
            Send(id, 1, 2, 3);
        "
    );

    [Fact]
    public void SendImplicitIdentifier() => VerifyContext(
        @"
            Send((ClientType.Flash, Direction.Out, ""Test""), 1, 2, 3);
        "
    );

    [Theory]
    [ClassData(typeof(ConcatData<PacketPrimitives, ObjectConstantData>))]
    public void Send(Constant constant) => VerifyContext(
        @$"
            Send((Direction.Out, 0), ({constant.Type}){constant.Value});
        ",
        severity: constant.Type == "object" ? DiagnosticSeverity.Error : null,
        args: [constant.Type]
    );

    [Theory]
    [ClassData(typeof(ConcatData<PacketPrimitives, ObjectConstantData>))]
    public void SendArray(Constant constant) => VerifyContext(
        @$"
            {constant.Type}[] array = [{constant.Value}];
            Send((Direction.Out, 0), array);
        ",
        severity: constant.Type == "object" ? DiagnosticSeverity.Error : null,
        args: [constant.Type]
    );

    [Theory]
    [ClassData(typeof(ConcatData<PacketPrimitives, ObjectConstantData>))]
    public void SendList(Constant constant) => VerifyContext(
        @$"
            List<{constant.Type}> list = [{constant.Value}];
            Send((Direction.Out, 0), list);
        ",
        severity: constant.Type == "object" ? DiagnosticSeverity.Error : null,
        args: [constant.Type]
    );

    /// <summary>
    /// This test ensures the context can send an IMessage<T>.
    /// </summary>
    [Fact]
    public void SendMessage() => VerifyContext(
        @$"
            Send(new Msg());
        ",
        additionalContent: TestConstants.MsgClass
    );

    /// <summary>
    /// This test ensures different arities for Send(Header/Identifier, ...) are generated.
    /// </summary>
    [Theory]
    [ClassData(typeof(HeaderIdentifierData))]
    public void SendVariadic(Constant specifier) => VerifyContext(
        string.Join("\n", Enumerable.Range(1, 10).Select(n =>
            $"Send({specifier.Value}, {
                string.Join(", ", Enumerable.Range(1, n))
            });"
        )),
        testType: TestType.InterceptorContext,
        args: [specifier.Type]
    );

    /// <summary>
    /// This test ensures ReceiveAsync() is injected into the context.
    /// </summary>
    [Theory]
    [ClassData(typeof(HeaderIdentifierData))]
    public void ReceiveHeaderAsync(Constant specifier) => VerifyContext(
        @$"
            IPacket packet = await ReceiveAsync([{specifier.Value}]);
        ",
        isAsync: true,
        args: [specifier.Type]
    );

    /// <summary>
    /// This test ensures ReceiveAsync<TMsg>(...) is injected into the context.
    /// </summary>
    [Fact]
    public void ReceiveMessageAsync() => VerifyContext(
        @$"
            Msg msg = await ReceiveAsync<Msg>();
        ",
        additionalContent: TestConstants.MsgClass,
        isAsync: true
    );
}
