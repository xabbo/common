using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the version of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : Attribute
{
    /// <summary>
    /// Gets the version of the extension.
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// Constructs a new <see cref="VersionAttribute"/> with the specified version.
    /// </summary>
    public VersionAttribute(string version)
    {
        Version = version;
    }
}
