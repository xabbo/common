//HintName: TargetedHandlerInterceptor.Interceptor.g.cs
using System;

using Xabbo;
using Xabbo.Messages;
using Xabbo.Interceptor;

public partial class TargetedHandlerInterceptor : IMessageHandler
{
    IDisposable IMessageHandler.Attach(IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.In, "Incoming")
                ],
                InterceptShockwave
            ) { Target = ClientType.Shockwave },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.Out, "Outgiong")
                ],
                InterceptFlash
            ) { Target = ClientType.Flash }
        ]));
    }
}
