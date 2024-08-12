using System.Threading.Tasks;

using Xabbo.Messages;
using Xabbo.Connection;

namespace Xabbo;

public static partial class ConnectionExtensions
{
    #region Generic Send
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1>(this IConnection connection, Identifier identifier, T1 arg1)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1>(this IConnection connection, Header header, T1 arg1)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2>(this IConnection connection, Header header, T1 arg1, T2 arg2)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        connection.Send(packet);
    }
    /// <summary>
    /// Sends a packet with the specified identifier and values to the client or server, depending on the identifier's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IConnection connection, Identifier identifier, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        using Packet packet = new Packet(connection.Messages.Resolve(identifier));
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        packet.Write<T10>(arg10);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to the client or server, depending on the header's direction.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        using Packet packet = new Packet(header);
        packet.Write<T1>(arg1);
        packet.Write<T2>(arg2);
        packet.Write<T3>(arg3);
        packet.Write<T4>(arg4);
        packet.Write<T5>(arg5);
        packet.Write<T6>(arg6);
        packet.Write<T7>(arg7);
        packet.Write<T8>(arg8);
        packet.Write<T9>(arg9);
        packet.Write<T10>(arg10);
        connection.Send(packet);
    }
    #endregion
}