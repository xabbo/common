namespace Xabbo.Messages;

/// <summary>
/// A header dictionary that provides named incoming header properties.
/// </summary>
public sealed partial class Incoming : Headers
{
    public Incoming()
        : base(Direction.Incoming)
    { }
}
