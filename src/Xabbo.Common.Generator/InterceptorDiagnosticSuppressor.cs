using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Xabbo.Common.Generator.Utility;

namespace Xabbo.Common.Generator;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InterceptorDiagnosticSuppressor : DiagnosticSuppressor
{
    public SuppressionDescriptor SuppressNonStaticIntercept => new(
        id: "XABBOSPR0001",
        suppressedDiagnosticId: "CA1822",
        justification: "Intercept handlers should not be static."
    );

    public SuppressionDescriptor SuppressInterceptReturnType => new(
        id: "XABBOSPR0002",
        suppressedDiagnosticId: "CA1859",
        justification: "The method is converted to ModifyMessageCallback which returns an IMessage."
    );

    public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions
        => ImmutableArray.Create(SuppressNonStaticIntercept, SuppressInterceptReturnType);

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
                    } interceptAttribute)
                    {
                        if (diagnostic.Id == SuppressNonStaticIntercept.SuppressedDiagnosticId)
                        {
                            context.ReportSuppression(Suppression.Create(SuppressNonStaticIntercept, diagnostic));
                        }
                        else if (diagnostic.Id == SuppressInterceptReturnType.SuppressedDiagnosticId &&
                            interceptAttribute.Name == "InterceptAttribute" &&
                            AnalysisHelper.IsIMessageInterface(methodSymbol.ReturnType))
                        {
                            context.ReportSuppression(Suppression.Create(SuppressInterceptReturnType, diagnostic));
                        }
                    }
                }
            }
        }
    }
}