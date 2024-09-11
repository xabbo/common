using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

[Extension(
    Name = "Extension title",
    Description = "Extension description"
)]
public partial class SampleInterceptor
{
    [InterceptIn("Chat", "Shout", "Whisper")]
    void OnChat(Intercept e) { }

    [InterceptOut("Move")]
    void OnMove(Intercept e)
    {
        var (x, y) = e.Packet.Read<int, int>();
        Console.WriteLine($"Moving to {x}, {y}");
    }

    [InterceptIn("Ping"), InterceptOut("Pong")]
    void OnPingPong(Intercept e) { }

    [InterceptIn("f:Objects")]
    void OnObjects(Intercept e) { }

    [Intercept(ClientType.Shockwave)]
    [InterceptIn("s:Objects")]
    void OnShockwaveObjects(Intercept e) { }

    [Intercept]
    void OnTestMessage(TestMessage msg) { }

    [Intercept]
    void OnTestInterceptMessage(Intercept<TestMessage> e) { }

    [Intercept]
    void OnTestInterceptMessage2(Intercept e, TestMessage msg) { }
}