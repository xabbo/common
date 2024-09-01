using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;
using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;

namespace Xabbo.Common.Generator;

internal static partial class Extractor
{
    internal static partial class Variadic
    {
        public static bool IsCandidateForGeneration(SyntaxNode node) => node is InvocationExpressionSyntax
        {
            Expression: MemberAccessExpressionSyntax
            {
                Name.Identifier.Value:
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

        static bool IsIComposerInterface(INamedTypeSymbol symbol) => symbol is
        {
            TypeKind: TypeKind.Interface,
            IsGenericType: false,
            ContainingNamespace:
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Messages",
            },
            Name: "IComposer"
        };

        static bool IsIParserInterface(INamedTypeSymbol symbol) => symbol is
        {
            TypeKind: TypeKind.Interface,
            IsGenericType: true,
            ContainingNamespace:
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Messages",
            },
            Name: "IParser"
        };

        static bool IsIParserComposerInterface(INamedTypeSymbol symbol) => symbol is
        {
            TypeKind: TypeKind.Interface,
            IsGenericType: true,
            ContainingNamespace:
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Messages",
            },
            Name: "IParserComposer"
        };

        static bool IsIPacketInterface(INamedTypeSymbol symbol) => symbol is
        {
            TypeKind: TypeKind.Interface,
            IsGenericType: false,
            ContainingNamespace:
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Messages",
            },
            Name: "IPacket"
        };


        static bool IsIConnectionInterface(INamedTypeSymbol symbol) => symbol is
        {
            TypeKind: TypeKind.Interface,
            IsGenericType: false,
            ContainingNamespace:
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Connection",
            },
            Name: "IConnection"
        };

        static bool IsHeaderType(ITypeSymbol? symbol) => symbol is
        {
            TypeKind: TypeKind.Struct,
            ContainingNamespace:
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Messages",
            },
            Name: "Header"
        };

        static bool IsIdentifierType(ITypeSymbol? symbol) => symbol is
        {
            TypeKind: TypeKind.Struct,
            ContainingNamespace:
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Messages",
            },
            Name: "Identifier"
        };


        public static bool ImplementsParser(ITypeSymbol? symbol) =>
            symbol is { } type && type.AllInterfaces.Any(IsIParserInterface);

        public static bool ImplementsComposer(ITypeSymbol? symbol) =>
            symbol is { } type && type.AllInterfaces.Any(IsIComposerInterface);

        public static bool IsPrimitivePacketType(ITypeSymbol? symbol)
        {
            if (symbol is null) return false;

            if (symbol is
                {
                    ContainingNamespace:
                    {
                        ContainingNamespace.IsGlobalNamespace: true,
                        Name: "System",
                    },
                    Name: "Boolean" or "Byte" or "Int16" or "Int32" or "Single" or "Int64" or "String",
                }) return true;

            if (symbol is
                {
                    ContainingNamespace:
                    {
                        ContainingNamespace.IsGlobalNamespace: true,
                        Name: "Xabbo",
                    },
                    Name: "Id" or "Length"
                }) return true;

            return false;
        }

        public static Result<VariadicInvocation?> ExtractVariadicInvocation(GeneratorSyntaxContext ctx, CancellationToken cancellationToken)
        {
            var invocationExpression = (InvocationExpressionSyntax)ctx.Node;
            var memberAccess = (MemberAccessExpressionSyntax)invocationExpression.Expression;
            var simpleName = memberAccess.Name;

            InvocationKind invocationKind = GetInvocationKind(simpleName.Identifier.ValueText);
            if (invocationKind == (InvocationKind)(-1))
                return null;

            TypeInfo memberTypeInfo = ctx.SemanticModel.GetTypeInfo(memberAccess.Expression);
            if (memberTypeInfo.Type is not INamedTypeSymbol memberType) return null;

            bool isSend = (invocationKind & InvocationKind.Send) > 0;

            // Ensure the target implements IPacket or IConnection.
            Func<INamedTypeSymbol, bool> checkInterface = isSend ? IsIConnectionInterface : IsIPacketInterface;
            if (!checkInterface(memberType) && !memberType.AllInterfaces.Any(checkInterface))
                return null;

            bool hasArguments = (invocationKind & InvocationKind.HasArguments) > 0;
            bool requireParser = (invocationKind & InvocationKind.RequiresParser) > 0;
            bool requireComposer = (invocationKind & InvocationKind.RequiresComposer) > 0;
            bool isPositional = (invocationKind & InvocationKind.At) > 0;
            bool isReplace = (invocationKind & InvocationKind.Replace) > 0;
            bool isModify = (invocationKind & InvocationKind.Modify) > 0;

            VariadicType[] types;
            List<DiagnosticInfo> diagnostics = [];

            List<(SyntaxNode Node, ITypeSymbol? TypeSymbol)> syntaxTypes = [];

            var methodArgs = invocationExpression.ArgumentList.Arguments;

            if (simpleName is GenericNameSyntax genericName)
            {
                // Get the types from the generic name syntax if we have it.
                if ((isSend || isPositional) && methodArgs.Count > 0)
                    syntaxTypes.Add((methodArgs[0], ctx.SemanticModel.GetTypeInfo(methodArgs[0].Expression).ConvertedType));
                foreach (var typeSyntax in genericName.TypeArgumentList.Arguments)
                    syntaxTypes.Add((typeSyntax, ctx.SemanticModel.GetTypeInfo(typeSyntax).ConvertedType));
            }
            else
            {
                // Otherwise, we will need to analyze the argument types.
                for (int i = 0; i < invocationExpression.ArgumentList.Arguments.Count; i++)
                {
                    var argumentSyntax = invocationExpression.ArgumentList.Arguments[i];

                    ITypeSymbol? typeSymbol = ctx.SemanticModel.GetTypeInfo(argumentSyntax.Expression).ConvertedType;

                    if (i == 0 && (isPositional || isSend))
                    {
                        syntaxTypes.Add((argumentSyntax, typeSymbol));
                        continue;
                    }

                    // If this is a modifier, extract the parameter type from the Func<T, T> or method symbol
                    if (isModify)
                    {
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
                        else
                        {
                            var operation = ctx.SemanticModel.GetOperation(argumentSyntax.Expression);
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
                    }
                    else
                    {
                        // ?
                    }

                    syntaxTypes.Add((argumentSyntax, typeSymbol));
                }
            }

            if (isPositional || isSend)
            {
                if (syntaxTypes.Count == 0)
                    return null;

                if (isSend)
                {
                    ITypeSymbol? typeSymbol = syntaxTypes[0].TypeSymbol;
                    // We need to know whether to generate a Send method for Identifier or Header first.
                    if (IsHeaderType(typeSymbol)) invocationKind |= InvocationKind.Header;
                    else if (IsIdentifierType(typeSymbol)) invocationKind |= InvocationKind.Identifier;
                    else return null;
                }

                syntaxTypes.RemoveAt(0);
            }

            types = new VariadicType[syntaxTypes.Count];
            for (int i = 0; i < types.Length; i++)
            {
                var (syntax, typeSymbol) = syntaxTypes[i];

                bool isValidType = false;
                string namespaceName = "";
                bool isArray = false;
                bool isParser = false;
                bool isComposer = false;

                if (typeSymbol is IArrayTypeSymbol arrayType && !isReplace && !isModify)
                {
                    isArray = true;
                    typeSymbol = arrayType.ElementType;
                }

                if (typeSymbol is INamedTypeSymbol namedType)
                {
                    isValidType = IsPrimitivePacketType(namedType) || (
                        !(requireComposer && !ImplementsComposer(namedType)) &&
                        !(requireParser && !ImplementsParser(namedType)));

                    namespaceName = typeSymbol.ContainingNamespace?.ToDisplayString() ?? "";
                    isParser = ImplementsParser(typeSymbol);
                    isComposer = ImplementsComposer(typeSymbol);
                }

                if (!isValidType)
                {
                    DiagnosticDescriptor descriptor = requireComposer switch
                    {
                        true when requireParser => DiagnosticDescriptors.NotPrimitiveOrParserComposerType,
                        true => DiagnosticDescriptors.NotPrimitiveOrComposerType,
                        false when requireParser => DiagnosticDescriptors.NotPrimitiveOrParserType,
                        false => DiagnosticDescriptors.NotPrimitiveType
                    };

                    diagnostics.Add(new DiagnosticInfo(
                        descriptor,
                        syntax.GetLocation(),
                        typeSymbol?.ToDisplayString() ?? "?"
                    ));
                }

                types[i] = new VariadicType(
                    FullyQualifiedName: typeSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? "?",
                    IsArray: isArray,
                    IsParser: isParser,
                    IsComposer: isComposer
                );
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
}