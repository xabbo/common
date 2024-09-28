namespace Xabbo.Messages;

/// <summary>
/// Represents a message with an associated identifier that can be parsed from and composed to a packet.
/// </summary>
public interface IMessage<T> : IMessage, IParserComposer<T> where T : IMessage<T>
{
    /// <summary>
    /// Gets the target identifiers for this message.
    /// </summary>
    /// <remarks>
    /// The default implementation returns an array containing the single <see cref="Identifier"/>.
    /// </remarks>
    static virtual Identifier[] Identifiers => [T.Identifier];

    /// <summary>
    /// Gets whether each of the identifiers should only intercept on their respective client types.
    /// </summary>
    /// <remarks>
    /// The default implementation returns false.
    /// </remarks>
    static virtual bool UseTargetedIdentifiers => false;

    /// <summary>
    /// Gets the client types that this message supports.
    /// </summary>
    static virtual ClientType SupportedClients => ClientType.All;
    ClientType IMessage.GetSupportedClients() => T.SupportedClients;

    /// <summary>
    /// Gets the identifier for this message.
    /// </summary>
    static abstract Identifier Identifier { get; }

    /// <inheritdoc/>
    /// <remarks>
    /// The default implementation returns the static <see cref="Identifier"/>.
    /// </remarks>
    Identifier IMessage.GetIdentifier(ClientType client) => T.Identifier;

    /// <summary>
    /// Checks whether the specified packet matches this message.
    /// </summary>
    /// <remarks>
    /// The default implementation returns true.
    /// </remarks>
    static virtual bool Match(in PacketReader p) => true;

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="InterceptCallback{T}"/> .
    /// </summary>
    static InterceptHandler CreateHandler(InterceptCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], (e) => {
            int pos = 0;
            PacketReader r = new(e.Packet, ref pos, e.Interceptor);
            if (!T.Match(in r)) return;
            pos = 0;
            callback(new Intercept<T>(ref e));
        })
        {
            Target = T.SupportedClients,
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="ModifyMessageCallback{T}"/> .
    /// </summary>
    static InterceptHandler CreateHandler(ModifyMessageCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], (e) => {
            int pos = 0;
            PacketReader r = new(e.Packet, ref pos, e.Interceptor);
            if (!T.Match(in r)) return;
            pos = 0;
            T msg = T.Parse(in r);
            IMessage? modified = callback(msg);
            if (modified is not null)
            {
                if (ReferenceEquals(modified, Block))
                {
                    e.Block();
                }
                else
                {
                    UnsupportedClientException.ThrowIf(e.Interceptor.Session.Client.Type, ~modified.GetSupportedClients());
                    e.Packet.Header = e.Interceptor.Messages.Resolve(
                        modified.GetIdentifier(e.Interceptor.Session.Client.Type));
                    e.Packet.Clear();
                    e.Packet.Writer().Compose(modified);
                }
            }
        })
        {
            Target = T.SupportedClients,
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="MessageCallback{T}"/> .
    /// </summary>
    static InterceptHandler CreateHandler(MessageCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], (e) =>
        {
            int pos = 0;
            PacketReader r = new(e.Packet, ref pos, e.Interceptor);
            if (!T.Match(in r)) return;
            pos = 0;
            callback(r.Parse<T>());
        })
        {
            Target = T.SupportedClients,
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="InterceptMessageCallback{T}"/> .
    /// </summary>
    static InterceptHandler CreateHandler(InterceptMessageCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], e =>
        {
            int pos = 0;
            PacketReader r = new(e.Packet, ref pos, e.Interceptor);
            if (!T.Match(in r)) return;
            pos = 0;
            callback(e, r.Parse<T>());
        })
        {
            Target = T.SupportedClients,
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }
}
