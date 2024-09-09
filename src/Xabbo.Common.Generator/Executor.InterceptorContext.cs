using Microsoft.CodeAnalysis;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

internal static partial class Executor
{
    internal static partial class InterceptorContext
    {
        static void EmitSendArity(SourceWriter w, InvocationKind kind, int arity)
        {
            w.Write("protected void Send");
            w.WriteTypeParams(arity);
            w.Write('(');
            if ((kind & InvocationKind.Header) > 0)
                w.Write("global::Xabbo.Messages.Header header");
            else if ((kind & InvocationKind.Identifier) > 0)
                w.Write("global::Xabbo.Messages.Identifier identifier");
            if (arity > 0)
            {
                w.Write(", ");
                w.WriteTypeArgs(arity);
            }
            w.Write(")");

            using (w.BraceScope())
            {
                w.WriteLine("global::Xabbo.Interceptor.IInterceptorContext context = (global::Xabbo.Interceptor.IInterceptorContext)this;");
                if ((kind & InvocationKind.Identifier) > 0)
                    w.WriteLine("global::Xabbo.Messages.Header header = context.Interceptor.Messages.Resolve(identifier);");
                w.WriteLine("using global::Xabbo.Messages.Packet packet = new(header);");
                for (int i = 0; i < arity; i++)
                {
                    w.Write("packet.Write<");
                    w.WriteTypeParam(i, arity);
                    w.Write(">(");
                    w.WriteTypeArgName(i, arity);
                    w.WriteLine(");");
                }
                w.WriteLine("context.Interceptor.Send(packet);");
            }
        }

        static void EmitSendArities(SourceWriter w, InvocationKind kind, EquatableArray<int> arities)
        {
            for (int i = 0; i < arities.Length; i++)
            {
                EmitSendArity(w, kind, arities[i]);
            }
        }

        public static void Execute(SourceProductionContext context, InterceptorContextInfo info)
        {
            using SourceWriter w = new();

            w.WriteLines(["#nullable enable", ""]);
            using (w.NamespaceScope(info.Namespace))
            {
                using (w.BraceScope($"partial class {info.Name}"))
                {
                    w.WriteLine(@"
        protected void Send<T>(T message) where T : global::Xabbo.Messages.IMessage<T>
            => global::Xabbo.ConnectionExtensions.Send<T>(((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor, message);

        protected global::System.Threading.Tasks.Task<global::Xabbo.Messages.IPacket> ReceiveAsync(
            global::System.ReadOnlySpan<global::Xabbo.Messages.Header> headers,
            int timeout = -1, bool block = false,
            global::System.Func<global::Xabbo.Messages.IPacket, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) => global::Xabbo.InterceptorExtensions.ReceiveAsync(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            headers, timeout, block, shouldCapture, cancellationToken
        );

        protected global::System.Threading.Tasks.Task<global::Xabbo.Messages.IPacket> ReceiveAsync(
            global::System.ReadOnlySpan<global::Xabbo.Messages.Identifier> identifiers,
            int timeout = -1, bool block = false,
            global::System.Func<global::Xabbo.Messages.IPacket, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) => global::Xabbo.InterceptorExtensions.ReceiveAsync(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            identifiers, timeout, block, shouldCapture, cancellationToken
        );

        protected global::System.Threading.Tasks.Task<T> ReceiveAsync<T>(
            int timeout = -1, bool block = false,
            global::System.Func<T, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) where T : global::Xabbo.Messages.IMessage<T> => global::Xabbo.InterceptorExtensions.ReceiveAsync<T>(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            timeout, block, shouldCapture, cancellationToken
        );");

                    w.WriteLine();
                    w.WriteLine($"// Generating {info.SendHeaderArities.Length} send header method(s)");
                    EmitSendArities(w, InvocationKind.SendHeader, info.SendHeaderArities);
                    w.WriteLine();
                    w.WriteLine($"// Generating {info.SendIdentifierArities.Length} send identifier method(s)");
                    EmitSendArities(w, InvocationKind.SendIdentifier, info.SendIdentifierArities);
                }
            }

            context.AddSource($"{info.Namespace}.{info.Name}.InterceptorContext.g.cs", w.ToSourceText());
        }
    }
}