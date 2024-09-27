using System;

namespace Xabbo.Messages;

/// <summary>
/// Represents a service that routes intercepted messages to registered intercept handlers.
/// </summary>
public interface IMessageDispatcher
{
    /// <summary>
    /// Gets the message manager associated with this dispatcher.
    /// </summary>
    IMessageManager Messages { get; }

    /// <summary>
    /// Routes the specified <see cref="Intercept"/> to the relevant handlers currently attached to this dispatcher.
    /// </summary>
    void Dispatch(Intercept intercept);

    /// <summary>
    /// Registers a group of intercept handlers.
    /// </summary>
    /// <returns>
    /// An <see cref="IDisposable"/> that deregisters the intercept group
    /// when <see cref="IDisposable.Dispose"/> is called.
    /// </returns>
    IDisposable Register(InterceptGroup group);

    /// <summary>
    /// Releases all bound intercept handlers and intercept callbacks.
    /// </summary>
    void Reset();
}
