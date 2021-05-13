using System;
using System.Buffers;

namespace Xabbo.Messages
{
    public class InterceptArgs : EventArgs, IDisposable
    {
        private bool _disposed;

        private readonly IMemoryOwner<byte> _originalDataOwner;

        public DateTime Timestamp { get; }
        public Destination Destination { get; }
        public ClientType Client { get; }
        public int Step { get; }
        public IPacket Packet { get; }
        public IReadOnlyPacket OriginalPacket { get; }

        public bool IsIncoming => Destination == Destination.Client;
        public bool IsOutgoing => Destination == Destination.Server;

        public bool IsBlocked { get; private set; }
        public bool IsModified =>
            Packet.Header != OriginalPacket.Header ||
            Packet.Length != OriginalPacket.Length ||
            !Packet.GetBuffer().Span.SequenceEqual(OriginalPacket.GetBuffer().Span);

        public InterceptArgs(Destination destination, ClientType client, int step, IPacket packet)
        {
            Timestamp = DateTime.Now;
            Destination = destination;
            Client = client;
            Step = step;
            Packet = packet;

            _originalDataOwner = MemoryPool<byte>.Shared.Rent(packet.Length);
            packet.CopyTo(_originalDataOwner.Memory.Span);

            OriginalPacket = new Packet(
                client,
                packet.Header,
                _originalDataOwner.Memory[0..packet.Length]
            );
        }

        public void Block() => IsBlocked = true;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;

            if (disposing)
            {
                _originalDataOwner.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
