namespace Xabbo.Common.Generator.Tests;

public class InterceptorTests
{
    [Fact]
    public Task TestBasicInterceptor() => TestHelper.Verify(TestType.Interceptor, @"
        using Xabbo;

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
    ");

    [Fact]
    public Task TestTargetedInterceptor() => TestHelper.Verify(TestType.Interceptor, @"
        using Xabbo;

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
    ");

    [Fact]
    public Task TestTargetedHandlers() => TestHelper.Verify(TestType.Interceptor, @"
        using Xabbo;

        [Intercept]
        partial class TargetedHandlerInterceptor
        {
            [Intercept(ClientType.Shockwave)]
            [InterceptIn(""Incoming"")]
            void InterceptShockwave(Intercept e) { }

            [Intercept(ClientType.Flash)]
            [InterceptOut(""Outgiong"")]
            void InterceptFlash(Intercept e) { }
        }
    ");

    [Fact]
    public Task TestTargetedIdentifiers() => TestHelper.Verify(TestType.Interceptor, @"
        using Xabbo;

        [Intercept]
        partial class TargetedIdentifierInterceptor
        {
            [InterceptIn(""f:IncomingFlash"")]
            void InterceptFlashIdentifier(Intercept e) { }

            [InterceptIn(""s:IncomingShockwave"")]
            void InterceptShockwaveIdentifier(Intercept e) { }
        }
    ");
}