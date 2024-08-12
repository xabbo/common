namespace Xabbo;

/// <summary>
/// Specifies a client type, identifier and version.
/// </summary>
public readonly record struct ClientInfo(Client Type, string Identifier, string Version)
{
    public static readonly ClientInfo None = new(Client.None, "", "");
}
