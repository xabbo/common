using System;

namespace Xabbo.Messages;

public interface IMessage : IComposer
{
    /// <summary>
    /// Blocks the packet when returned from a <see cref="ModifyMessageCallback{T}"/>.
    /// </summary>
    public static readonly IMessage Block = new BlockMessage();

    /// <summary>
    /// Gets the identifier for this message instance.
    /// </summary>
    abstract Identifier GetIdentifier(ClientType client);

    /// <summary>
    /// Gets the clients that this message is supported on.
    /// </summary>
    abstract ClientType GetSupportedClients();
}

internal sealed class BlockMessage : IMessage
{
    public void Compose(in PacketWriter p) => throw new NotSupportedException("Block message cannot be written to a packet.");
    public Identifier GetIdentifier(ClientType client) => Identifier.Unknown;
    public ClientType GetSupportedClients() => ClientType.None;
}
