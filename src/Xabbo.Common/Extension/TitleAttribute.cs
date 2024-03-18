using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the title of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TitleAttribute(string title) : Attribute
{
    /// <summary>
    /// Gets the title of the extension.
    /// </summary>
    public string Title { get; } = title;
}
