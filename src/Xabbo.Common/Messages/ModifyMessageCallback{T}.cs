namespace Xabbo.Messages;

public delegate T? ModifyMessageCallback<T>(T msg) where T : IMessage<T>;