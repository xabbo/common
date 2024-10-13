using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;

using static Xabbo.Common.Generator.Utility.AnalysisHelper;

namespace Xabbo.Common.Generator;

/*
 * Analyze type arguments of all invocations to:
 * - Read<T, ...>()
 * - ReadAt<T, ...>()
 * - Write<T, ...>()
 * - WriteAt<T, ...>()
 * - Replace<T, ...>()
 * - ReplaceAt<T, ...>()
 * - Modify<T, ...>()
 * - ModifyAt<T, ...>()
 * - Send<T, ...>()
*/

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class PacketTypeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create([
        DiagnosticDescriptors.NotPrimitiveOrComposerType,
        DiagnosticDescriptors.NotPrimitiveOrParserType,
        DiagnosticDescriptors.NotPrimitiveOrParserComposerType,
        DiagnosticDescriptors.NotPrimitiveType,
        DiagnosticDescriptors.MethodDoesNotSupportArray,
        DiagnosticDescriptors.MethodDoesNotSupportEnumerable,
    ]);

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext ctx)
    {
        if (ctx.IsGeneratedCode ||
            ctx.Node is not InvocationExpressionSyntax invocation ||
            ctx.SemanticModel.GetSymbolInfo(invocation.Expression).Symbol is not IMethodSymbol method ||
            method.ReceiverType is not INamedTypeSymbol receiverType)
        {
            return;
        }

        InvocationKind invocationKind = GetInvocationKind(method.Name);
        if (invocationKind == (InvocationKind)(-1))
            return;

        // Validate the receiver type.
        Func<ITypeSymbol?, bool> checkInterface =
            (invocationKind & InvocationKind.Send) > 0
            ? ((typeSymbol) => IsIInterceptorContext(typeSymbol) || IsIConnectionInterface(typeSymbol))
            : IsIPacketInterface;

        if (!receiverType.IsOrImplementsInterface(checkInterface))
            return;

        bool
            requiresParser = (invocationKind & InvocationKind.RequiresParser) > 0,
            requiresComposer = (invocationKind & InvocationKind.RequiresComposer) > 0,
            hasFixedArg = (invocationKind & InvocationKind.HasFixedFirstArg) > 0;

        for (int i = 0; i < method.TypeParameters.Length; i++)
        {
            var typeArg = method.TypeArguments[i];
            var packetType = ExtractPacketType(invocationKind, typeArg, true);

            if (IsValidTypeForInvocation(invocationKind, packetType))
                continue;

            Location location = Location.None;

            if (invocation.Expression is MemberAccessExpressionSyntax {
                    Name: GenericNameSyntax { TypeArgumentList.Arguments: var typeArguments }
                } && i < typeArguments.Count)
            {
                // Use the generic type arg if we have it
                location = typeArguments[i].GetLocation();
            }
            else
            {
                // Otherwise use the argument.
                int argumentIndex = i;
                if ((invocationKind & InvocationKind.HasFixedFirstArg) > 0)
                    argumentIndex++;

                if (argumentIndex < invocation.ArgumentList.Arguments.Count)
                    location = invocation.ArgumentList.Arguments[argumentIndex].GetLocation();
            }

            ITypeSymbol? typeToReport = packetType.OuterType;

            if (packetType.IsInArray)
            {
                if ((invocationKind & InvocationKind.SupportsArray) == 0)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.MethodDoesNotSupportArray,
                        location,
                        invocationKind.GetMethodName()
                    ));
                    return;
                }
                typeToReport = packetType.InnerType;
            }
            else if (packetType.IsInEnumerable)
            {
                if ((invocationKind & InvocationKind.SupportsEnumerable) == 0)
                {
                    ctx.ReportDiagnostic(Diagnostic.Create(
                        DiagnosticDescriptors.MethodDoesNotSupportEnumerable,
                        location,
                        invocationKind.GetMethodName()
                    ));
                    return;
                }
                typeToReport = packetType.InnerType;
            }

            ctx.ReportDiagnostic(Diagnostic.Create(
                GetInvalidTypeDescriptorForInvocation(invocationKind),
                location,
                typeToReport?.ToDisplayString() ?? "?"
            ));
        }
    }
}
