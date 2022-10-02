using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the description of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class DescriptionAttribute : Attribute
{
    /// <summary>
    /// Gets the description of the extension.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Constructs a new <see cref="DescriptionAttribute"/> with the specified description.
    /// </summary>
    public DescriptionAttribute(string description)
    {
        Description = description;
    }
}