using Microsoft.CodeAnalysis;
using Xabbo.Common.Generator.Diagnostics;
using Xabbo.Common.Generator.Model;

namespace Xabbo.Common.Generator.Utility;

internal static partial class AnalysisHelper
{
    public static InvocationKind GetInvocationKind(string methodName) => methodName switch
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

    public static bool IsInterceptAttribute(ITypeSymbol? type) => type is INamedTypeSymbol
    {
        ContainingNamespace:
        {
            ContainingNamespace.IsGlobalNamespace: true,
            Name: "Xabbo"
        },
        Name: "InterceptAttribute"
    };

    public static bool IsDirectionalInterceptAttribute(ITypeSymbol? type) => type is INamedTypeSymbol
    {
        ContainingNamespace:
        {
            ContainingNamespace.IsGlobalNamespace: true,
            Name: "Xabbo"
        },
        Name: "InterceptInAttribute" or "InterceptOutAttribute"
    };

    public static bool IsExtensionAttribute(ITypeSymbol? type) => type is INamedTypeSymbol
    {
        ContainingNamespace:
        {
            ContainingNamespace:
            {
                ContainingNamespace.IsGlobalNamespace: true,
                Name: "Xabbo"
            },
            Name: "Extension"
        },
        Name: "ExtensionAttribute"
    };

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

    public static bool IsImplicitHeaderTuple(ITypeSymbol? typeSymbol) => typeSymbol is INamedTypeSymbol
    {
        Name: "ValueTuple",
        TypeArguments: [
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Direction"
            },
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "System"
                },
                Name: "Int32"
            }
        ]
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

    public static bool IsImplicitIdentifierTuple(ITypeSymbol? typeSymbol) => typeSymbol is INamedTypeSymbol
    {
        Name: "ValueTuple",
        TypeArguments: [
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "ClientType"
            },
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "Xabbo"
                },
                Name: "Direction"
            },
            {
                ContainingNamespace:
                {
                    ContainingNamespace.IsGlobalNamespace: true,
                    Name: "System"
                },
                Name: "String"
            },
        ]
    };
    public static bool IsIComposerInterface(this INamedTypeSymbol symbol) => symbol is
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

    public static bool IsIParserInterface(this INamedTypeSymbol symbol) => symbol is
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

    public static bool IsIPacketInterface(ITypeSymbol? symbol) => symbol is INamedTypeSymbol
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

    public static bool IsIInterceptorContext(ITypeSymbol? symbol) => symbol is INamedTypeSymbol
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
            Name: "Interceptor",
        },
        Name: "IInterceptorContext"
    };


    public static bool IsIConnectionInterface(ITypeSymbol? symbol) => symbol is INamedTypeSymbol
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

    public static bool ImplementsInterface(this INamedTypeSymbol type, Func<INamedTypeSymbol, bool> predicate)
        => type is INamedTypeSymbol namedType && namedType.AllInterfaces.Any(predicate);

    public static bool IsOrImplementsInterface(this INamedTypeSymbol type, Func<INamedTypeSymbol, bool> predicate)
        => type is INamedTypeSymbol namedType && (predicate(namedType) || ImplementsInterface(namedType, predicate));

    public static bool ImplementsInterface(this ITypeSymbol? type, Func<INamedTypeSymbol, bool> predicate)
        => type is INamedTypeSymbol namedType && ImplementsInterface(namedType, predicate);

    public static bool IsOrImplementsInterface(this ITypeSymbol? type, Func<INamedTypeSymbol, bool> predicate)
        => type is INamedTypeSymbol namedType && IsOrImplementsInterface(namedType, predicate);

    public static INamedTypeSymbol? GetInterfaceOrSelf(
        this INamedTypeSymbol type,
        Func<INamedTypeSymbol, bool> predicate)
    {
        if (predicate(type))
            return type;
        return type.AllInterfaces.FirstOrDefault(predicate);
    }

    public static ExtractedType ExtractPacketType(
        InvocationKind invocationKind,
        ITypeSymbol? type,
        bool isGenericTypeArg)
    {
        ExtractedType extracted = new(
            InnerType: type,
            OuterType: type
        );

        if ((invocationKind & InvocationKind.Modify) > 0 && !isGenericTypeArg)
        {
            if (type is not null && ExtractInnerTypeModify(type) is { } modifyType)
            {
                type = modifyType;
                extracted = extracted with {
                    IsInFunc = true,
                    InnerType = modifyType
                };
            }
        }

        if (type is IArrayTypeSymbol arrayType)
        {
            extracted = extracted with {
                IsInArray = true,
                InnerType = arrayType.ElementType
            };
        }
        else if (type is INamedTypeSymbol namedType)
        {
            if (!IsPrimitivePacketType(type) && !ImplementsComposer(type))
            {
                INamedTypeSymbol? enumerableType = namedType.GetInterfaceOrSelf(IsIEnumerable_1);
                if (enumerableType is not null)
                {
                    extracted = extracted with {
                        IsInEnumerable = true,
                        InnerType = enumerableType.TypeArguments[0]
                    };
                }
            }
        }

        return extracted;
    }

    /// <summary>
    /// Extracts the inner type for a modifier from a Func.
    /// </summary>
    public static ITypeSymbol? ExtractInnerTypeModify(ITypeSymbol type)
    {
        if (type is INamedTypeSymbol
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
            return funcTypeSymbol.TypeArguments[0];
        }
        else
        {
            return null;
        }
    }


    public static bool IsValidTypeForInvocation(InvocationKind invocationKind, ExtractedType extracted)
    {
        if (extracted.IsInFunc && (invocationKind & InvocationKind.SupportsFunc) == 0)
            return false;
        if (extracted.IsInArray && (invocationKind & InvocationKind.SupportsArray) == 0)
            return false;
        if (extracted.IsInEnumerable && (invocationKind & InvocationKind.SupportsEnumerable) == 0)
            return false;
        if (!IsPrimitivePacketType(extracted.InnerType))
        {
            if ((invocationKind & InvocationKind.RequiresParser) > 0 && !ImplementsParser(extracted.InnerType))
                return false;
            if ((invocationKind & InvocationKind.RequiresComposer) > 0 && !IsOrImplementsInterface(extracted.InnerType, IsIComposerInterface))
                return false;
        }
        return true;
    }

    public static VariadicType ToVariadicType(InvocationKind invocationKind, ExtractedType extractedType)
    {
        return new VariadicType(
            FullyQualifiedName: extractedType.InnerType?.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat) ?? "?",
            IsParser: ImplementsInterface(extractedType.InnerType, IsIParserInterface),
            IsComposer: IsOrImplementsInterface(extractedType.InnerType, IsIComposerInterface),
            IsArray: extractedType.IsInArray,
            IsValid: IsValidTypeForInvocation(invocationKind, extractedType)
        );
    }

    public static DiagnosticDescriptor GetInvalidTypeDescriptorForInvocation(InvocationKind kind)
    {
        bool
            requiresParser = (kind & InvocationKind.RequiresParser) > 0,
            requiresComposer = (kind & InvocationKind.RequiresComposer) > 0;

        return requiresParser switch
        {
            true when requiresComposer => DiagnosticDescriptors.NotPrimitiveOrParserComposerType,
            true => DiagnosticDescriptors.NotPrimitiveOrParserType,
            false when requiresComposer => DiagnosticDescriptors.NotPrimitiveOrComposerType,
            false => DiagnosticDescriptors.NotPrimitiveType
        };
    }
}