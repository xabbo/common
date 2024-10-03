using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;
using static Xabbo.Common.Generator.Utility.AnalysisHelper;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static partial class InterceptorContext
    {
        internal static InterceptorContextInfo? ExtractInterceptorContextInfo(
            GeneratorAttributeSyntaxContext context, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (context.TargetSymbol is not INamedTypeSymbol namedType)
                return null;

            // Check if this class implements IInterceptorContext.
            if (!AnalysisHelper.ImplementsInterface(namedType, IsIInterceptorContext))
                return null;

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

                        bool typesFromGenericTypeArgs = false;

                        List<ITypeSymbol?> argumentTypeSymbols = [];
                        if (simpleName is GenericNameSyntax genericName)
                        {
                            typesFromGenericTypeArgs = true;
                            foreach (var typeSyntax in genericName.TypeArgumentList.Arguments)
                                argumentTypeSymbols.Add(context.SemanticModel.GetTypeInfo(typeSyntax).ConvertedType);
                        }
                        else
                        {
                            for (int i = 1; i < args.Count; i++)
                                argumentTypeSymbols.Add(context.SemanticModel.GetTypeInfo(args[i].Expression).ConvertedType);
                        }

                        var types = new VariadicType[argumentTypeSymbols.Count];
                        for (int i = 0; i < types.Length; i++)
                        {
                            var extractedType = AnalysisHelper.ExtractPacketType(InvocationKind.Send, argumentTypeSymbols[i], typesFromGenericTypeArgs);
                            types[i] = ToVariadicType(invocationKind, extractedType);
                        }

                        invocations.Add(new VariadicInvocation(
                            invocationKind,
                            types
                        ));
                    }
                }
            }

            return new InterceptorContextInfo(
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
                    .ToEquatableArray()
            );
        }
    }
}