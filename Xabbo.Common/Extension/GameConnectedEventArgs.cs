using System;
using System.Collections.Generic;

using Xabbo.Messages;

namespace Xabbo.Extension;

public sealed class GameConnectedEventArgs : EventArgs
{
    public string Host { get; init; }
    public int Port { get; init; }
    public string? ClientVersion { get; init; }
    public string? ClientIdentifier { get; init; }
    public ClientType ClientType { get; init; }
    public string? MessagesPath { get; init; }
    public List<IClientMessageInfo> Messages { get; init; } = new();

    public GameConnectedEventArgs()
    {
        Host = string.Empty;
        ClientType = ClientType.Unknown;
    }
}
