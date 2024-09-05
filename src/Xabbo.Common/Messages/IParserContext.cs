namespace Xabbo.Messages;

public interface IParserContext
{
    /// <summary>
    /// Gets the message manager.
    /// </summary>
    IMessageManager Messages { get; }
}