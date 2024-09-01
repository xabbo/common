namespace Xabbo.Common.Generator.Model;

internal sealed record VariadicType(
    string FullyQualifiedName,
    bool IsParser,
    bool IsComposer,
    bool IsArray
)
{
    public override string ToString() => FullyQualifiedName;
}