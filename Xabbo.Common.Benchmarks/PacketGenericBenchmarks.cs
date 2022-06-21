using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xabbo.Messages;

namespace Xabbo.Common.Benchmarks;

[MemoryDiagnoser]
public class PacketGenericBenchmarks
{
    [Params(1, 10, 100)]
    public int Packets { get; set; }

    [Params(1, 10, 100)] 
    public int Writes { get; set; }

    [Benchmark(Baseline = true)]
    public void WritePacket()
    {
        for (int i = 0; i < Packets; i++)
        {
            Packet packet = new Packet();
            for (int j = 0; j < Writes; j++)
            {
                packet.Write(1, "this", 2, "is", 3, "a", 4, "test");
            }
        }
    }

    [Benchmark]
    public void WriteIPacket()
    {
        for (int i = 0; i < Packets; i++)
        {
            IPacket packet = new Packet();
            for (int j = 0; j < Writes; j++)
            {
                packet.Write(1, "this", 2, "is", 3, "a", 4, "test");
            }
        }
    }
}
