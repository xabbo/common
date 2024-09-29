namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message.
/// </summary>
/// <typeparam name="TMsg">The type of the message.</typeparam>
/// <param name="intercept">The intercept event arguments.</param>
public delegate void InterceptCallback<TMsg>(Intercept<TMsg> intercept) where TMsg : IMessage<TMsg>;
