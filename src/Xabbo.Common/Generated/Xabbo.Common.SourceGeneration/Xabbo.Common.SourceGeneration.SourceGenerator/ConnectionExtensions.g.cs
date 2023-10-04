using System.Threading.Tasks;

using Xabbo.Messages;
using Xabbo.Connection;

namespace Xabbo;

public static partial class ConnectionExtensions
{
    #region Generic Send
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1>(this IConnection connection, Header header, T1 arg1)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1>(this IConnection connection, Header header, T1 arg1)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2>(this IConnection connection, Header header, T1 arg1, T2 arg2)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2>(this IConnection connection, Header header, T1 arg1, T2 arg2)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17)
            .Write<Packet, T18>(arg18);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17)
            .Write<Packet, T18>(arg18);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17)
            .Write<Packet, T18>(arg18)
            .Write<Packet, T19>(arg19);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17)
            .Write<Packet, T18>(arg18)
            .Write<Packet, T19>(arg19);
        await connection.SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19, T20 arg20)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17)
            .Write<Packet, T18>(arg18)
            .Write<Packet, T19>(arg19)
            .Write<Packet, T20>(arg20);
        connection.Send(packet);
    }
    
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(this IConnection connection, Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19, T20 arg20)
    {
        using Packet packet = new Packet(header, connection.Client)
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9)
            .Write<Packet, T10>(arg10)
            .Write<Packet, T11>(arg11)
            .Write<Packet, T12>(arg12)
            .Write<Packet, T13>(arg13)
            .Write<Packet, T14>(arg14)
            .Write<Packet, T15>(arg15)
            .Write<Packet, T16>(arg16)
            .Write<Packet, T17>(arg17)
            .Write<Packet, T18>(arg18)
            .Write<Packet, T19>(arg19)
            .Write<Packet, T20>(arg20);
        await connection.SendAsync(packet);
    }
    #endregion
}