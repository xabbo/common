using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the author of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class AuthorAttribute(string author) : Attribute
{
    /// <summary>
    /// Gets the author of the extension.
    /// </summary>
    public string Author { get; } = author;
}
