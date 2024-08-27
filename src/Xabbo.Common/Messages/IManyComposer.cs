using System.Collections.Generic;

namespace Xabbo.Messages;

public interface IManyComposer<T> where T : IComposer, IManyComposer<T>
{
    static abstract void ComposeAll(in PacketWriter p, IEnumerable<T> items);
}