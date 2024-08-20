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
        Direction,
        Unity?.ToUpperInvariant(),
        Flash?.ToUpperInvariant(),
        Shockwave?.ToUpperInvariant()
    ).GetHashCode();

    public bool Equals(MessageNames other) =>
        Direction == other.Direction &&
        string.Equals(Unity, other.Unity, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(Flash, other.Flash, StringComparison.OrdinalIgnoreCase) &&
        string.Equals(Shockwave, other.Shockwave, StringComparison.OrdinalIgnoreCase);

    public override string ToString()
    {
        bool
            u = Unity is not null && Unity != "-",
            f = Flash is not null && Flash != "-",
            s = Shockwave is not null && Shockwave != "-",
            uf = u && Unity!.Equals(Flash, StringComparison.OrdinalIgnoreCase),
            us = u && Unity!.Equals(Shockwave, StringComparison.OrdinalIgnoreCase),
            fs = f && Flash!.Equals(Shockwave, StringComparison.OrdinalIgnoreCase);

        // ufs uf-s u-fs u-f-s uf us fs u f s

        if (uf && fs)
            return $"ufs:{Unity}";
        else if (uf)
            return s ? $"uf:{Unity} s:{Shockwave}" : $"uf:{Unity}";
        else if (fs)
            return u ? $"u:{Unity} fs:{Flash}" : $"fs:{Flash}";
        else if (us)
            return f ? $"us:{Unity} f:{Flash}" : $"us:Unity";
        else if (u)
            return $"u:{Unity}";
        else if (f)
            return $"f:{Flash}";
        else if (s)
            return $"s:{Shockwave}";
        else
            return "";
    }
}
