namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message and allows the handler to modify the intercepted message
/// by returning a modified or new <see cref="IMessage"/> instance.
/// </summary>
public delegate IMessage? ModifyMessageCallback<T>(T msg) where T : IMessage<T>;
