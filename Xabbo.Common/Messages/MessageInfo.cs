using System;
using System.Collections.Generic;

namespace Xabbo.Messages
{
    public class MessageInfo : IMessageInfo
    {
        public Direction Direction { get; set; }
        public Destination Destination => Direction.ToDestination();
        public bool IsIncoming => Direction == Direction.Incoming;
        public bool IsOutgoing => Direction == Direction.Outgoing;
        public string Name { get; set; }
        public short Header { get; set; }
        public HashSet<string> Aliases { get; set; }
        IReadOnlyCollection<string> IMessageInfo.Aliases => Aliases;

        public MessageInfo()
        {
            Header = -1;
            Name = string.Empty;
            Aliases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}
