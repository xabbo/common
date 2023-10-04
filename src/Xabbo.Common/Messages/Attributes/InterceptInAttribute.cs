﻿namespace Xabbo.Messages;

public sealed class InterceptInAttribute : InterceptAttribute
{
    public InterceptInAttribute(params string[] identifiers)
        : base(Direction.Incoming, identifiers)
    { }
}
