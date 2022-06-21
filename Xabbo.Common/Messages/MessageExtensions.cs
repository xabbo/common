using System;
using System.ComponentModel;

namespace Xabbo.Messages;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class MessageExtensions
{
    public static Direction ToDirection(this Destination destination) => destination switch
    {
        Destination.Client => Direction.Incoming,
        Destination.Server => Direction.Outgoing,
        _ => Direction.Unknown
    };

    public static Destination ToDestination(this Direction direction) => direction switch
    {
        Direction.Incoming => Destination.Client,
        Direction.Outgoing => Destination.Server,
        _ => Destination.Unknown
    };
}
