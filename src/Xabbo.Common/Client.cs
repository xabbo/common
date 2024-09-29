namespace Xabbo;

/// <summary>
/// Represents a client type, identifier and version.
/// </summary>
public readonly record struct Client(ClientType Type, string Identifier = "", string Version = "")
{
    public static readonly Client None = new(ClientType.None);
}
