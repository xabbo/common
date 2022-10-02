using System;

namespace Xabbo.Extension;

/// <summary>
/// Specifies the author of the extension.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class AuthorAttribute : Attribute
{
    /// <summary>
    /// Gets the author of the extension.
    /// </summary>
    public string Author { get; }

    /// <summary>
    /// Constructs a new <see cref="AuthorAttribute"/> with the specified author.
    /// </summary>
    public AuthorAttribute(string author)
    {
        Author = author;
    }
}
