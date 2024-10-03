namespace Xabbo.Common.Generator.Tests;

public class InterceptorTests
{
    [Fact]
    public void Basic() => V.Source(
        @"
            [Intercept]
            partial class BasicInterceptor
            {
                [InterceptIn(""Incoming"")]
                void InterceptIncoming(Intercept e) { }

                [InterceptOut(""Outgoing"")]
                void InterceptOutgoing(Intercept e) { }

                [InterceptIn(""Incoming"")]
                [InterceptOut(""Outgoing"")]
                void InterceptBoth(Intercept e) { }

                [InterceptIn(""Incoming1"", ""Incoming2"", ""Incoming3"")]
                [InterceptOut(""Outgoing1"", ""Outgoing2"", ""Outgoing3"")]
                void InterceptMultiple(Intercept e) { }
            }
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void TargetedClass() => V.Source(
        @"
            [Intercept(ClientType.Flash)]
            partial class TargetedInterceptor
            {
                [InterceptIn(""Identifier"")]
                void InterceptFlashOnly(Intercept e) { }
            }
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void TargetedHandlers() => V.Source(
        @"
            [Intercept]
            partial class TargetedHandlerInterceptor
            {
                [Intercept(ClientType.Shockwave)]
                [InterceptIn(""Incoming"")]
                void InterceptShockwave(Intercept e) { }

                [Intercept(ClientType.Flash)]
                [InterceptOut(""Outgoing"")]
                void InterceptFlash(Intercept e) { }
            }
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void TargetedMixed() => V.Source(
        @"
            // Not shockwave
            [Intercept(~ClientType.Shockwave)]
            partial class TargetedInterceptor
            {
                [InterceptIn(""Incoming"")]
                void InterceptUnityAndFlash(Intercept e) { }

                // Not Unity (nor Shockwave, inherited from class attribute)
                [Intercept(~ClientType.Unity)]
                [InterceptOut(""Outgoing"")]
                void InterceptFlashOnly(Intercept e) { }
            }
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void ClientIdentifiers() => V.Source(
        @"
            [Intercept]
            partial class TargetedIdentifierInterceptor
            {
                [InterceptIn(""f:IncomingFlash"")]
                void InterceptFlashIdentifier(Intercept e) { }

                [InterceptIn(""s:IncomingShockwave"")]
                void InterceptShockwaveIdentifier(Intercept e) { }
            }
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void HandleIntercept() => V.Source(
        @"
            [Intercept]
            partial class Interceptor
            {
                [InterceptIn(""Identifier"")]
                void Handler(Intercept e) { }
            }
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void HandleIntercept_T_Message() => V.Source(
        @$"
            [Intercept]
            partial class Interceptor
            {{
                [Intercept]
                void Handler(Intercept<Msg> e) {{ }}
            }}

            {TestConstants.MsgClass}
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void HandleInterceptMessage() => V.Source(
        @$"
            [Intercept]
            partial class Interceptor
            {{
                [Intercept]
                void Handler(Intercept e, Msg msg) {{ }}
            }}

            {TestConstants.MsgClass}
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void HandleMessage() => V.Source(
        @$"
            [Intercept]
            partial class Interceptor
            {{
                [Intercept]
                void Handler(Msg msg) {{ }}
            }}

            {TestConstants.MsgClass}
        ",
        testType: TestType.Interceptor
    );

    [Fact]
    public void ModifyMessage() => V.Source(
        @$"
            [Intercept]
            partial class Interceptor
            {{
                [Intercept]
                IMessage? Handler(Msg msg)
                {{
                    return null;
                }}
            }}

            {TestConstants.MsgClass}
        ",
        testType: TestType.Interceptor
    );
}