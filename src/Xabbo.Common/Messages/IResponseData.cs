namespace Xabbo.Messages;

public interface IResponseData<TMsg, TData>
{
    TData GetData(TMsg msg);
}
