using Microsoft.CodeAnalysis;

namespace Xabbo.Common.Generator;

internal sealed record ExtractedType(
    ITypeSymbol? InnerType = null,
    ITypeSymbol? OuterType = null,
    bool IsInArray = false,
    bool IsInFunc = false,
    bool IsInEnumerable = false
);
