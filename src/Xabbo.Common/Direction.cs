using System;

namespace Xabbo;

/// <summary>
/// Represents the direction of a message.
/// </summary>
[Flags]
public enum Direction
{
    None = 0,
    In = 1 << 0,
    Out = 1 << 1,
    Both = In | Out
}
