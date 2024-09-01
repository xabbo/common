namespace Xabbo.Common.Generator.Model;

internal sealed record VariadicInvocation(InvocationKind Kind, EquatableArray<VariadicType> Types);