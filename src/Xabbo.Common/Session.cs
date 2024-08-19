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
    /// Gets whether this is a Unity session.
    /// </summary>
    public bool IsUnity => Client.Type == ClientType.Unity;

    /// <summary>
    /// Gets whether this is a Flash session.
    /// </summary>
    public bool IsFlash => Client.Type == ClientType.Flash;

    /// <summary>
    /// Gets whether this is a Shockwave session.
    /// </summary>
    public bool IsShockwave => Client.Type == ClientType.Shockwave;

    public static bool operator ==(Session session, ClientType client) => session.Client.Type == client;
    public static bool operator !=(Session session, ClientType client) => !(session == client);
}