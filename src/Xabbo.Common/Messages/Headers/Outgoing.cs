namespace Xabbo.Messages;

/// <summary>
/// A header dictionary that provides named outgoing header properties.
/// </summary>
public sealed partial class Outgoing : Headers
{
    public Outgoing()
        : base(Direction.Outgoing)
    { }
}
