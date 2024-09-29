namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message.
/// </summary>
/// <typeparam name="TMsg">The type of the message.</typeparam>
/// <param name="intercept">The intercept event arguments.</param>
/// <param name="msg">The intercepted message.</param>
public delegate void InterceptMessageCallback<TMsg>(Intercept intercept, TMsg msg) where TMsg : IMessage<TMsg>;
