using System;

namespace Xabbo.Messages;

/// <summary>
/// Defines an association of message names between clients.
/// </summary>
public readonly record struct MessageNames(Direction Direction, string? Unity = null, string? Flash = null, string? Shockwave = null)
{
    public string? GetName(ClientType client) => client switch
    {
        ClientType.Unity => Unity,
        ClientType.Flash => Flash,
        ClientType.Shockwave => Shockwave,
        _ => throw new Exception($"Unknown client: {client}"),
    };

    public MessageNames WithName(ClientType client, string name) => client switch
    {
        ClientType.Unity => this with { Unity = name },
        ClientType.Flash => this with { Flash = name },
        ClientType.Shockwave => this with { Shockwave = name },
        _ => throw new Exception($"Unknown client: {client}"),
    };

    public override int GetHashCode() => (
        Unity?.ToUpperInvariant(),
        Flash?.ToUpperInvariant(),
        Shockwave?.ToUpperInvariant()
    ).GetHashCode();

    public bool Equals(MessageNames other) =>
        string.Equals(Unity, other.Unity, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(Flash, other.Flash, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(Shockwave, other.Shockwave, StringComparison.OrdinalIgnoreCase);
}
