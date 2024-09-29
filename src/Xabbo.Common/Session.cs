namespace Xabbo;

/// <summary>
/// Represents a connection session.
/// </summary>
/// <param name="Hotel">The hotel of the current session.</param>
/// <param name="Client">The client of the current session.</param>
public sealed record Session(Hotel Hotel, Client Client)
{
    /// <summary>
    /// Represents no session.
    /// </summary>
    public static readonly Session None = new(Hotel.None, Client.None);

    /// <summary>
    /// Gets whether the session is any of the specified clients.
    /// </summary>
    /// <param name="client">
    /// The client to test for. This may be a combination of multiple clients.
    /// </param>
    /// <returns><c>true</c> if the specified client type contains the current session's client.</returns>
    public bool Is(ClientType client) => (Client.Type & client) != 0;

    public static bool operator ==(Session session, ClientType client) => session.Client.Type == client;
    public static bool operator !=(Session session, ClientType client) => !(session == client);
}