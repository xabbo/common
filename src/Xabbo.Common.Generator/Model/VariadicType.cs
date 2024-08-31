namespace Xabbo.Common.Generator.Model;

internal sealed record VariadicType(
    string Namespace,
    string Name,
    bool IsParser,
    bool IsComposer,
    bool IsArray,
    bool IsTypeKnown
)
{
    public override string ToString()
    {
        if (!string.IsNullOrWhiteSpace(Namespace))
            return $"{Namespace}.{Name}";
        else
            return $"{Name}";
    }
}