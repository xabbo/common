using System;
using System.Collections.Generic;

using Xabbo.Messages;

namespace Xabbo.Extension;

/// <summary>
/// Provides data for the <see cref="IExtension.Connected"/> event.
/// </summary>
public sealed class GameConnectedEventArgs : EventArgs
{
    /// <summary>
    /// Gets the host name.
    /// </summary>
    public string Host { get; init; } = string.Empty;

    /// <summary>
    /// Gets the port.
    /// </summary>
    public int Port { get; init; }

    /// <summary>
    /// Gets the client type.
    /// </summary>
    public ClientType ClientType { get; init; } = ClientType.Unknown;

    /// <summary>
    /// Gets the client identifier, if it is available.
    /// </summary>
    public string? ClientIdentifier { get; init; }

    /// <summary>
    /// Gets the client version, if it is available.
    /// </summary>
    public string? ClientVersion { get; init; }

    /// <summary>
    /// Gets the path to the messages file, if it is available.
    /// </summary>
    public string? MessagesPath { get; init; }

    /// <summary>
    /// Gets a list of client specific message information provided by the interceptor service.
    /// </summary>
    public List<IClientMessageInfo> Messages { get; init; } = [];
}
