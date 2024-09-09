namespace Xabbo.Common.Generator.Model;

internal readonly record struct InterceptInfo(
    EquatableArray<Identifier> Identifiers,
    Client TargetClients,
    string HandlerMethodName,
    string? MessageType = null
);
