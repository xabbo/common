using System;

namespace Xabbo.Messages
{
    public class MessageInfo : IMessageInfo
    {
        public Direction Direction { get; set; }
        public Destination Destination => Direction.ToDestination();
        public bool IsIncoming => Direction == Direction.Incoming;
        public bool IsOutgoing => Direction == Direction.Outgoing;
        public string Name { get; set; }
        public string? UnityName { get; set; }
        public string? FlashName { get; set; }
        public short Header { get; set; }

        public MessageInfo()
        {
            Name = string.Empty;
            Header = -1;
        }
    }
}
