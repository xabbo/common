using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the version of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute(string version) : Attribute
{
    /// <summary>
    /// Gets the version of the extension.
    /// </summary>
    public string Version { get; } = version;
}
