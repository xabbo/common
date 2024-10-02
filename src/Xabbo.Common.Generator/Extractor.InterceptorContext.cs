using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Xabbo.Common.Generator.Model;

using static Xabbo.Common.Generator.Utility.AnalysisHelper;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static partial class InterceptorContext
    {
        internal static Result<InterceptorContextInfo?> ExtractInterceptorContextInfo(GeneratorAttributeSyntaxContext context)
        {
            if (context.TargetSymbol is not INamedTypeSymbol namedType)
                return null;

            // Check if this class implements IInterceptorContext
            if (!namedType.AllInterfaces.Any(it => it is
            {
                ContainingNamespace: {
                    ContainingNamespace: {
                        ContainingNamespace.IsGlobalNamespace: true,
                        Name: "Xabbo"
                    },
                    Name: "Interceptor"
                },
                Name: "IInterceptorContext"
            })) return null;

            List<DiagnosticInfo> diagnostics = [];
            List<VariadicInvocation> invocations = [];

            foreach (var member in namedType.GetMembers())
            {
                if (member is not IMethodSymbol method) continue;

                foreach (var declaringSyntaxReference in member.DeclaringSyntaxReferences)
                {
                    foreach (var invocationExpression in declaringSyntaxReference
                        .GetSyntax().DescendantNodes().OfType<InvocationExpressionSyntax>())
                    {
                        if (invocationExpression.ArgumentList.Arguments.Count < 1 ||
                            invocationExpression.Expression is not SimpleNameSyntax simpleName ||
                            simpleName.Identifier.ValueText != "Send")
                        {
                            continue;
                        }

                        var args = invocationExpression.ArgumentList.Arguments;
                        ITypeSymbol? firstArgType = context.SemanticModel.GetTypeInfo(args[0].Expression).ConvertedType;

                        InvocationKind invocationKind;
                        if (IsHeader(firstArgType) || IsImplicitHeaderTuple(firstArgType))
                            invocationKind = InvocationKind.SendHeader;
                        else if (IsIdentifier(firstArgType) || IsImplicitIdentifierTuple(firstArgType))
                            invocationKind = InvocationKind.SendIdentifier;
                        else
                            continue;

                        List<(SyntaxNode Node, ITypeSymbol? TypeSymbol)> syntaxTypes = [];
                        if (simpleName is GenericNameSyntax genericName)
                        {
                            foreach (var typeSyntax in genericName.TypeArgumentList.Arguments)
                                syntaxTypes.Add((typeSyntax, context.SemanticModel.GetTypeInfo(typeSyntax).ConvertedType));
                        }
                        else
                        {
                            for (int i = 1; i < args.Count; i++)
                                syntaxTypes.Add((args[i], context.SemanticModel.GetTypeInfo(args[i].Expression).ConvertedType));
                        }

                        var types = new VariadicType[syntaxTypes.Count];
                        for (int i = 0; i < types.Length; i++)
                        {
                            var (syntax, typeSymbol) = syntaxTypes[i];
                            var (type, isValidType) = ToVariadicType(invocationKind, typeSymbol);

                            if (!isValidType)
                            {
                                diagnostics.Add(new DiagnosticInfo(
                                    GetInvalidTypeDescriptor(invocationKind),
                                    syntax.GetLocation(),
                                    typeSymbol?.ToDisplayString() ?? "?"
                                ));
                            }

                            types[i] = type;
                        }

                        invocations.Add(new VariadicInvocation(
                            invocationKind,
                            types
                        ));
                    }
                }
            }

            return new Result<InterceptorContextInfo?>(
                new InterceptorContextInfo(
                    namedType.ContainingNamespace.ToString(),
                    namedType.Name,
                    invocations
                        .Where(x => (x.Kind & InvocationKind.SendHeader) > 0)
                        .Select(x => x.Types.Length)
                        .Distinct()
                        .ToEquatableArray(),
                    invocations
                        .Where(x => (x.Kind & InvocationKind.SendIdentifier) > 0)
                        .Select(x => x.Types.Length)
                        .Distinct()
                        .ToEquatableArray(),
                    invocations.ToEquatableArray()
                ),
                diagnostics.ToEquatableArray()
            );
        }
    }
}