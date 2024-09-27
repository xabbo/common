using System;

using Xabbo.Messages;

namespace Xabbo;

/// <summary>
/// Thrown when an unhandled exception is thrown from an intercept handler.
/// </summary>
/// <param name="header">The header of the message</param>
/// <param name="handler">The handler that the exception was thrown from.</param>
/// <param name="innerException">The exception that was thrown.</param>
public sealed class UnhandledInterceptException(Header header, InterceptHandler handler, Exception innerException)
    : Exception("An unhandled exception occured within an intercept handler.", innerException)
{
    /// <summary>
    /// The header of the intercepted message when the exception was thrown.
    /// </summary>
    public Header Header { get; } = header;

    /// <summary>
    /// The handler that the exception was thrown from.
    /// </summary>
    public InterceptHandler Handler { get; } = handler;
}