using System;

using Xabbo;
using Xabbo.Messages;
using Xabbo.Interceptor;

[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822", Justification = "Intercept handler methods should not be marked static.", Scope = "member", Target = "~M:Xabbo.Common.Tests.SampleInterceptor.OnChat(Xabbo.Intercept)")]
[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822", Justification = "Intercept handler methods should not be marked static.", Scope = "member", Target = "~M:Xabbo.Common.Tests.SampleInterceptor.OnMove(Xabbo.Intercept)")]
[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822", Justification = "Intercept handler methods should not be marked static.", Scope = "member", Target = "~M:Xabbo.Common.Tests.SampleInterceptor.OnPingPong(Xabbo.Intercept)")]
[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822", Justification = "Intercept handler methods should not be marked static.", Scope = "member", Target = "~M:Xabbo.Common.Tests.SampleInterceptor.OnObjects(Xabbo.Intercept)")]
[assembly: global::System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822", Justification = "Intercept handler methods should not be marked static.", Scope = "member", Target = "~M:Xabbo.Common.Tests.SampleInterceptor.OnToken(Xabbo.Intercept)")]

namespace Xabbo.Common.Tests;

public partial class SampleInterceptor : IMessageHandler
{
    IDisposable IMessageHandler.Attach(IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.In, "Chat"), 
                    (ClientType.None, Direction.In, "Shout"), 
                    (ClientType.None, Direction.In, "Whisper")
                ],
                OnChat
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.Out, "Move")
                ],
                OnMove
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.In, "Ping"), 
                    (ClientType.None, Direction.Out, "Pong")
                ],
                OnPingPong
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.Flash, Direction.In, "Objects")
                ],
                OnObjects
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.Shockwave, Direction.In, "Objects")
                ],
                OnToken
            ) { Target = ClientType.Shockwave }
        ]));
    }
}
