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
    /// Gets the identifier for this message instance.
    /// </summary>
    public virtual Identifier GetIdentifier(ClientType client) => T.Identifier;

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="InterceptCallback{T}"/> .
    /// </summary>
    public static InterceptHandler CreateHandler(InterceptCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], (e) => {
            int pos = 0;
            callback(new Intercept<T>(ref e, e.Packet.ReaderAt(ref pos).Parse<T>()));
        })
        {
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="MessageCallback{T}"/> .
    /// </summary>
    public static InterceptHandler CreateHandler(MessageCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], (e) =>
        {
            int pos = 0;
            callback(e.Packet.ReaderAt(ref pos).Parse<T>());
        })
        {
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }

    /// <summary>
    /// Creates an <see cref="InterceptHandler"/> for the specified <see cref="InterceptMessageCallback{T}"/> .
    /// </summary>
    public static InterceptHandler CreateHandler(InterceptMessageCallback<T> callback)
    {
        return new InterceptHandler([.. T.Identifiers], e =>
        {
            int pos = 0;
            callback(e, e.Packet.ReaderAt(ref pos).Parse<T>());
        })
        {
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }
}