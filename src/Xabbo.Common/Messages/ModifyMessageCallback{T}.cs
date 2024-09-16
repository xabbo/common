namespace Xabbo.Messages;

public delegate IMessage? ModifyMessageCallback<T>(T msg) where T : IMessage<T>;