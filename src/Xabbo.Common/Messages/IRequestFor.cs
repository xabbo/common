namespace Xabbo.Messages;

/// <summary>
/// Allows a request message to check whether a response matches the request.
/// </summary>
/// <typeparam name="TRes">The type of the response message.</typeparam>
public interface IRequestFor<TRes> where TRes : IMessage<TRes>
{
    /// <summary>
    /// Gets whether the response is a match for this request message.
    /// </summary>
    bool MatchResponse(TRes msg) => true;
}
