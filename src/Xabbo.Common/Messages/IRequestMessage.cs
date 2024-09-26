namespace Xabbo.Messages;

/// <summary>
/// Represents a request message with an associated response message and response data type.
/// </summary>
/// <typeparam name="TReq">The type of the request message.</typeparam>
/// <typeparam name="TRes">The type of the response message.</typeparam>
/// <typeparam name="TData">The type of the response data.</typeparam>
public interface IRequestMessage<TReq, TRes, TData> : IMessage<TReq>, IRequestFor<TRes>, IResponseData<TRes, TData>
    where TReq : IMessage<TReq>
    where TRes : IMessage<TRes>;

/// <summary>
/// Represents a request message with an associated response message.
/// </summary>
/// <typeparam name="TReq">The type of the request message.</typeparam>
/// <typeparam name="TRes">The type of the response message.</typeparam>
public interface IRequestMessage<TReq, TRes> : IRequestMessage<TReq, TRes, TRes>
    where TReq : IMessage<TReq>
    where TRes : IMessage<TRes>
{
    TRes IResponseData<TRes, TRes>.GetData(TRes msg) => msg;
}
