namespace Xabbo.Messages;

public interface IRequestFor<TResponse> where TResponse : IMessage<TResponse>
{
    /// <summary>
    /// Gets whether the response is a match for this request message.
    /// </summary>
    bool MatchResponse(TResponse msg) => true;
}
