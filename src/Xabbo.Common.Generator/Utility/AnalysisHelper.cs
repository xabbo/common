using Microsoft.CodeAnalysis;

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
}