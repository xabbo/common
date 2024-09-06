using System;

using Xabbo.Interceptor;
using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public sealed record MoveFloorItemMsg(Id Id, int X, int Y, int Direction) : IMessage<MoveFloorItemMsg>
{
    public static Identifier Identifier => (ClientType.Flash, Xabbo.Direction.Out, "MoveObject");
    public static MoveFloorItemMsg Parse(in PacketReader p) => throw new NotImplementedException();
    public void Compose(in PacketWriter p)
    {
        switch (p.Client)
        {
            case ClientType.Unity or ClientType.Flash:
                p.WriteId(Id);
                p.WriteInt(X);
                p.WriteInt(Y);
                p.WriteInt(Direction);
                break;
            case ClientType.Shockwave:
                p.Content = $"{Id} {X} {Y} {Direction}";
                break;
            default:
                throw new UnsupportedClientException(p.Client);
        }
    }
}

public sealed record WalkMsg(int X, int Y) : IMessage<WalkMsg>
{
    static Identifier IMessage<WalkMsg>.Identifier => (ClientType.Flash, Direction.Out, "MoveAvatar");

    public static WalkMsg Parse(in PacketReader p) => p.Client switch
    {
        ClientType.Shockwave => new WalkMsg(p.ReadB64(), p.ReadB64()),
        _ => new WalkMsg(p.ReadInt(), p.ReadInt()),
    };

    public void Compose(in PacketWriter p)
    {
        if (p.Client == ClientType.Shockwave)
        {
            p.WriteB64((B64)X);
            p.WriteB64((B64)Y);
        }
        else
        {
            p.WriteInt(X);
            p.WriteInt(Y);
        }
    }
}

public class TestInterceptor
{
    public static void Test(IInterceptor ext)
    {
        // message only
        ext.Intercept<MoveFloorItemMsg>(msg => {
            Console.WriteLine($"Moving floor item {msg.Id} to {msg.X}, {msg.Y}");
        });

        // intercept + message (if block/modification is needed)
        ext.Intercept<MoveFloorItemMsg>((e, msg) => {
            e.Block();
            Console.WriteLine($"Moving floor item {msg.Id} to {msg.X}, {msg.Y}");
        });
    }

    private static Identifier[] GetIdentifiers<T>() where T : IMessage<T>
        => T.Identifiers;

    public IDisposable? Attach(IMessageDispatcher dispatcher)
    {
        return dispatcher.Register(new InterceptGroup([
            new InterceptHandler([.. GetIdentifiers<WalkMsg>()], Intercept<WalkMsg>.Wrap(HandleMove))
        ]));
    }

    [Intercepts]
    void HandleMove(Intercept<WalkMsg> e)
    {
        e.Block();

        Console.WriteLine($"moving item to {e.Msg.X}, {e.Msg.Y}");

        Send(new MoveFloorItemMsg(123456, 3, 4, 2));
    }


    void Send(object o) { }
}