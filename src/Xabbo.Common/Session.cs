namespace Xabbo;

public sealed record Session(Hotel Hotel, Client Client)
{
    public static readonly Session None = new(Hotel.None, Client.None);

    /// <summary>
    /// Gets whether the session is any of the specified clients.
    /// </summary>
    /// <param name="client">
    /// The client to test for. This may be a combination of multiple clients.
    /// </param>
    /// <returns><c>true</c> if the specified client type contains the current session's client.</returns>
    public bool Is(ClientType client) => (Client.Type & client) != 0;

    /// <summary>
    /// Gets whether this is a Unity client session.
    /// </summary>
    public bool IsUnity => Client.Type is ClientType.Unity;

    /// <summary>
    /// Gets whether this is a Flash client session.
    /// </summary>
    public bool IsFlash => Client.Type is ClientType.Flash;

    /// <summary>
    /// Gets whether this is a Shockwave client session.
    /// </summary>
    public bool IsShockwave => Client.Type is ClientType.Shockwave;

    /// <summary>
    /// Gets whether this is an Origins client session.
    /// Alias for IsShockwave.
    /// </summary>
    public bool IsOrigins => Client.Type is ClientType.Shockwave;

    /// <summary>
    /// Gets whether this is a modern client session.
    /// Alias for !IsShockwave.
    /// </summary>
    public bool IsModern => Client.Type is not ClientType.Shockwave;

    public static bool operator ==(Session session, ClientType client) => session.Client.Type == client;
    public static bool operator !=(Session session, ClientType client) => !(session == client);
}