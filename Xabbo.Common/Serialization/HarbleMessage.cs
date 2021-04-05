using System;

namespace Xabbo.Serialization
{
    public class HarbleMessage
    {
        public string Name { get; set; }
        public short Id { get; set; }
        public string Hash { get; set; }
        public bool IsOutgoing { get; set; }
        public string ClassName { get; set; }

        public HarbleMessage()
        {
            Name = string.Empty;
            Id = -1;
            Hash = string.Empty;
            ClassName = string.Empty;
        }
    }
}
