namespace Xabbo.Messages;

public delegate void InterceptMessageCallback<T>(Intercept intercept, T msg) where T : IMessage<T>;