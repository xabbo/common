﻿using System;

using Xabbo.Messages;
using Xabbo.Connection;

namespace Xabbo.Interceptor;

/// <summary>
/// Represents a packet interceptor that can read, modify and send packets.
/// </summary>
public interface IInterceptor : IConnection, IInterceptorContext
{
    /// <summary>
    /// Gets the message dispatcher associated with this interceptor.
    /// </summary>
    IMessageDispatcher Dispatcher { get; }

    /// <summary>
    /// Invoked when the interceptor has been initialized.
    /// </summary>
    event Action<InitializedArgs>? Initialized;

    IInterceptor IInterceptorContext.Interceptor => this;
}
