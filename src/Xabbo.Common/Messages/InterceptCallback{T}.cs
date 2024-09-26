namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message.
/// </summary>
public delegate void InterceptCallback<T>(Intercept<T> intercept) where T : IMessage<T>;