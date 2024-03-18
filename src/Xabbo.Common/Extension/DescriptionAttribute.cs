using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the description of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DescriptionAttribute(string description) : Attribute
{
    /// <summary>
    /// Gets the description of the extension.
    /// </summary>
    public string Description { get; } = description;
}