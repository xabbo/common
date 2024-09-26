using System;

namespace Xabbo;

/// <summary>
/// Defines information about an extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class ExtensionAttribute : Attribute
{
    /// <summary>
    /// Defines the title of the extension.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Defines the description of the extension.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Defines the author of the extension.
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Defines the version of the extension.
    /// </summary>
    public string? Version { get; set; }
}