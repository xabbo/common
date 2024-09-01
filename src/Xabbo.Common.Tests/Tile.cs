using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public readonly record struct Tile(int X, int Y, float Z) : IParserComposer<Tile>
{
    static Tile IParser<Tile>.Parse(in PacketReader p) => new(p.ReadInt(), p.ReadInt(), p.ReadFloat());

    void IComposer.Compose(in PacketWriter p)
    {
        p.WriteInt(X);
        p.WriteInt(Y);
        p.WriteFloat(Z);
    }
}