using System;

using Xabbo.Messages;

namespace Xabbo;

public sealed class UnhandledInterceptException(Header header, InterceptHandler handler, Exception innerException)
    : Exception("An unhandled exception occured within an intercept handler.", innerException)
{
    public Header Header { get; } = header;
    public InterceptHandler Handler { get; } = handler;
}