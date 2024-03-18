﻿using System;

namespace Xabbo.Messages;

public sealed class MessageInfo : IMessageInfo
{
    public Direction Direction { get; set; }
    public bool IsIncoming => Direction == Direction.Incoming;
    public bool IsOutgoing => Direction == Direction.Outgoing;
    public string? UnityName { get; set; }
    public short UnityHeader { get; set; } = -1;
    public string? FlashName { get; set; }
    public short FlashHeader { get; set; } = -1;
}

public interface IClientMessageInfo
{
    ClientType Client { get; }
    Direction Direction { get; }
    short Header { get; }
    string Name { get; }
}

public sealed class ClientMessageInfo : IClientMessageInfo
{
    public ClientType Client { get; set; }
    public Direction Direction { get; set; }
    public short Header { get; set; }
    public string Name { get; set; } = string.Empty;
}
