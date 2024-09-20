namespace Xabbo.Messages;

public interface IRequestMessage<TRequest, TResponse> : IMessage<TRequest>, IRequestFor<TResponse>
    where TRequest : IMessage<TRequest>
    where TResponse : IMessage<TResponse>;
