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
    public static string Short(this Client client) => client switch {
        Client.None => "",
        Client.Unity => "u",
        Client.Unity | Client.Flash => "uf",
        Client.Flash => "f",
        Client.Flash | Client.Shockwave => "fs",
        Client.Shockwave => "s",
        Client.Unity | Client.Shockwave => "us",
        Client.All => "ufs",
        _ => client.ToString(),
    };
}