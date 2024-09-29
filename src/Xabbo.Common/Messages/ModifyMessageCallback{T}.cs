namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message and allows the handler to modify the intercepted message
/// by returning a modified or new <see cref="IMessage"/> instance.
/// </summary>
/// <typeparam name="TMsg">The type of the message.</typeparam>
public delegate IMessage? ModifyMessageCallback<TMsg>(TMsg msg) where TMsg : IMessage<TMsg>;
