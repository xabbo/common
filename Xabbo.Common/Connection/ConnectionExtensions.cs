using System.ComponentModel;
using System.Threading.Tasks;

using Xabbo.Messages;
using Xabbo.Connection;

namespace Xabbo;

/// <summary>
/// Provides extensions for sending generic values to a connection.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ConnectionExtensions
{
    /// <summary>
    /// Sends a packet with the specified header to either the client or server, depending on the header destination.
    /// </summary>
    public static void Send(this IConnection connection, Header header)
    {
        using Packet packet = new(header, connection.Client);
        connection.Send(packet);
    }

    /// <summary>
    /// Sends a packet with the specified header to either the client or server, depending on the header destination.
    /// </summary>
    public static async ValueTask SendAsync(this IConnection connection, Header header)
    {
        using Packet packet = new(header, connection.Client);
        await connection.SendAsync(packet);
    }
}
