namespace Xabbo.Messages;

/// <summary>
/// Represents a response message with an associated data type.
/// </summary>
/// <typeparam name="TMsg">The type of the response message.</typeparam>
/// <typeparam name="TData">The type of the response data.</typeparam>
public interface IResponseData<TMsg, TData>
{
    /// <summary>
    /// Extracts the data from the response message.
    /// </summary>
    /// <param name="msg">The response message.</param>
    /// <returns>The extracted data.</returns>
    TData GetData(TMsg msg);
}
