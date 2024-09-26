namespace Xabbo.Extension;

/// <summary>
/// Defines information about an extension.
/// </summary>
/// <param name="Name">The name of the extension.</param>
/// <param name="Description">The description of the extension.</param>
/// <param name="Author">The author of the extension.</param>
/// <param name="Version">The version of the extension.</param>
public sealed record ExtensionInfo(
    string? Name = null,
    string? Description = null,
    string? Author = null,
    string? Version = null
);