namespace Xabbo.Messages;

/// <summary>
/// Allows a request message to check whether a response matches the request.
/// </summary>
/// <typeparam name="TResponse">The type of the response message.</typeparam>
public interface IRequestFor<TResponse> where TResponse : IMessage<TResponse>
{
    /// <summary>
    /// Gets whether the response is a match for this request message.
    /// </summary>
    bool MatchResponse(TResponse msg) => true;
}
