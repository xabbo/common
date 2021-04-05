using System;
using System.Collections.Generic;

namespace Xabbo.Messages
{
    public class MessageInfo : IMessageInfo
    {
        public Destination Destination { get; set; }
        public bool IsIncoming => Destination == Destination.Client;
        public bool IsOutgoing => Destination == Destination.Server;
        public string Name { get; set; }
        public string Hash { get; set; }
        public short UnityHeader { get; set; }
        public short Header { get; set; }
        public HashSet<string> Aliases { get; set; }
        IReadOnlyCollection<string> IMessageInfo.Aliases => Aliases;
        public string Structure { get; set; }

        public MessageInfo()
        {
            UnityHeader = -1;
            Header = -1;
            Hash = string.Empty;
            Name = string.Empty;
            Aliases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Structure = string.Empty;
        }
    }
}
