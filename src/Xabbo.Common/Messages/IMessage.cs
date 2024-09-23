namespace Xabbo.Messages;

public interface IMessage : IComposer
{
    /// <summary>
    /// Gets the identifier for this message instance.
    /// </summary>
    abstract Identifier GetIdentifier(ClientType client);

    /// <summary>
    /// Gets the clients that this message is supported on.
    /// </summary>
    abstract ClientType GetSupportedClients();
}
