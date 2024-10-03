using Xabbo.Messages;

namespace Xabbo.Common.Tests;

public class ReplaceTests
{
    public void ReplaceArray()
    {
        Packet packet = new(Header.Unknown, ClientType.Flash);

        int[] array = [1, 2, 3];

        packet.Write<int[]>([1, 2, 3]);


    }
}