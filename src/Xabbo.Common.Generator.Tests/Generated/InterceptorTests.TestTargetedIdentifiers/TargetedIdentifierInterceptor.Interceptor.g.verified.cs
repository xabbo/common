﻿//HintName: TargetedIdentifierInterceptor.Interceptor.g.cs
partial class TargetedIdentifierInterceptor : global::Xabbo.Messages.IMessageHandler
{
    global::System.IDisposable global::Xabbo.Messages.IMessageHandler.Attach(global::Xabbo.Interceptor.IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new global::Xabbo.Messages.InterceptGroup([
            new global::Xabbo.Messages.InterceptHandler(
                (global::System.ReadOnlySpan<global::Xabbo.Messages.Identifier>)[
                    new global::Xabbo.Messages.Identifier(global::Xabbo.ClientType.Flash, global::Xabbo.Direction.In, "IncomingFlash")
                ],
                InterceptFlashIdentifier
            ) { Target = global::Xabbo.ClientType.All },
            new global::Xabbo.Messages.InterceptHandler(
                (global::System.ReadOnlySpan<global::Xabbo.Messages.Identifier>)[
                    new global::Xabbo.Messages.Identifier(global::Xabbo.ClientType.Shockwave, global::Xabbo.Direction.In, "IncomingShockwave")
                ],
                InterceptShockwaveIdentifier
            ) { Target = global::Xabbo.ClientType.All }
        ]));
    }
}
