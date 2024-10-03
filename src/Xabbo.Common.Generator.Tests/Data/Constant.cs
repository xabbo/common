namespace Xabbo.Common.Generator.Tests;

public readonly record struct Constant(
    string Type,
    string Value,
    bool IsPacketPrimitive = false
)
{
    public override string ToString() => Type;
}
