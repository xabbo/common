namespace Xabbo.Messages;

public delegate void InterceptCallback<T>(Intercept<T> intercept) where T : IMessage<T>;