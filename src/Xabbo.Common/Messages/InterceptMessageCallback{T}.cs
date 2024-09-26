namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message.
/// </summary>
public delegate void InterceptMessageCallback<T>(Intercept intercept, T msg) where T : IMessage<T>;