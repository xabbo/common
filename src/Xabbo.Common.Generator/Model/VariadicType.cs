namespace Xabbo.Common.Generator.Model;

internal sealed record VariadicType(
    string FullyQualifiedName,
    bool IsParser,
    bool IsComposer,
    bool IsArray,
    bool IsValid
)
{
    public override string ToString() => FullyQualifiedName;
}