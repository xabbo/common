namespace Xabbo.Common.Generator.Tests;

public class PacketTests
{
    // The issue here is that the source generator currently verifies
    // that the receiver type of invocations of Read, Write etc. implements IPacket,
    // however, ReceiveAsync is injected by the source generator, so there
    // is no return type associated with it during source generation.
    [Fact(Skip = "Fails to generate case for MyParser in ReadImpl")]
    public Task TestInterceptorContextReadInlineAwaitedPacket() => TestHelper.Verify(
        @"
        [Intercept]
        partial class MyExtension : IInterceptorContext
        {
            IInterceptor IInterceptorContext.Interceptor => throw new NotImplementedException();

            async Task Test()
            {
                Header header = (ClientType.Flash, Direction.In, 0);
                (await ReceiveAsync(header)).Read<MyParser>();
            }
        }

        public class MyParser : IParser<MyParser>
        {
            public static MyParser Parse(in PacketReader p) => new();
        }",
        testType: TestType.ReadImpl
    );
}
