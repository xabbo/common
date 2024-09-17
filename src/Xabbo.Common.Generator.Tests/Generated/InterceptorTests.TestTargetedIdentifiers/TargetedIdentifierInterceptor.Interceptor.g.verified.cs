//HintName: TargetedIdentifierInterceptor.Interceptor.g.cs
using System;

using Xabbo;
using Xabbo.Messages;
using Xabbo.Interceptor;

public partial class TargetedIdentifierInterceptor : IMessageHandler
{
    IDisposable IMessageHandler.Attach(IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.Flash, Direction.In, "IncomingFlash")
                ],
                InterceptFlashIdentifier
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.Shockwave, Direction.In, "IncomingShockwave")
                ],
                InterceptShockwaveIdentifier
            ) { Target = ClientType.All }
        ]));
    }
}
