using Microsoft.CodeAnalysis;

using Xabbo.Common.Generator.Model;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static class Extension
    {
        internal static ExtensionInfo? ExtractInfo(GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (context.SemanticModel.GetDeclaredSymbol(context.TargetNode) is not INamedTypeSymbol classSymbol)
            {
                return null;
            }

            AttributeData? attribute = classSymbol
                .GetAttributes()
                .FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == Constants.ExtensionAttributeMetadataName);

            if (attribute is null) return null;

            string namespaceName = "";
            if (classSymbol.ContainingNamespace.CanBeReferencedByName)
                namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
            ExtensionInfo extension = new(
                namespaceName,
                classSymbol.Name
            );

            foreach (var entry in attribute.NamedArguments)
            {
                string name = entry.Key;
                TypedConstant constant = entry.Value;

                if (constant.Value is not string value) continue;

                switch (name)
                {
                    case nameof(ExtensionInfo.Name):
                        extension = extension with { Name = value };
                        break;
                    case nameof(ExtensionInfo.Description):
                        extension = extension with { Description = value };
                        break;
                    case nameof(ExtensionInfo.Author):
                        extension = extension with { Author = value };
                        break;
                    case nameof(ExtensionInfo.Version):
                        extension = extension with { Version = value };
                        break;
                }
            }

            return extension;
        }
    }

}