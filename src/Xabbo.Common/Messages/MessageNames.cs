using System;
using System.Collections.Immutable;

namespace Xabbo.Messages;

/// <summary>
/// Defines an association of message names between clients.
/// </summary>
public readonly record struct MessageNames(string? Unity = null, string? Flash = null, string? Shockwave = null)
{
    private static readonly ImmutableArray<ClientType> ClientTypes = [
        ClientType.Unity,
        ClientType.Flash,
        ClientType.Shockwave
    ];

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

    public override string ToString()
    {
        ClientType processed = ClientType.None;

        string result = "";
        for (int i = 0; i < ClientTypes.Length; i++)
        {
            ClientType current = ClientTypes[i];
            if ((processed & current) != 0)
                continue;

            string? name = GetName(ClientTypes[i]);
            if (name is null) continue;

            for (int j = i + 1; j < ClientTypes.Length; j++)
            {
                ClientType other = ClientTypes[j];
                if ((processed & other) != 0)
                    continue;

                if (name.Equals(GetName(other)))
                {
                    processed |= other;
                    current |= other;
                }
            }

            if (result.Length > 0)
                result += " ";
            result += $"{current.Short()}:{name}";
        }

        return result;
    }
}
