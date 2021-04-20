using System;

namespace Xabbo.Serialization
{
    public class MessageMapItem
    {
        public string Name { get; set; }
        public short Header { get; set; }
        public string[] Aliases { get; set; }

        public MessageMapItem()
        {
            Name = string.Empty;
            Header = -1;
            Aliases = Array.Empty<string>();
        }
    }
}
