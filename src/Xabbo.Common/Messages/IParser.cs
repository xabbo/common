namespace Xabbo.Messages;

/// <summary>
/// Represents an object that can be parsed from a packet.
/// </summary>
public interface IParser<T> where T : IParser<T>
{
    /// <summary>
    /// Parses an object of type <typeparamref name="T"/> from a packet using the specified <see cref="PacketReader"/>.
    /// </summary>
    static abstract T Parse(in PacketReader p);
}
