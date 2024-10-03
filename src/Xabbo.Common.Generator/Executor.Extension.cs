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
        internal static void Execute(SourceProductionContext context, ExtensionInfo? extension)
        {
            if (extension is null)
                return;

            string hintName = $"{extension.ClassName}.Extension.g.cs";
            if (extension.Namespace != "")
                hintName = $"{extension.Namespace}.{hintName}";
            context.AddSource(hintName, GenerateSource(extension));
        }

        static SourceText GenerateSource(ExtensionInfo extension)
        {
            using var w = new SourceWriter();

            using (w.NamespaceScope(extension.Namespace))
            {
                using (w.BraceScope($"partial class {extension.ClassName} : global::Xabbo.Extension.IExtensionInfoInit"))
                {
                    w.WriteLine("global::Xabbo.Extension.ExtensionInfo global::Xabbo.Extension.IExtensionInfoInit.Info => new global::Xabbo.Extension.ExtensionInfo(");
                    using (w.IndentScope())
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
            }

            return w.ToSourceText();
        }
    }
}