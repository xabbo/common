using System;

namespace Xabbo.Messages;

/// <summary>
/// Defines an association of message names between clients.
/// </summary>
public readonly record struct MessageNames(Direction Direction, string? Unity = null, string? Flash = null, string? Shockwave = null)
{
    public string? GetName(Clients client) => client switch
    {
        Clients.Unity => Unity,
        Clients.Flash => Flash,
        Clients.Shockwave => Shockwave,
        _ => throw new Exception($"Unknown client: {client}"),
    };

    public MessageNames WithName(Clients client, string name) => client switch
    {
        Clients.Unity => this with { Unity = name },
        Clients.Flash => this with { Flash = name },
        Clients.Shockwave => this with { Shockwave = name },
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
