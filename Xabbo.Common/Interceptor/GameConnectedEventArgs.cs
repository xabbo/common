using System;

namespace Xabbo.Interceptor
{
    public class GameConnectedEventArgs : EventArgs
    {
        public string Host { get; }
        public int Port { get; }
        public string Version { get; }
        public string MessagesPath { get; }
        public string ClientType { get; }

        public GameConnectedEventArgs(string host, int port,
            string version, string messagesPath, string clientType)
        {
            Host = host;
            Port = port;
            Version = version;
            MessagesPath = messagesPath;
            ClientType = clientType;
        }
    }
}
