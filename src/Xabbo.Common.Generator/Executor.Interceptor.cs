using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

internal static partial class Executor
{
    internal static class Interceptor
    {
        internal static void Execute(SourceProductionContext context, InterceptorInfo interceptor)
        {
            string hintName = $"{interceptor.Name}.Interceptor.g.cs";
            if (interceptor.Namespace != "")
                hintName = $"{interceptor.Namespace}.{hintName}";
            context.AddSource(hintName, GenerateSource(interceptor));
        }

        internal static SourceText GenerateSource(InterceptorInfo interceptor)
        {
            var w = new SourceWriter();

            w.WriteLines([
                "using System;",
                "",
                "using Xabbo;",
                "using Xabbo.Messages;",
                "using Xabbo.Interceptor;",
                "",
            ]);

            if (interceptor.Namespace != "")
                w.WriteLines([$"namespace {interceptor.Namespace};", ""]);

            w.WriteLine($"public partial class {interceptor.Name} : IMessageHandler");
            using (w.BraceScope())
            {
                w.WriteLine("IDisposable IMessageHandler.Attach(IInterceptor interceptor)");
                using (w.BraceScope())
                {
                    w.WriteLine("return interceptor.Dispatcher.Register(new InterceptGroup([");
                    using (w.IndentScope())
                    {
                        GenerateInterceptsFor(w, interceptor);
                    }
                    w.WriteLine("]));");
                }
            }

            return w.ToSourceText();
        }

        internal static void GenerateInterceptsFor(SourceWriter w, InterceptorInfo interceptor)
        {
            for (int i = 0; i < interceptor.Intercepts.Length; i++)
            {
                var intercept = interceptor.Intercepts[i];

                if (i > 0)
                    w.WriteLine(",");

                // Write intercept handler
                if (intercept.MessageType is not null)
                {
                    w.Write("global::Xabbo.Messages.IMessage<");
                    w.Write(intercept.MessageType);
                    w.Write(">.CreateHandler(");
                    w.Write(intercept.HandlerMethodName);
                    w.Write(')');
                }
                else
                {
                    w.WriteLine("new InterceptHandler(");
                    using (w.IndentScope())
                    {
                        // Write target identifiers
                        w.WriteLine("(ReadOnlySpan<Identifier>)[");
                        using (w.IndentScope())
                        {
                            for (int j = 0; j < intercept.Identifiers.Length; j++)
                            {
                                var identifier = intercept.Identifiers[j];
                                if (j > 0)
                                    w.WriteLine(", ");
                                w.Write("(ClientType.");
                                w.Write(identifier.Client);
                                w.Write(", Direction.");
                                w.Write(identifier.Direction);
                                w.Write(", \"");
                                w.Write(identifier.Name);
                                w.Write("\")");
                            }
                            w.WriteLine();
                        }
                        w.WriteLine("],");
                        w.WriteLine(intercept.HandlerMethodName);
                    }
                    w.Write(") { Target = ");
                    w.Write(string.Join(" | ", intercept.TargetClients.ToString().Split(',').Select(clientName => $"ClientType.{clientName.Trim()}")));
                    w.Write(" }");
                }
            }
            w.WriteLine();
        }

    }
}
