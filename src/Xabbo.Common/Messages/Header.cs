using System;

namespace Xabbo.Messages;

/// <summary>
/// Specifies a client type, direction, and header value.
/// </summary>
public readonly record struct Header(ClientType Client, Direction Direction, short Value)
{
    /// <summary>
    /// Constructs a new header with the specified direction and value, and <see cref="Client"/> set to <see cref="ClientType.None"/>.
    /// </summary>
    public Header(Direction direction, short value) : this(ClientType.None, direction, value) { }

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
    public static readonly Header All = new(ClientType.All, Direction.Both, 0);

    public static implicit operator Header((ClientType client, Direction direction, short value) x) => new(x.client, x.direction, x.value);
    public static implicit operator Header((Direction direction, short value) x) => new(x.direction, x.value);

    public static implicit operator ReadOnlySpan<Header>(in Header header) => new(in header);
}
