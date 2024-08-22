using System.ComponentModel;

namespace Xabbo.Common.Generator.Model;

[Flags]
internal enum Client
{
    None = 0,
    Unity = 1 << 0,
    Flash = 1 << 1,
    Shockwave = 1 << 2,
    All = Unity | Flash | Shockwave
}

[EditorBrowsable(EditorBrowsableState.Never)]
internal static class ClientExtensions
{
    public static Client ToClient(this char c) => c switch
    {
        'u' => Client.Unity,
        'f' => Client.Flash,
        's' => Client.Shockwave,
        _ => Client.None,
    };
}