using System;

namespace Xabbo.Messages;

/// <summary>
/// Defines an association of message names between clients.
/// </summary>
public readonly record struct MessageNames(Direction Direction, string? Unity = null, string? Flash = null, string? Shockwave = null)
{
    public string? GetName(Client client) => client switch
    {
        Client.Unity => Unity,
        Client.Flash => Flash,
        Client.Shockwave => Shockwave,
        _ => throw new Exception($"Unknown client: {client}"),
    };

    public MessageNames WithName(Client client, string name) => client switch
    {
        Client.Unity => this with { Unity = name },
        Client.Flash => this with { Flash = name },
        Client.Shockwave => this with { Shockwave = name },
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
