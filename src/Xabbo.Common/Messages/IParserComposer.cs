namespace Xabbo.Messages;

/// <summary>
/// Represents an object that can be parsed from and composed to a packet.
/// </summary>
public interface IParserComposer<T> : IComposer, IParser<T> where T : IParser<T>;
