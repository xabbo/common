namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message.
/// </summary>
/// <param name="msg">The intercepted message.</param>
public delegate void MessageCallback<TMsg>(TMsg msg) where TMsg : IMessage<TMsg>;
