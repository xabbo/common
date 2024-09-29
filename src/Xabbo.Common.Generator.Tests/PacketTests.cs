namespace Xabbo.Common.Generator.Tests;

public class PacketTests
{
    // The issue here is that the source generator currently verifies
    // that the receiver type of invocations of Read, Write etc. implements IPacket,
    // however, ReceiveAsync is injected by the source generator, so there
    // is no return type associated with it during source generation.
    [Fact]
    public Task TestInterceptorContextReadInlineAwaitedPacket() => TestHelper.Verify(
        @"
        [Intercept]
        partial class MyExtension : IInterceptorContext
        {
            IInterceptor IInterceptorContext.Interceptor => throw new NotImplementedException();

            async Task Test()
            {
                Header header = (Direction.In, 0);
                (await ReceiveAsync(header)).Read<MyParser>();
            }
        }

        public class MyParser : IParser<MyParser>
        {
            public static MyParser Parse(in PacketReader p) => new();
        }",
        testType: TestType.ReadImpl
    );

    [Fact(DisplayName = "Packet.Read<int[]>() should generate a type switch case for int[]")]
    public Task TestReadArray() => TestHelper.Verify(
        @"((IPacket)null!).Read<int[]>();",
        testType: TestType.ReadImpl,
        isScript: true
    );

    [Fact(DisplayName = "Packet.Read<Parser[]>() should generate type switch cases for Parser and Parser[]")]
    public Task TestReadArrayOfParser() => TestHelper.Verify(
        @"
        ((IPacket)null!).Read<Parser[]>();

        class Parser : IParser<Parser> {
            public static Parser Parse(in PacketReader p) => throw new NotImplementedException();
        }
        ",
        testType: TestType.ReadImpl,
        isScript: true
    );
}
