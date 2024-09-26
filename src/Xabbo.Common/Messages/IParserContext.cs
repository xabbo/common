namespace Xabbo.Messages;

/// <summary>
/// Provides additional context to a <see cref="PacketReader"/> or <see cref="PacketWriter"/>.
/// </summary>
public interface IParserContext
{
    /// <summary>
    /// Gets the message manager.
    /// </summary>
    IMessageManager Messages { get; }
}
