namespace Xabbo;

/// <summary>
/// Specifies a client type, identifier and version.
/// </summary>
public readonly record struct Client(Clients Type, string Identifier, string Version)
{
    public static readonly Client None = new(Clients.None, "", "");
}
