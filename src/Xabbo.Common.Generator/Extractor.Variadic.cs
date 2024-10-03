using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Xabbo.Common.Generator.Model;
using Xabbo.Common.Generator.Utility;
using static Xabbo.Common.Generator.Utility.AnalysisHelper;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static partial class Variadic
    {
        public static bool IsCandidateForGeneration(SyntaxNode node) => node is InvocationExpressionSyntax
        {
            // Expression: MemberAccessExpressionSyntax
            // {
            //     Name.Identifier.ValueText:
            //         "Read" or "ReadAt" or
            //         "Write" or "WriteAt" or
            //         "Replace" or "ReplaceAt" or
            //         "Modify" or "ModifyAt" or
            //         "Send"
            // }
        };

        public static VariadicInvocation? ExtractVariadicInvocation(GeneratorSyntaxContext ctx, CancellationToken cancellationToken)
        {
            var invocationExpression = (InvocationExpressionSyntax)ctx.Node;
            if (invocationExpression.Expression is not MemberAccessExpressionSyntax
            {
                Name.Identifier.ValueText:
                    "Read" or "ReadAt" or
                    "Write" or "WriteAt" or
                    "Replace" or "ReplaceAt" or
                    "Modify" or "ModifyAt" or
                    "Send"
            } memberAccess)
            {
                return null;
            }

            // var memberAccess = (MemberAccessExpressionSyntax)invocationExpression.Expression;
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
                // Func<INamedTypeSymbol, bool> checkInterface =
                //     isSend ? IsIConnectionInterface : IsIPacketInterface;
                // if (!checkInterface(memberType) && !memberType.AllInterfaces.Any(checkInterface))
                //     return null;
            }

            bool hasArguments = (invocationKind & InvocationKind.HasArguments) > 0;
            bool requireParser = (invocationKind & InvocationKind.RequiresParser) > 0;
            bool requireComposer = (invocationKind & InvocationKind.RequiresComposer) > 0;
            bool isPositional = (invocationKind & InvocationKind.At) > 0;
            bool isReplace = (invocationKind & InvocationKind.Replace) > 0;
            bool isModify = (invocationKind & InvocationKind.Modify) > 0;

            List<TypeInfo> typeInfos = [];

            var methodArgs = invocationExpression.ArgumentList.Arguments;

            bool typesFromGenericTypeArgs = false;

            if (simpleName is GenericNameSyntax genericName)
            {
                typesFromGenericTypeArgs = true;

                // Get the types from the generic name syntax if we have it.
                if ((isSend || isPositional) && methodArgs.Count > 0)
                {
                    typeInfos.Add(ctx.SemanticModel.GetTypeInfo(methodArgs[0].Expression));
                }
                foreach (var typeSyntax in genericName.TypeArgumentList.Arguments)
                    typeInfos.Add(ctx.SemanticModel.GetTypeInfo(typeSyntax));
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
                        typeInfos.Add(typeInfo);
                        continue;
                    }

                    typeInfos.Add(typeInfo);
                }
            }

            if (isPositional || isSend)
            {
                if (typeInfos.Count == 0)
                    return null;

                if (isSend)
                {
                    TypeInfo typeInfo = typeInfos[0];
                    ITypeSymbol? typeSymbol = typeInfo.Type;
                    ITypeSymbol? convertedTypeSymbol = typeInfo.ConvertedType;

                    // We need to know whether to generate a Send method for Identifier or Header first.
                    if (IsHeader(convertedTypeSymbol) || IsImplicitHeaderTuple(typeSymbol))
                        invocationKind |= InvocationKind.Header;
                    else if (IsIdentifier(typeSymbol) || IsImplicitIdentifierTuple(typeSymbol))
                        invocationKind |= InvocationKind.Identifier;
                    else return null;
                }

                typeInfos.RemoveAt(0);
            }

            var variadicTypes = new VariadicType[typeInfos.Count];
            for (int i = 0; i < variadicTypes.Length; i++)
            {
                var typeInfo = typeInfos[i];

                ITypeSymbol? typeSymbol = typeInfo.ConvertedType;

                var extractedType = ExtractPacketType(invocationKind, typeSymbol, typesFromGenericTypeArgs);

                variadicTypes[i] = ToVariadicType(invocationKind, extractedType);
            }

            return new VariadicInvocation(
                invocationKind,
                new EquatableArray<VariadicType>(variadicTypes)
            );
        }
    }
}