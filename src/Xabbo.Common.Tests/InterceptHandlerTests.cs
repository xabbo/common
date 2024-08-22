using System;

using Xabbo.Extension;
using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class GeneratorTests { }

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

    [InterceptsOn(ClientType.Shockwave)]
    [InterceptIn("s:Objects")]
    void OnToken(Intercept e) { }
}