using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

using Xabbo.Common.Generator.Model;

using static Xabbo.Common.Generator.Utility.AnalysisHelper;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static partial class Variadic
    {
        public static bool IsCandidateForGeneration(SyntaxNode node) => node is InvocationExpressionSyntax
        {
            Expression: MemberAccessExpressionSyntax
            {
                Name.Identifier.ValueText:
                    "Read" or "ReadAt" or
                    "Write" or "WriteAt" or
                    "Replace" or "ReplaceAt" or
                    "Modify" or "ModifyAt" or
                    "Send"
            }
        };

        static InvocationKind GetInvocationKind(string methodName) => methodName switch
        {
            "Read" => InvocationKind.Read,
            "ReadAt" => InvocationKind.ReadAt,
            "Write" => InvocationKind.Write,
            "WriteAt" => InvocationKind.WriteAt,
            "Replace" => InvocationKind.Replace,
            "ReplaceAt" => InvocationKind.ReplaceAt,
            "Modify" => InvocationKind.Modify,
            "ModifyAt" => InvocationKind.ModifyAt,
            "Send" => InvocationKind.Send,
            _ => (InvocationKind)(-1)
        };

        public static Result<VariadicInvocation?> ExtractVariadicInvocation(GeneratorSyntaxContext ctx, CancellationToken cancellationToken)
        {
            var invocationExpression = (InvocationExpressionSyntax)ctx.Node;
            var memberAccess = (MemberAccessExpressionSyntax)invocationExpression.Expression;
            var simpleName = memberAccess.Name;

            InvocationKind invocationKind = GetInvocationKind(simpleName.Identifier.ValueText);
            if (invocationKind == (InvocationKind)(-1))
                return null;

            bool isSend = (invocationKind & InvocationKind.Send) > 0;

            TypeInfo memberTypeInfo = ctx.SemanticModel.GetTypeInfo(memberAccess.Expression);
            if (memberTypeInfo.Type is INamedTypeSymbol memberType &&
                memberType.TypeKind != TypeKind.Error)
            {
                // Ensure the target implements IPacket or IConnection.
                Func<INamedTypeSymbol, bool> checkInterface =
                    isSend ? IsIConnectionInterface : IsIPacketInterface;
                if (!checkInterface(memberType) && !memberType.AllInterfaces.Any(checkInterface))
                    return null;
            }

            bool hasArguments = (invocationKind & InvocationKind.HasArguments) > 0;
            bool requireParser = (invocationKind & InvocationKind.RequiresParser) > 0;
            bool requireComposer = (invocationKind & InvocationKind.RequiresComposer) > 0;
            bool isPositional = (invocationKind & InvocationKind.At) > 0;
            bool isReplace = (invocationKind & InvocationKind.Replace) > 0;
            bool isModify = (invocationKind & InvocationKind.Modify) > 0;

            VariadicType[] types;
            List<DiagnosticInfo> diagnostics = [];

            List<(SyntaxNode Node, TypeInfo TypeInfo)> syntaxTypes = [];

            var methodArgs = invocationExpression.ArgumentList.Arguments;

            if (simpleName is GenericNameSyntax genericName)
            {
                // Get the types from the generic name syntax if we have it.
                if ((isSend || isPositional) && methodArgs.Count > 0)
                {
                    syntaxTypes.Add((methodArgs[0], ctx.SemanticModel.GetTypeInfo(methodArgs[0].Expression)));
                }
                foreach (var typeSyntax in genericName.TypeArgumentList.Arguments)
                    syntaxTypes.Add((typeSyntax, ctx.SemanticModel.GetTypeInfo(typeSyntax)));
            }
            else
            {
                // Otherwise, we will need to analyze the argument types.
                for (int i = 0; i < invocationExpression.ArgumentList.Arguments.Count; i++)
                {
                    var argumentSyntax = invocationExpression.ArgumentList.Arguments[i];

                    TypeInfo typeInfo = ctx.SemanticModel.GetTypeInfo(argumentSyntax.Expression);

                    if (i == 0 && (isPositional || isSend))
                    {
                        syntaxTypes.Add((argumentSyntax, typeInfo));
                        continue;
                    }

                    syntaxTypes.Add((argumentSyntax, typeInfo));
                }
            }

            if (isPositional || isSend)
            {
                if (syntaxTypes.Count == 0)
                    return null;

                if (isSend)
                {
                    TypeInfo typeInfo = syntaxTypes[0].TypeInfo;
                    ITypeSymbol? typeSymbol = typeInfo.Type;
                    ITypeSymbol? convertedTypeSymbol = typeInfo.ConvertedType;

                    // We need to know whether to generate a Send method for Identifier or Header first.
                    if (IsHeader(convertedTypeSymbol) || IsImplicitHeaderTuple(typeSymbol))
                        invocationKind |= InvocationKind.Header;
                    else if (IsIdentifier(typeSymbol) || IsImplicitIdentifierTuple(typeSymbol))
                        invocationKind |= InvocationKind.Identifier;
                    else return null;
                }

                syntaxTypes.RemoveAt(0);
            }

            types = new VariadicType[syntaxTypes.Count];
            for (int i = 0; i < types.Length; i++)
            {
                var (syntax, typeInfo) = syntaxTypes[i];

                ITypeSymbol? typeSymbol = typeInfo.ConvertedType;

                // If this is a modifier, attempt to extract the parameter type from the Func<T, T> or method symbol
                if (isModify)
                    typeSymbol = ExtractModifyInnerType(ctx.SemanticModel, syntax, typeInfo);

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

            return new Result<VariadicInvocation?>(
                new VariadicInvocation(
                    invocationKind,
                    new EquatableArray<VariadicType>(types)
                ),
                diagnostics.ToEquatableArray()
            );
        }
    }

    static ITypeSymbol? ExtractModifyInnerType(SemanticModel semanticModel, SyntaxNode syntax, TypeInfo typeInfo)
    {
        ITypeSymbol? typeSymbol = typeInfo.ConvertedType;

        if (typeSymbol is INamedTypeSymbol
        {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "System",
            },
            Name: "Func",
            IsGenericType: true,
            TypeParameters.Length: 2
        } funcTypeSymbol)
        {
            typeSymbol = funcTypeSymbol.TypeArguments[0];
        }
        else if (syntax is ArgumentSyntax argumentSyntax)
        {
            var operation = semanticModel.GetOperation(argumentSyntax.Expression);
            if (operation is IAnonymousFunctionOperation anonymousFunction)
            {
                if (anonymousFunction.Symbol.Parameters.Length > 0)
                {
                    typeSymbol = anonymousFunction.Symbol.Parameters[0].Type;
                }
            }
            else
            {
                // ?
            }
        }

        return typeSymbol;
    }
}