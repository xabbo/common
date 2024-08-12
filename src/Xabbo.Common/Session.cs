namespace Xabbo;

public sealed record Session(Hotel Hotel, Client Client)
{
    public static readonly Session None = new(Hotel.None, Client.None);
}