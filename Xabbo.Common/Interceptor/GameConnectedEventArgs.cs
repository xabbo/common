using System;
using System.Collections.Generic;

using Xabbo.Messages;

namespace Xabbo.Interceptor
{
    public class GameConnectedEventArgs : EventArgs
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string? ClientVersion { get; init; }
        public string? ClientIdentifier { get; init; }
        public ClientType ClientType { get; init; }
        public string? MessagesPath { get; init; }
        public List<MessageInfo> Messages { get; init; } = new List<MessageInfo>();

        public GameConnectedEventArgs()
        {
            Host = string.Empty;
            ClientType = ClientType.Unknown;
            Messages = new List<MessageInfo>();
        }
    }
}
