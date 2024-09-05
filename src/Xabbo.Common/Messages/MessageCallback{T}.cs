namespace Xabbo.Messages;

public delegate void MessageCallback<T>(T msg) where T : IMessage<T>;