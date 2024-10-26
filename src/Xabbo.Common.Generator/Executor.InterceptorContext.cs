using Microsoft.CodeAnalysis;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

internal static partial class Executor
{
    const string ContextBase = @"
        /// <inheritdoc cref=""global::Xabbo.ConnectionExtensions.Send(global::Xabbo.Connection.IConnection, global::Xabbo.Messages.IMessage)"" />
        private void Send(global::Xabbo.Messages.IMessage message)
            => global::Xabbo.ConnectionExtensions.Send(((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor, message);

        /// <inheritdoc cref=""global::Xabbo.InterceptorExtensions.ReceiveAsync(global::Xabbo.Interceptor.IInterceptor, global::System.ReadOnlySpan{global::Xabbo.Messages.Header}, int?, bool, global::System.Func{global::Xabbo.Messages.IPacket, bool}?, global::System.Threading.CancellationToken)"" />
        private global::System.Threading.Tasks.Task<global::Xabbo.Messages.IPacket> ReceiveAsync(
            global::System.ReadOnlySpan<global::Xabbo.Messages.Header> headers,
            int? timeout = null, bool block = false,
            global::System.Func<global::Xabbo.Messages.IPacket, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) => global::Xabbo.InterceptorExtensions.ReceiveAsync(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            headers, timeout, block, shouldCapture, cancellationToken
        );

        /// <inheritdoc cref=""global::Xabbo.InterceptorExtensions.ReceiveAsync(global::Xabbo.Interceptor.IInterceptor, global::System.ReadOnlySpan{global::Xabbo.Messages.Identifier}, int?, bool, global::System.Func{global::Xabbo.Messages.IPacket, bool}?, global::System.Threading.CancellationToken)"" />
        private global::System.Threading.Tasks.Task<global::Xabbo.Messages.IPacket> ReceiveAsync(
            global::System.ReadOnlySpan<global::Xabbo.Messages.Identifier> identifiers,
            int? timeout = null, bool block = false,
            global::System.Func<global::Xabbo.Messages.IPacket, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) => global::Xabbo.InterceptorExtensions.ReceiveAsync(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            identifiers, timeout, block, shouldCapture, cancellationToken
        );

        /// <inheritdoc cref=""global::Xabbo.InterceptorExtensions.ReceiveAsync{TMsg}(global::Xabbo.Interceptor.IInterceptor, int?, bool, global::System.Func{TMsg, bool}?, global::System.Threading.CancellationToken)"" />
        private global::System.Threading.Tasks.Task<TMsg> ReceiveAsync<TMsg>(
            int? timeout = null, bool block = false,
            global::System.Func<TMsg, bool>? shouldCapture = null,
            global::System.Threading.CancellationToken cancellationToken = default
        ) where TMsg : global::Xabbo.Messages.IMessage<TMsg> => global::Xabbo.InterceptorExtensions.ReceiveAsync<TMsg>(
            ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor,
            timeout, block, shouldCapture, cancellationToken
        );

        /// <inheritdoc cref=""global::Xabbo.InterceptorExtensions.RequestAsync{TReq, TRes, TData}(global::Xabbo.Interceptor.IInterceptor, global::Xabbo.Messages.IRequestMessage{TReq, TRes, TData}, int?, bool, global::System.Threading.CancellationToken)"" />
        private async global::System.Threading.Tasks.Task<TData> RequestAsync<TReq, TRes, TData>(
            global::Xabbo.Messages.IRequestMessage<TReq, TRes, TData> request,
            int? timeout = null, global::System.Threading.CancellationToken cancellationToken = default
        )
            where TReq : global::Xabbo.Messages.IRequestMessage<TReq, TRes, TData>
            where TRes : global::Xabbo.Messages.IMessage<TRes>
        {
            global::Xabbo.Interceptor.IInterceptor interceptor = ((global::Xabbo.Interceptor.IInterceptorContext)this).Interceptor;
            global::System.Threading.Tasks.Task<TRes> response = global::Xabbo.InterceptorExtensions.ReceiveAsync<TRes>(
                interceptor,
                timeout: timeout,
                block: true,
                shouldCapture: request.MatchResponse,
                cancellationToken: cancellationToken
            );
            global::Xabbo.ConnectionExtensions.Send(interceptor, request);

            return request.GetData(await response);
        }";

    internal static partial class InterceptorContext
    {
        static void EmitSendArity(SourceWriter w, InvocationKind kind, int arity)
        {
            w.Write("private void Send");
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
                w.WriteLine("using global::Xabbo.Messages.Packet packet = new(header, context.Interceptor.Session.Client.Type);");
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
                w.WriteLine();
                EmitSendArity(w, kind, arities[i]);
            }
        }

        public static void Execute(SourceProductionContext context, InterceptorContextInfo? info)
        {
            if (info is null)
                return;

            GenerateContextBase(context, info);
            GenerateSendArtities(context, info);
        }

        static void GenerateContextBase(SourceProductionContext context, InterceptorContextInfo info)
        {
            using SourceWriter w = new();

            w.WriteLine("#nullable enable");
            w.WriteLine("");

            using (w.NamespaceScope(info.Namespace))
            {
                using (w.BraceScope($"partial class {info.Name}"))
                {
                    w.WriteLine(ContextBase);
                }
            }

            string ns = info.Namespace;
            if (ns == "<global namespace>")
                ns = "global";

            context.AddSource($"{ns}.{info.Name}.InterceptorContext.Base.g.cs", w.ToSourceText());
        }

        static void GenerateSendArtities(SourceProductionContext context, InterceptorContextInfo info)
        {
            using SourceWriter w = new();

            using (w.NamespaceScope(info.Namespace))
            {
                using (w.BraceScope($"partial class {info.Name}"))
                {
                    w.WriteLine($"// Generating {info.SendHeaderArities.Length} send header method(s)");
                    EmitSendArities(w, InvocationKind.SendHeader, info.SendHeaderArities);
                    w.WriteLine($"// Generating {info.SendIdentifierArities.Length} send identifier method(s)");
                    EmitSendArities(w, InvocationKind.SendIdentifier, info.SendIdentifierArities);
                }
            }

            string ns = info.Namespace;
            if (ns == "<global namespace>")
                ns = "global";

            context.AddSource($"{ns}.{info.Name}.InterceptorContext.g.cs", w.ToSourceText());
        }
    }
}