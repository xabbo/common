//HintName: BasicInterceptor.Interceptor.g.cs
using System;

using Xabbo;
using Xabbo.Messages;
using Xabbo.Interceptor;

public partial class BasicInterceptor : IMessageHandler
{
    IDisposable IMessageHandler.Attach(IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new InterceptGroup([
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.In, "Incoming")
                ],
                InterceptIncoming
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.Out, "Outgoing")
                ],
                InterceptOutgoing
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.In, "Incoming"), 
                    (ClientType.None, Direction.Out, "Outgoing")
                ],
                InterceptBoth
            ) { Target = ClientType.All },
            new InterceptHandler(
                (ReadOnlySpan<Identifier>)[
                    (ClientType.None, Direction.In, "Incoming1"), 
                    (ClientType.None, Direction.In, "Incoming2"), 
                    (ClientType.None, Direction.In, "Incoming3"), 
                    (ClientType.None, Direction.Out, "Outgoing1"), 
                    (ClientType.None, Direction.Out, "Outgoing2"), 
                    (ClientType.None, Direction.Out, "Outgoing3")
                ],
                InterceptMultiple
            ) { Target = ClientType.All }
        ]));
    }
}
