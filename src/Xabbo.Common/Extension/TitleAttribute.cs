using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the title of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TitleAttribute : Attribute
{
    /// <summary>
    /// Gets the title of the extension.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Constructs a new <see cref="TitleAttribute"/> with the specified title.
    /// </summary>
    public TitleAttribute(string title)
    {
        Title = title;
    }
}
