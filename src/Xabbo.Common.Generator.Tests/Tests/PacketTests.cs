using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator.Tests;

public class PacketTests
{
    [Fact]
    public void TestInterceptorContextReadInlineAwaitedPacket() => V.DiagnosticClass(
        @"
        [Intercept]
        partial class MyExtension : IInterceptorContext
        {
            IInterceptor IInterceptorContext.Interceptor => throw new NotImplementedException();

            async void Test()
            {
                Header header = (Direction.In, 0);
                (await ReceiveAsync(header)).Read<MyParser>();
            }
        }

        public class MyParser : IParser<MyParser>
        {
            public static MyParser Parse(in PacketReader p) => new();
        }"
    );
}
