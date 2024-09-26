namespace Xabbo.Messages;

/// <summary>
/// Handles an intercepted message.
/// </summary>
public delegate void MessageCallback<T>(T msg) where T : IMessage<T>;