namespace Xabbo.Messages;

public interface IRequestMessage<TRequest, TResponse> : IMessage<TRequest>
    where TRequest : IMessage<TRequest>
    where TResponse : IMessage<TResponse>
{
    /// <summary>
    /// Gets whether the response is a match for this request message.
    /// </summary>
    bool MatchResponse(TResponse response) => true;
}
