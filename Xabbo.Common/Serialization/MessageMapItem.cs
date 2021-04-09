using System;
using System.Collections.Generic;

namespace Xabbo.Serialization
{
    public class MessageMapItem
    {
        public string Name { get; set; }
        public short Header { get; set; }
        public List<string> Aliases { get; set; }

        public MessageMapItem()
        {
            Name = string.Empty;
            Header = -1;
            Aliases = new List<string>();
        }
    }
}
