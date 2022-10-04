using Xabbo.Messages.Dispatcher;
using Xabbo.Interceptor;

namespace Xabbo.Messages;

/// <summary>
/// Represents a handler that can be bound to an <see cref="IMessageDispatcher"/>.
/// <para>
/// The class implementing this interface should define methods decorated with
/// <see cref="ReceiveAttribute"/>, <see cref="InterceptInAttribute"/> or <see cref="InterceptOutAttribute"/>.
/// </para>
/// <para>
/// For a <see cref="ReceiveAttribute"/>, the method signature must be
/// <see langword="void"/> (<see cref="object"/>? sender, <see cref="IReadOnlyPacket"/> packet).
/// </para>
/// <para>
/// For an <see cref="InterceptAttribute"/>, the method signature must be
/// <see langword="void"/> (<see cref="InterceptArgs"/> e).
/// </para>
/// <para>
/// When an <see cref="IMessageDispatcher"/> attempts to bind an <see cref="IMessageHandler"/>, the message names (identifiers) specified in the attributes are resolved to <see cref="Header"/>s by the <see cref="IMessageManager"/>.
/// If any required message header is unable to be resolved, the binding fails.
/// If <c>Required</c> is set to <c>false</c> on the attribute and is unable to be resolved, it is silently ignored. 
/// </para>
/// <para>
/// Once an instance is bound, the methods decorated with those attributes
/// will be invoked whenever a message matching the specified header is intercepted.
/// </para>
/// </summary>
public interface IMessageHandler { }