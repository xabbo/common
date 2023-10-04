using System.Threading.Tasks;

using Xabbo;
using Xabbo.Messages;

namespace Xabbo.Connection;

public abstract partial class ConnectionBase : IConnection
{
    #region Generic Send
    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1>(Header header, T1 arg1)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1>(Header header, T1 arg1)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2>(Header header, T1 arg1, T2 arg2)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2>(Header header, T1 arg1, T2 arg2)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3>(Header header, T1 arg1, T2 arg2, T3 arg3)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3>(Header header, T1 arg1, T2 arg2, T3 arg3)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9);
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
    {
        using Packet packet = new Packet(header, Client);
        packet
            .Write<Packet, T1>(arg1)
            .Write<Packet, T2>(arg2)
            .Write<Packet, T3>(arg3)
            .Write<Packet, T4>(arg4)
            .Write<Packet, T5>(arg5)
            .Write<Packet, T6>(arg6)
            .Write<Packet, T7>(arg7)
            .Write<Packet, T8>(arg8)
            .Write<Packet, T9>(arg9);
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected void Send<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19, T20 arg20)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header and values to either the client or server, depending on the header destination.
    /// </summary>
    protected async ValueTask SendAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Header header, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, T16 arg16, T17 arg17, T18 arg18, T19 arg19, T20 arg20)
    {
        using Packet packet = new Packet(header, Client);
        packet
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
        await SendAsync(packet);
    }
    #endregion
}