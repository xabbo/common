using System;

namespace Xabbo.Messages
{
    /// <summary>
    /// A composable object that can be serialized to a packet.
    /// </summary>
    public interface IComposable
    {
        void Compose(IPacket packet, ClientType clientType = ClientType.Unknown);
    }
}
