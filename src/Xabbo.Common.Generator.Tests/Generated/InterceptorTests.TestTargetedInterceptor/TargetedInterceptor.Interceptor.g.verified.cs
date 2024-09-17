//HintName: TargetedInterceptor.Interceptor.g.cs
using System;

using Xabbo;
using Xabbo.Messages;
using Xabbo.Interceptor;

public partial class TargetedInterceptor : IMessageHandler
{
    IDisposable IMessageHandler.Attach(IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.In, "Incoming")
                ],
                InterceptUnityAndFlash
            ) { Target = ClientType.Unity | ClientType.Flash },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.Out, "Outgoing")
                ],
                InterceptFlashOnly
            ) { Target = ClientType.Flash }
        ]));
    }
}
