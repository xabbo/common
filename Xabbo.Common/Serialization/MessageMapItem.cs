using System;

namespace Xabbo.Serialization;

public class MessageMapItem
{
    public string UnityName { get; set; }
    public short Header { get; set; }
    public string? FlashName { get; set; }

    public MessageMapItem()
    {
        UnityName = string.Empty;
        Header = -1;
    }
}
