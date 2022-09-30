using System;
using Xabbo.Common;

namespace Xabbo.Messages;

/// <summary>
/// Thrown when attempting to access the value of a header which has not been resolved.
/// </summary>
public sealed class UnresolvedHeaderException : Exception
{
    private static string BuildMessage(ClientType client, Header header)
    {
        return $"Unresolved {(header.IsOutgoing ? "outgoing" : "incoming")} header value for {client} message: \"{header.GetName(client) ?? "?"}\".";
    }

    /// <summary>
    /// The client for which the header is unresolved.
    /// </summary>
    public ClientType Client { get; }

    /// <summary>
    /// The unresolved header.
    /// </summary>
    public Header Header { get; }

    internal UnresolvedHeaderException(ClientType client, Header header)
        : base(BuildMessage(client, header))
    {
        Client = client;
        Header = header;
    }
}
