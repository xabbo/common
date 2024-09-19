using System;

namespace Xabbo;

/// <summary>
/// Thrown when an operation is not supported for the specified client.
/// </summary>
public sealed class UnsupportedClientException(ClientType client)
    : Exception($"This operation is not supported for the {client} client.")
{
    public ClientType Client { get; } = client;

    /// <summary>
    /// Throws if the specified client is not a known client.
    /// </summary>
    public static void ThrowIfNone(ClientType client)
    {
        if ((client & ClientType.All) == ClientType.None)
            throw new UnsupportedClientException(client);
    }

    /// <summary>
    /// Throws if the client is any of the specified clients.
    /// </summary>
    public static void ThrowIf(ClientType client, ClientType clients)
    {
        if ((client & clients) != ClientType.None)
            throw new UnsupportedClientException(client);
    }

    /// <summary>
    /// Throws if the client is unknown or any of the specified clients.
    /// </summary>
    public static void ThrowIfNoneOr(ClientType client, ClientType clients)
    {
        if ((client & ClientType.All) == ClientType.None)
            throw new UnsupportedClientException(client);
        if ((client & clients) != ClientType.None)
            throw new UnsupportedClientException(client);
    }

    /// <summary>
    /// Throws if the client is the Shockwave client.
    /// </summary>
    public static void ThrowIfOrigins(ClientType client)
    {
        if ((client & ClientType.Shockwave) != ClientType.None)
            throw new UnsupportedClientException(client);
    }

    /// <summary>
    /// Throws if the client is the Unity or Flash client.
    /// </summary>
    public static void ThrowIfModern(ClientType client)
    {
        if ((client & (ClientType.Unity | ClientType.Flash)) != ClientType.None)
            throw new UnsupportedClientException(client);
    }
}