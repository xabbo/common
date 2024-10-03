using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

internal static partial class Executor
{
    internal static class Interceptor
    {
        internal static void Execute(SourceProductionContext context, InterceptorInfo? interceptor)
        {
            if (interceptor is null)
                return;

            string hintName = $"{interceptor.Name}.Interceptor.g.cs";
            if (interceptor.Namespace != "")
                hintName = $"{interceptor.Namespace}.{hintName}";
            context.AddSource(hintName, GenerateSource(interceptor));
        }

        internal static SourceText GenerateSource(InterceptorInfo interceptor)
        {
            var w = new SourceWriter();

            using (w.NamespaceScope(interceptor.Namespace))
            {
                w.WriteLine($"partial class {interceptor.Name} : global::Xabbo.Messages.IMessageHandler");
                using (w.BraceScope())
                {
                    w.WriteLine("global::System.IDisposable global::Xabbo.Messages.IMessageHandler.Attach(global::Xabbo.Interceptor.IInterceptor interceptor)");
                    using (w.BraceScope())
                    {
                        w.WriteLine("return interceptor.Dispatcher.Register(new global::Xabbo.Messages.InterceptGroup([");
                        using (w.IndentScope())
                        {
                            GenerateInterceptsFor(w, interceptor);
                        }
                        w.WriteLine("]));");
                    }
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
                    w.WriteLine("new global::Xabbo.Messages.InterceptHandler(");
                    using (w.IndentScope())
                    {
                        // Write target identifiers
                        w.WriteLine("(global::System.ReadOnlySpan<global::Xabbo.Messages.Identifier>)[");
                        using (w.IndentScope())
                        {
                            for (int j = 0; j < intercept.Identifiers.Length; j++)
                            {
                                var identifier = intercept.Identifiers[j];
                                if (j > 0)
                                    w.WriteLine(", ");
                                w.Write("new global::Xabbo.Messages.Identifier(global::Xabbo.ClientType.");
                                w.Write(identifier.Client);
                                w.Write(", global::Xabbo.Direction.");
                                w.Write(identifier.Direction);
                                w.Write(", \"");
                                w.Write(identifier.Name);
                                w.Write("\")");
                            }
                            w.WriteLine();
                        }
                        w.WriteLine("],");
                        // w.Write("this.");
                        w.WriteLine(intercept.HandlerMethodName);
                    }
                    w.Write(") { Target = ");
                    w.Write(string.Join(" | ", intercept.TargetClients.ToString().Split(',').Select(clientName => $"global::Xabbo.ClientType.{clientName.Trim()}")));
                    w.Write(" }");
                }
            }
            w.WriteLine();
        }
    }
}
