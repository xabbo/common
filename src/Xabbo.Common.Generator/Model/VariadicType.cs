namespace Xabbo.Common.Generator.Model;

internal sealed record VariadicType(
    string Namespace,
    string Name,
    bool IsParser,
    bool IsComposer,
    bool IsArray,
    bool IsTypeKnown
);