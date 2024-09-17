using System;
using System.Runtime.CompilerServices;

namespace Xabbo.Messages;

public interface IMessage<T> : IMessage, IParserComposer<T> where T : IMessage<T>
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

    Identifier IMessage.GetIdentifier(ClientType client) => T.Identifier;

    /// <summary>
    /// Checks whether the specified packet matches this message.
    /// </summary>
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
                e.Packet.Header = e.Interceptor.Messages.Resolve(
                    modified.GetIdentifier(e.Interceptor.Session.Client.Type));
                e.Packet.Clear();
                e.Packet.Writer().Compose(modified);
            }
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
            if (!T.Match(in r)) return;
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
            if (!T.Match(in r)) return;
            pos = 0;
            callback(e, r.Parse<T>());
        })
        {
            UseTargetedIdentifiers = T.UseTargetedIdentifiers
        };
    }
}