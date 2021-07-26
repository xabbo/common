using System;

namespace Xabbo.Messages
{
    [Flags]
    public enum Direction
    {
        Unknown = 0,
        Incoming = 1,
        Outgoing = 2,
        Both = Incoming | Outgoing
    }
}
