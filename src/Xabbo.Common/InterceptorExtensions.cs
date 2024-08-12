using System;
using System.ComponentModel;
using Xabbo.Interceptor;
using Xabbo.Messages;

namespace Xabbo;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class InterceptorExtensions
{
    public static IDisposable Intercept(this IInterceptor interceptor, ReadOnlySpan<Header> headers, Action<Intercept> callback)
        => interceptor.Dispatcher.Register([ new(headers, callback) ]);

    public static IDisposable Intercept(this IInterceptor interceptor, Header header, Action<Intercept> callback)
        => interceptor.Dispatcher.Register([ new([header], callback) ]);

    public static IDisposable Intercept(this IInterceptor interceptor, ReadOnlySpan<Identifier> identifiers, Action<Intercept> callback)
        => interceptor.Dispatcher.Register([ new(interceptor.Messages.Resolve(identifiers), callback) ]);

    public static IDisposable Intercept(this IInterceptor interceptor, Identifier identifier, Action<Intercept> callback)
        => interceptor.Dispatcher.Register([ new([interceptor.Messages.Resolve(identifier)], callback) ]);
}