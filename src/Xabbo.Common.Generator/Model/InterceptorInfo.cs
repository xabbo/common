namespace Xabbo.Common.Generator.Model;

internal sealed record InterceptorInfo(
    string Namespace,
    string Name,
    EquatableArray<InterceptInfo> Intercepts
);
