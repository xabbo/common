namespace Xabbo.Messages;

/// <summary>
/// Provides a method for a request message to check whether a response matches the request.
/// </summary>
/// <typeparam name="TRes">The type of the response message.</typeparam>
public interface IRequestFor<TRes> where TRes : IMessage<TRes>
{
    /// <summary>
    /// Gets whether the response message matches the request message.
    /// </summary>
    /// <param name="msg">The response message to check.</param>
    bool MatchResponse(TRes msg) => true;
}
