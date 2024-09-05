namespace Xabbo.Messages;

public interface IMessage<T> : IParserComposer<T> where T : IMessage<T>
{
    /// <summary>
    /// Gets the target identifiers for this message.
    /// </summary>
    static virtual Identifier[] Identifiers => [T.Identifier];

    /// <summary>
    /// Gets whether each of the identifiers should target their specified client.
    /// </summary>
    static virtual bool UseTargetedIdentifiers => false;

    /// <summary>
    /// Gets the identifier for this message.
    /// </summary>
    static abstract Identifier Identifier { get; }

    /// <summary>
    /// Checks whether the specified packet matches this message.
    /// </summary>
    static virtual bool Matches(in PacketReader p) => true;

    /// <summary>
    /// Gets the identifier for this message instance.
    /// </summary>
    virtual Identifier GetIdentifier(ClientType client) => T.Identifier;

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="InterceptCallback{T}"/> .
    /// </summary>
    static InterceptHandler CreateHandler(InterceptCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], (e) => {
            int pos = 0;
            PacketReader r = new(e.Packet, ref pos, e.Interceptor);
            if (!T.Matches(in r)) return;
            pos = 0;
            callback(new Intercept<T>(ref e, r.Parse<T>()));
        })
        {
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
            if (!T.Matches(in r)) return;
            pos = 0;
            callback(r.Parse<T>());
        })
        {
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
            if (!T.Matches(in r)) return;
            pos = 0;
            callback(e, r.Parse<T>());
        })
        {
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }
}