using System;

namespace Xabbo.Messages;

/// <summary>
/// Specifies a direction and header value.
/// </summary>
public readonly record struct Header(Direction Direction, short Value)
{
    /// <summary>
    /// Represents an unknown header.
    /// </summary>
    public static readonly Header Unknown = new();

    /// <summary>
    /// Represents a header that matches all messages.
    /// </summary>
    /// <remarks>
    /// This can be used to intercept all packets with <see cref="InterceptorExtensions.Intercept(Interceptor.IInterceptor, ReadOnlySpan{Header}, InterceptCallback, ClientType)"/>.
    /// </remarks>
    public static readonly Header All = new(Direction.Both, 0);

    public static implicit operator Header((Direction direction, short value) x) => new(x.direction, x.value);

    public static implicit operator ReadOnlySpan<Header>(in Header header) => new(in header);
}
