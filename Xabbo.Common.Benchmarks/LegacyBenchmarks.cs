using System;

using BenchmarkDotNet.Attributes;

using Xabbo.Messages;

namespace Xabbo.Common.Benchmarks
{
    [MemoryDiagnoser]
    public class LegacyBenchmarks
    {
        [Params(1000)]
        public int Packets { get; set; }

        [Params(100)]
        public int Writes { get; set; }

        [Benchmark(Baseline = true)]
        public void WriteInt()
        {
            for (int i = 0; i < Packets; i++)
            {
                IPacket packet = new Packet(ClientType.Flash, Header.Unknown);
                for (int j = 0; j < Writes; j++)
                {
                    packet.WriteInt(j);
                }
            }
        }

        [Benchmark]
        public void WriteLegacyLong()
        {
            for (int i = 0; i < Packets; i++)
            {
                IPacket packet = new Packet(ClientType.Flash, Header.Unknown);
                for (int j = 0; j < Writes; j++)
                {
                    packet.WriteLegacyLong(j);
                }
            }
        }

    }
}
