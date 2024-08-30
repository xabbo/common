using Xabbo.Connection;
using Xabbo.Messages;

namespace Xabbo.Common.Generator.Tests;

public class VariadicTests
{
    static Packet GetPacket() => throw new NotImplementedException();

    // Ensures the variadic Read<T...> methods are generated.
    static void GenerateRead(IPacket p)
    {
        p.Read<int>();
        p.Read<int, int>();
        p.Read<int, int, int>();
        p.Read<int, int, int, int>();

        p.Read<bool, byte, short, int, float, long, string, Id, Length>();
    }

    // Ensures the variadic ReadAt<T...> methods are generated.
    static void GenerateReadAt(IPacket p)
    {
        p.ReadAt<int>(0);
        p.ReadAt<int, int>(0);
        p.ReadAt<int, int, int>(0);
        p.ReadAt<int, int, int, int>(0);

        p.ReadAt<bool, byte, short, int, float, long, string, Id, Length>(0);
    }

    // Ensures the variadic Write<T...> methods are generated.
    static void GenerateWrite(IPacket p)
    {
        p.Write(1);
        p.Write(1, 2);
        p.Write(1, 2, 3);
        p.Write(1, 2, 3, 4);

        p.Write<bool, byte, short, int, float, long, string, Id, Length>(false, 1, 2, 3, 4, 5, "6", 7, 8);
    }

    // Ensures the variadic WriteAt<T...> methods are generated.
    static void GenerateWriteAt(IPacket p)
    {
        p.WriteAt(0, 1);
        p.WriteAt(0, 1, 2);
        p.WriteAt(0, 1, 2, 3);
        p.WriteAt(0, 1, 2, 3, 4);

        p.WriteAt<bool, byte, short, int, float, long, string, Id, Length>(0, false, 1, 2, 3, 4, 5, "6", 7, 8);
    }

    // Ensures the variadic Replace<T...> methods are generated.
    static void GenerateReplace(IPacket p)
    {
        p.Replace(1);
        p.Replace(1, 2);
        p.Replace(1, 2, 3);
        p.Replace(1, 2, 3, 4);

        p.Replace<bool, byte, short, int, float, long, string, Id, Length>(false, 1, 2, 3, 4, 5, "6", 7, 8);
    }

    // Ensures the variadic ReplaceAt<T...> methods are generated.
    static void GenerateReplaceAt(IPacket p)
    {
        p.ReplaceAt(0, 1);
        p.ReplaceAt(0, 1, 2);
        p.ReplaceAt(0, 1, 2, 3);
        p.ReplaceAt(0, 1, 2, 3, 4);

        p.ReplaceAt<bool, byte, short, int, float, long, string, Id, Length>(0, false, 1, 2, 3, 4, 5, "6", 7, 8);
    }

    // TODO: Modify(At)

    static void GenerateSendHeader(IConnection c)
    {
        Header header = new();

        c.Send(header);
        c.Send(header, 1);
        c.Send(header, 1, 2);
        c.Send(header, 1, 2, 3);
        c.Send(header, 1, 2, 3, 4);
    }

    static void GenerateSendIdentifier(IConnection c)
    {
        Identifier identifier = new();

        c.Send(identifier);
        c.Send(identifier, 1);
        c.Send(identifier, 1, 2);
        c.Send(identifier, 1, 2, 3);
        c.Send(identifier, 1, 2, 3, 4);
    }
}