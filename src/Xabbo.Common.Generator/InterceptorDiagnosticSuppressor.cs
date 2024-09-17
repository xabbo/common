using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Xabbo.Common.Generator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InterceptorDiagnosticSuppressor : DiagnosticSuppressor
{
    public SuppressionDescriptor SuppressionDescriptor => new(
        id: "XABBOSPR0001",
        suppressedDiagnosticId: "CA1822",
        justification: "Intercept handlers should not be static."
    );

    public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions
        => ImmutableArray.Create(SuppressionDescriptor);

    public override void ReportSuppressions(SuppressionAnalysisContext context)
    {
        foreach (var diagnostic in context.ReportedDiagnostics)
        {
            var syntaxTree = diagnostic.Location.SourceTree;
            if (syntaxTree is null) continue;

            var semanticModel = context.GetSemanticModel(syntaxTree);
            var node = syntaxTree.GetRoot().FindNode(diagnostic.Location.SourceSpan);
            var symbol = semanticModel.GetDeclaredSymbol(node);
            if (symbol is IMethodSymbol methodSymbol)
            {
                foreach (var attribute in methodSymbol.GetAttributes())
                {
                    if (attribute.AttributeClass is
                    {
                        ContainingNamespace: {
                            ContainingNamespace.IsGlobalNamespace: true,
                            Name: "Xabbo"
                        },
                        Name: "InterceptAttribute" or "InterceptInAttribute" or "InterceptOutAttribute"
                    })
                    {
                        context.ReportSuppression(Suppression.Create(SuppressionDescriptor, diagnostic));
                        break;
                    }
                }
            }
        }
    }
}