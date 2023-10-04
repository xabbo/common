namespace Xabbo.Messages;

public sealed class InterceptOutAttribute : InterceptAttribute
{
    public InterceptOutAttribute(params string[] identifiers)
        : base(Direction.Outgoing, identifiers)
    { }
}
