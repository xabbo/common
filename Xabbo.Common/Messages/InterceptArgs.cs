using System;
using System.Buffers;

namespace Xabbo.Messages
{
    public class InterceptArgs : EventArgs, IDisposable
    {
        private bool _disposed;

        private readonly Header _originalHeader = Header.Unknown;
        private readonly IMemoryOwner<byte> _originalDataOwner;
        private readonly ReadOnlyMemory<byte> _originalData;

        public DateTime Timestamp { get; }
        public Destination Destination { get; }
        public ClientType Client { get; }
        public int Step { get; }
        public IPacket Packet { get; }

        public bool IsIncoming => Destination == Destination.Client;
        public bool IsOutgoing => Destination == Destination.Server;

        public bool IsBlocked { get; private set; }
        public bool IsModified =>
            Packet.Header != _originalHeader ||
            Packet.Length != _originalData.Length ||
            !Packet.GetBuffer().Span.SequenceEqual(_originalData.Span);

        public InterceptArgs(Destination destination, ClientType client, int step, IPacket packet)
        {
            Timestamp = DateTime.Now;
            Destination = destination;
            Client = client;
            Step = step;
            Packet = packet;

            _originalHeader = packet.Header;
            _originalDataOwner = MemoryPool<byte>.Shared.Rent(packet.Length);
            _originalData = _originalDataOwner.Memory[0..packet.Length];

            packet.CopyTo(_originalDataOwner.Memory.Span);
        }

        public void Block() => IsBlocked = true;

        public IPacket GetOriginalPacket()
        {
            return new Packet(_originalData.Span) { Header = _originalHeader };
        }

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
