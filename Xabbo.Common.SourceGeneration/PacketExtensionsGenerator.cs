using System;
using System.Linq;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Xabbo.Common.SourceGeneration
{
    [Generator]
    public class PacketExtensionsGenerator : ISourceGenerator
    {
        const int MaxParams = 20;

        public void Initialize(GeneratorInitializationContext context) { }

        public void Execute(GeneratorExecutionContext context)
        {
            GeneratePacketExtensions(context);
        }

        private void GeneratePacketExtensions(GeneratorExecutionContext context)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System.ComponentModel;");
            sb.AppendLine();

            sb.AppendLine("namespace Xabbo.Messages;");
            sb.AppendLine();

            sb.AppendLine("public static partial class PacketExtensions");
            sb.AppendLine("{"); // Open class

            // Generic read
            for (int n = 2; n <= MaxParams; n++)
            {
                if (n > 2) sb.AppendLine();
                sb.AppendLine("\t/// <summary>Reads the specified generically typed values from the packet into a tuple.</summary>");
                sb.Append("\tpublic static (");
                // Return tuple type
                for (int i = 1; i <= n; i++)
                {
                    if (i > 1) sb.Append(", ");
                    sb.Append($"T{i}");
                }
                sb.Append(") Read<");
                // Generic parameters
                for (int i = 1; i <= n; i++)
                {
                    if (i > 1) sb.Append(", ");
                    sb.Append($"T{i}");
                }
                sb.Append(">(this IReadOnlyPacket p) => (");
                // Read into tuple
                for (int i = 1; i <= n; i++)
                {
                    if (i > 1) sb.Append(", ");
                    sb.Append($"p.Read<T{i}>()");
                }
                sb.AppendLine(");");
            }

            // Generic write
            sb.AppendLine();
            for (int n = 2; n <= MaxParams; n++)
            {
                if (n > 2) sb.AppendLine();
                sb.AppendLine("\t/// <summary>Writes the specified generically typed values to the packet.</summary>");
                sb.Append("\tpublic static TPacket Write<TPacket, ");

                // Generic parameters
                for (int i = 1; i <= n; i++)
                {
                    if (i > 1) sb.Append(", ");
                    sb.Append("T").Append(i);
                }
                sb.Append(">(this TPacket packet, ");

                // Method arguments
                for (int i = 1; i <= n; i++)
                {
                    if (i > 1) sb.Append(", ");
                    sb.Append($"T{i} value{i}");
                }
                sb.AppendLine(")");

                sb.AppendLine("\t\twhere TPacket : IPacket"); // Type constraint
                
                sb.AppendLine("\t{"); // Open method

                sb.Append("\t\treturn packet");
                for (int i = 1; i <= n; i++)
                {
                    sb.AppendLine();
                    sb.Append($"\t\t\t.Write<TPacket, T{i}>(value{i})");
                }
                sb.AppendLine(";");

                sb.AppendLine("\t}"); // Close method
            }

            sb.AppendLine("}"); // Close class

            context.AddSource("PacketExtensions.g.cs", SourceText.From(sb.ToString(), Encoding.UTF8));
        }
    }
}