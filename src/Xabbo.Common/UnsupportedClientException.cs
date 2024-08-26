using System;

namespace Xabbo;

/// <summary>
/// Thrown when an operation is not supported for the specified client.
/// </summary>
public sealed class UnsupportedClientException(ClientType client)
    : Exception($"This operation is not supported for the {client} client.")
{
    public ClientType Client { get; } = client;

    public static void ThrowIfUnknown(ClientType client)
    {
        if ((client & ClientType.All) == ClientType.None)
            throw new UnsupportedClientException(client);
    }

    public static void ThrowIf(ClientType client, ClientType clients)
    {
        if ((client & ClientType.All) == ClientType.None)
            throw new UnsupportedClientException(client);
        if ((client & clients) != ClientType.None)
            throw new UnsupportedClientException(client);
    }
}