namespace Xabbo.Common.Generator.Model;

// Contains all the information necessary to generate methods within classes implementing IInterceptorContext.
internal sealed record InterceptorContextInfo(
    string Namespace,
    string Name,
    EquatableArray<int> SendHeaderArities,
    EquatableArray<int> SendIdentifierArities
);
