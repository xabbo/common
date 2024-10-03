using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator;

sealed record PacketType(
    ITypeSymbol Type,
    ITypeSymbol InnerType,
    bool IsArray,
    bool IsEnumerable,
    bool IsFunc
);