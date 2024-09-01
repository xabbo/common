namespace Xabbo.Messages;

public interface IMessage : IComposer
{
    /// <summary>
    /// Gets the header of this message.
    /// <para/>
    /// This will only be used if <see cref="Identifier"/> is set to <see cref="Identifier.Unknown"/>.
    /// </summary>
    Header Header => Header.Unknown;

    /// <summary>
    /// Gets the identifier of this message.
    /// <para/>
    /// If this is <see cref="Identifier.Unknown"/>, the <see cref="Header"/> will be used instead.
    /// </summary>
    Identifier Identifier { get; }
}