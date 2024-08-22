namespace Xabbo.Common.Generator.Model;

internal sealed record ExtensionInfo(
    string Namespace,
    string ClassName,
    string? Name = null,
    string? Description = null,
    string? Author = null,
    string? Version = null
);