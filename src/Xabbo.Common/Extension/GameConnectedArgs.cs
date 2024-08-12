using System;
using System.Collections.Generic;

using Xabbo.Messages;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IExtension.Connected"/> event.
/// </summary>
public sealed class GameConnectedArgs
{
    /// <summary>
    /// Gets the host for the current game connection.
    /// </summary>
    public string Host { get; init; } = string.Empty;

    /// <summary>
    /// Gets the port for the current game connection.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Gets the client for the current game connection.
    /// </summary>
    public Client Client { get; init; }

    /// <summary>
    /// Gets the path to the messages file, if it is available.
    /// </summary>
    public string? MessagesFilePath { get; init; }

    /// <summary>
    /// Gets a list of client-specific message information.
    /// </summary>
    public List<ClientMessage> Messages { get; init; } = [];
}
