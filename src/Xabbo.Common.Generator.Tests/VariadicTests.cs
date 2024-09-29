namespace Xabbo.Common.Generator.Tests;

public class VariadicTests
{
    [Fact]
    public Task TestVariadicSendHeader() => TestHelper.Verify(@"
        IInterceptor x = null!;
        Header header = new(Direction.Out, 123);
        x.Send(header, 1);
        x.Send(header, 1, 2);
        x.Send(header, 1, 2, 3);
        x.Send(header, 1, 2, 3, 4);
        x.Send(header, 1, 2, 3, 4, 5);
    ", TestType.SendHeader, isScript: true);

    [Fact]
    public Task TestVariadicSendIdentifier() => TestHelper.Verify(@" IInterceptor x = null!;
        Identifier id = new(ClientType.Flash, Direction.Out, ""Test"");
        x.Send(id, 1);
        x.Send(id, 1, 2);
        x.Send(id, 1, 2, 3);
        x.Send(id, 1, 2, 3, 4);
        x.Send(id, 1, 2, 3, 4, 5);
    ", TestType.SendIdentifier, isScript: true);

    [Fact]
    public Task TestVariadicSendHeaderImplicit() => TestHelper.Verify(@"
        IInterceptor x = null!;
        x.Send((Direction.Out, 0), 1, 2, 3);
    ", TestType.SendHeader, isScript: true);

    [Fact]
    public Task TestVariadicSendIdentifierImplicit() => TestHelper.Verify(@"
        IInterceptor x = null!;
        x.Send((ClientType.Flash, Direction.Out, ""Test""), 1, 2, 3);
    ", TestType.SendIdentifier, isScript: true);
}