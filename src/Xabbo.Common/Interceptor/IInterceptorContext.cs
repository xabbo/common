namespace Xabbo.Interceptor;

/// <summary>
/// Represents a context that allows the source generator
/// to inject interceptor related methods into.
/// <para/>
/// The generator will inject the following methods into partial classes
/// that implement this interface and are marked with
/// an <c>[Extension]</c> or <c>[Intercepts]</c> attribute:
/// <list type="bullet">
/// <item><c>void Send&lt;T, ...&gt;(Identifier, T, ...)</c> <i>(variadic)</i></item>
/// <item><c>void Send&lt;T, ...&gt;(Header, T, ...)</c> <i>(variadic)</i></item>
/// <item><c>void Send&lt;T&gt;(T msg) where T : IMessage&lt;T&gt;</c></item>
/// <item><c>Task&lt;IPacket&gt; ReceiveAsync(Identifier, ...)</c></item>
/// <item><c>Task&lt;IPacket&gt; ReceiveAsync(Header, ...)</c></item>
/// <item><c>Task&lt;T&gt; ReceiveAsync&lt;T&gt;(...) where T : IMessage&lt;T&gt;</c></item>
/// </list>
/// </summary>
public interface IInterceptorContext
{
    IInterceptor Interceptor { get; }
}