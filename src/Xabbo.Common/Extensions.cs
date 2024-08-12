using System.ComponentModel;

using Xabbo.Messages;

namespace Xabbo;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class Extensions
{
    /// <summary>
    /// Returns a short string representation of the specified direction.
    /// </summary>
    /// <returns><c>in</c>, <c>out</c> or <c>both</c>.</returns>
    public static string Short(this Direction dir) => dir switch
    {
        Direction.None => "",
        Direction.In => "in",
        Direction.Out => "out",
        Direction.Both => "both",
        _ => dir.ToString(),
    };

    /// <summary>
    /// Returns a short string representation of the specified client.
    /// </summary>
    /// <returns><c>u</c>, <c>f</c>, <c>s</c> or a combination representing which client flags are set.</returns>
    public static string Short(this ClientType client) => client switch {
        ClientType.None => "",
        ClientType.Unity => "u",
        ClientType.Unity | ClientType.Flash => "uf",
        ClientType.Flash => "f",
        ClientType.Flash | ClientType.Shockwave => "fs",
        ClientType.Shockwave => "s",
        ClientType.Unity | ClientType.Shockwave => "us",
        ClientType.All => "ufs",
        _ => client.ToString(),
    };
}