using System;
using System.Collections.Generic;

namespace Xabbo.Serialization
{
    public class MessageMap
    {
        public List<MessageMapItem> Incoming { get; set; }
        public List<MessageMapItem> Outgoing { get; set; }

        public MessageMap()
        {
            Incoming = new List<MessageMapItem>();
            Outgoing = new List<MessageMapItem>();
        }
    }
}
