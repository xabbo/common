using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Xabbo.Common.SourceGeneration
{
    [Generator]
    public class InterceptorExtensionsGenerator : ISourceGenerator
    {
        const int MaxParams = 20;

        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System.ComponentModel;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine();

            sb.AppendLine("using Xabbo.Messages;");
            sb.AppendLine();

            sb.AppendLine("namespace Xabbo.Interceptor;");
            sb.AppendLine();

            sb.AppendLine("[EditorBrowsable(EditorBrowsableState.Never)]");
            sb.AppendLine("public static class InterceptorExtensions");
            sb.AppendLine("{"); // Open class

            for (int n = 1; n <= MaxParams; n++)
            {
                if (n > 1) sb.AppendLine();
                sb.Append("\tpublic static async ValueTask SendAsync<");
                for (int i = 1; i <= n; i++)
                {
                    if (i > 1) sb.Append(", ");
                    sb.Append($"T{i}");
                }
                sb.Append(">(this IInterceptor interceptor, Header header, ");
                for (int i = 1; i <= n; i++)
                {
                    if (i > 1) sb.Append(", ");
                    sb.Append($"T{i} value{i}");
                }
                sb.AppendLine(")");
                sb.AppendLine("\t{"); // Open method
                sb.AppendLine("\t\tusing Packet packet = new Packet(interceptor.Client, header);");
                sb.AppendLine("\t\tpacket");
                for (int i = 1; i <= n; i++)
                {
                    sb.Append($"\t\t\t.Write(value{i})");
                    if (i == n) sb.Append(";");
                    sb.AppendLine();
                }
                sb.AppendLine("\t\tawait interceptor.SendAsync(packet);");
                sb.AppendLine("\t}"); // Close method
            }

            sb.AppendLine("}"); // Close class

            context.AddSource("InterceptorExtensions.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }
    }
}
