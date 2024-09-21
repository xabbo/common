namespace Xabbo.Messages;

public interface IRequestMessage<TReq, TRes, TData> : IMessage<TReq>, IRequestFor<TRes>, IResponseData<TRes, TData>
    where TReq : IMessage<TReq>
    where TRes : IMessage<TRes>;

public interface IRequestMessage<TReq, TRes> : IMessage<TReq>, IRequestFor<TRes>, IResponseData<TRes, TRes>
    where TReq : IMessage<TReq>
    where TRes : IMessage<TRes>
{
    TRes IResponseData<TRes, TRes>.GetData(TRes msg) => msg;
}
