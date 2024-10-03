//HintName: Interceptor.Interceptor.g.cs
partial class Interceptor : global::Xabbo.Messages.IMessageHandler
{
    global::System.IDisposable global::Xabbo.Messages.IMessageHandler.Attach(global::Xabbo.Interceptor.IInterceptor interceptor)
    {
        return interceptor.Dispatcher.Register(new global::Xabbo.Messages.InterceptGroup([
            global::Xabbo.Messages.IMessage<global::Msg>.CreateHandler(Handler)
        ]));
    }
}
