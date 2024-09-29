//HintName: TestMessageInterceptGeneric.Interceptor.g.cs
partial class TestMessageInterceptGeneric : global::Xabbo.Messages.IMessageHandler
{
    global::System.IDisposable global::Xabbo.Messages.IMessageHandler.Attach(global::Xabbo.Interceptor.IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new global::Xabbo.Messages.InterceptGroup([
            global::Xabbo.Messages.IMessage<global::Msg>.CreateHandler(InterceptMsg)
        ]));
    }
}
