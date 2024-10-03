namespace Xabbo.Common.Generator.Tests;

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

internal static class DirectionExtensions
{
    public static string ToShortString(this Direction direction) => direction switch
    {
        Direction.In => "In",
        Direction.Out => "Out",
        Direction.Both => "Both",
        _ => "None"
    };

    public static string ToLongString(Direction direction) => direction switch
    {
        Direction.In => "Incoming",
        Direction.Out => "Outgoing",
        Direction.Both => "Both",
        _ => "None"
    };
}