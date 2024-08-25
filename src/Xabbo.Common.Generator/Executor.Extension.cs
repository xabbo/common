using System.CodeDom.Compiler;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

internal static partial class Executor
{
    internal static class Extension
    {
        internal static void Execute(SourceProductionContext context, ExtensionInfo extension)
        {
            string source = GenerateSource(extension);
            string hintName = $"{extension.ClassName}.Extension.g.cs";
            if (extension.Namespace != "")
                hintName = $"{extension.Namespace}.{hintName}";
            context.AddSource(hintName, SourceText.From(source, Encoding.UTF8));
        }

        static string GenerateSource(ExtensionInfo extension)
        {
            var buffer = new StringWriter();
            var w = new IndentedTextWriter(buffer, new string(' ', 4));

            w.WriteLines([
                "using System;",
                "",
                "using Xabbo.Extension;",
                "",
            ]);

            if (extension.Namespace != "")
                w.WriteLines([ $"namespace {extension.Namespace};", "" ]);

            w.WriteLines([
                $"public partial class {extension.ClassName} : IExtensionInfoInit",
                "{"
            ]);

            using (w.IndentBlock())
            {
                w.WriteLine("ExtensionInfo IExtensionInfoInit.Info => new(");
                using (w.IndentBlock())
                {
                    var properties = new List<string>(4);
                    if (extension.Name is not null)
                        properties.Add($"{nameof(ExtensionInfo.Name)}: {SymbolDisplay.FormatLiteral(extension.Name, true)}");
                    if (extension.Description is not null)
                        properties.Add($"{nameof(ExtensionInfo.Description)}: {SymbolDisplay.FormatLiteral(extension.Description, true)}");
                    if (extension.Author is not null)
                        properties.Add($"{nameof(ExtensionInfo.Author)}: {SymbolDisplay.FormatLiteral(extension.Author, true)}");
                    if (extension.Version is not null)
                        properties.Add($"{nameof(ExtensionInfo.Version)}: {SymbolDisplay.FormatLiteral(extension.Version, true)}");

                    for (int i = 0; i < properties.Count; i++)
                    {
                        w.Write(properties[i]);
                        if (i < (properties.Count - 1))
                            w.Write(',');
                        w.WriteLine();
                    }
                }
                w.WriteLine(");");
            }
            w.WriteLine("}");

            return buffer.ToString();
        }
    }
}