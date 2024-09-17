using Microsoft.CodeAnalysis;

using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;

namespace Xabbo.Common.Generator.Utility;

internal static class AnalysisHelper
{
    public static bool IsHeader(ITypeSymbol? typeSymbol) => typeSymbol is
    {
        TypeKind: TypeKind.Struct,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages"
        },
        Name: "Header"
    };

    public static bool IsIdentifier(ITypeSymbol? typeSymbol) => typeSymbol is
    {
        TypeKind: TypeKind.Struct,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages"
        },
        Name: "Identifier"
    };

    public static bool IsIComposerInterface(INamedTypeSymbol symbol) => symbol is
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

    public static bool IsIParserInterface(INamedTypeSymbol symbol) => symbol is
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

    public static bool IsIParserComposerInterface(INamedTypeSymbol symbol) => symbol is
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

    public static bool IsIPacketInterface(INamedTypeSymbol symbol) => symbol is
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

    public static bool IsIConnectionInterface(INamedTypeSymbol symbol) => symbol is
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

    public static bool IsIMessageInterface(ISymbol? symbol) => symbol is INamedTypeSymbol
    {
        TypeKind: TypeKind.Interface,
        IsGenericType: false,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages"
        },
        Name: "IMessage"
    };

    public static bool IsGenericIMessageInterface(INamedTypeSymbol symbol) => symbol is
    {
        TypeKind: TypeKind.Interface,
        IsGenericType: true,
        Arity: 1,
        ContainingNamespace:
        {
            ContainingNamespace:
            {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Messages",
        },
        Name: "IMessage"
    };

    /// <summary>
    /// Returns true if the method signature is <c>void(Intercept)</c>.
    /// </summary>
    public static bool IsInterceptHandlerSignature(IMethodSymbol method) => method is
    {
        ReturnsVoid: true,
        Parameters: [
            {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol
                {
                    IsGenericType: false,
                    ContainingNamespace:
                    {
                        ContainingNamespace.IsGlobalNamespace: true,
                        Name: "Xabbo"
                    },
                    Name: "Intercept"
                }
            }
        ]
    };

    /// <summary>
    /// Returns true if the method signature is <c>void(Intercept&lt;T&gt;)</c>.
    /// </summary>
    public static bool IsInterceptMessageHandlerSignature(IMethodSymbol method) => method is
    {
        ReturnsVoid: true,
        Parameters: [
            {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol
                {
                    IsGenericType: true,
                    Arity: 1,
                    ContainingNamespace:
                    {
                        ContainingNamespace.IsGlobalNamespace: true,
                        Name: "Xabbo"
                    },
                    Name: "Intercept"
                }
            }
        ]
    };

    /// <summary>
    /// Returns true if the method signature is <c>void(IMessage&lt;T&gt;)</c>.
    /// </summary>
    public static bool IsMessageHandlerSignature(IMethodSymbol method) => method is
    {
        ReturnsVoid: true,
        Parameters: [
            {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol messageTypeSymbol
            }
        ]
    } && ImplementsIMessage(messageTypeSymbol);

    /// <summary>
    /// Returns true if the method signature is <c>void(Intercept, IMessage&lt;T&gt;)</c>.
    /// </summary>
    public static bool IsInterceptMessageHandlerSignature2(IMethodSymbol method) => method is
    {
        ReturnsVoid: true,
        Parameters: [
            {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol
                {
                    IsGenericType: false,
                    ContainingNamespace:
                    {
                        ContainingNamespace.IsGlobalNamespace: true,
                        Name: "Xabbo"
                    },
                    Name: "Intercept"
                }
            },
            {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol messageTypeSymbol
            }
        ]
    } && ImplementsIMessage(messageTypeSymbol);

    public static bool IsModifyMessageHandlerSignature(IMethodSymbol method) => method is
    {
        ReturnType: INamedTypeSymbol
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
                Name: "Messages"
            },
            Name: "IMessage"
        },
        Parameters: [
            {
                RefKind: RefKind.None,
                Type: INamedTypeSymbol messageTypeSymbol
            }
        ]
    } && ImplementsIMessage(messageTypeSymbol);

    public static bool ImplementsParser(ITypeSymbol? symbol) =>
        symbol is { } type && type.AllInterfaces.Any(IsIParserInterface);

    public static bool ImplementsComposer(ITypeSymbol? symbol) =>
        symbol is { } type && type.AllInterfaces.Any(IsIComposerInterface);

    public static bool ImplementsIMessage(ITypeSymbol? symbol)
        => symbol is { } type && type.AllInterfaces.Any(IsGenericIMessageInterface);

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

    public static bool IsXabboInterface(this ISymbol? symbol, string parentNs, string name)
    {
        return symbol is INamedTypeSymbol
        {
            TypeKind: TypeKind.Interface,
            ContainingNamespace: {
                ContainingNamespace: {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
            } containingNamespace,
        } && containingNamespace.Name == parentNs && symbol.Name == "";
    }

    public static bool IsIEnumerable_1(this ISymbol? symbol) => symbol is INamedTypeSymbol
    {
        TypeKind: TypeKind.Interface,
        IsGenericType: true,
        Arity: 1,
        ContainingNamespace: {
            ContainingNamespace: {
                ContainingNamespace: {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "System"
                },
                Name: "Collections"
            },
            Name: "Generic"
        },
        Name: "IEnumerable"
    };

    public static (VariadicType Type, bool IsValid) ToVariadicType(InvocationKind invocationKind, ITypeSymbol? typeSymbol)
    {
        bool isValidType = false;
        bool isArray = false;
        bool isParser = false;
        bool isComposer = false;

        if (typeSymbol is IArrayTypeSymbol arrayType &&
            (invocationKind & InvocationKind.Replace) == 0 &&
            (invocationKind & InvocationKind.Modify) == 0)
        {
            isArray = true;
            typeSymbol = arrayType.ElementType;
        }
        else if (((invocationKind & InvocationKind.Send) > 0 || (invocationKind & InvocationKind.Write) > 0) &&
            typeSymbol is INamedTypeSymbol iEnumerableType && iEnumerableType.IsIEnumerable_1())
        {
            typeSymbol = iEnumerableType.TypeArguments[0];
        }

        if (typeSymbol is INamedTypeSymbol namedType)
        {
            isValidType = IsPrimitivePacketType(namedType) || (
                !(((invocationKind & InvocationKind.RequiresComposer) > 0) && !ImplementsComposer(namedType)) &&
                !(((invocationKind & InvocationKind.RequiresParser) > 0) && !ImplementsParser(namedType)));

            isParser = ImplementsParser(typeSymbol);
            isComposer = ImplementsComposer(typeSymbol);
        }

        return (
            new VariadicType(
                FullyQualifiedName: typeSymbol?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? "?",
                IsArray: isArray,
                IsParser: isParser,
                IsComposer: isComposer
            ),
            isValidType
        );
    }

    public static DiagnosticDescriptor GetInvalidTypeDescriptor(InvocationKind kind)
    {
        bool
            requiresParser = (kind & InvocationKind.RequiresParser) > 0,
            requiresComposer = (kind & InvocationKind.RequiresComposer) > 0;
        return requiresParser switch
        {
            true when requiresParser => DiagnosticDescriptors.NotPrimitiveOrParserComposerType,
            true => DiagnosticDescriptors.NotPrimitiveOrParserType,
            false when requiresComposer => DiagnosticDescriptors.NotPrimitiveOrComposerType,
            false => DiagnosticDescriptors.NotPrimitiveType
        };
    }
}