using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xabbo.Messages;

namespace Xabbo.Common.Benchmarks;

[MemoryDiagnoser]
public class PacketBenchmarks
{
    [Params(1_000)]
    public int Packets { get; set; }

    [Params(1_000)]
    public int Writes { get; set; }

    [Benchmark(Baseline = true)]
    public void WriteParams()
    {
        for (int i = 0; i < Packets; i++)
        {
            IPacket packet = new Packet();
            for (int j = 0; j < Writes; j++)
            {
                WriteParams(packet, i, j, i, j);
            }
        }
    }

    [Benchmark]
    public void WriteObject()
    {
        for (int i = 0; i < Packets; i++)
        {
            IPacket packet = new Packet();
            for (int j = 0; j < Writes; j++)
            {
                WriteObjects(packet, i, j, i, j);
            }
        }
    }

    [Benchmark]
    public void WriteGeneric()
    {
        for (int i = 0; i < Packets; i++)
        {
            Packet pp = new Packet();
            ((IPacket)pp).Write(1, 2, 3, 4);
            IPacket packet = new Packet();

            packet.Write(1, 2, 3, 4);

            // packet.Write(1, 2, 3, 4);
            // packet.Write(3, 4, 5);
            for (int j = 0; j < Writes; j++)
            {
                WriteGeneric(packet, i, j, i, j);
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WriteParams(IPacket packet, params object[] values)
    {
        foreach (object value in values)
        {
            switch (value)
            {
                case int i: packet.WriteInt(i); break;
                case string s: packet.WriteString(s); break;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WriteObjects(IPacket packet, object a, object b, object c, object d)
    {
        WriteObject(packet, a);
        WriteObject(packet, b);
        WriteObject(packet, c);
        WriteObject(packet, d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WriteObject(IPacket packet, object value)
    {
        switch (value)
        {
            case int i: packet.WriteInt(i); break;
            case string s: packet.WriteString(s); break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WriteGeneric<A, B, C, D>(IPacket packet, A a, B b, C c, D d)
    {
        WriteGeneric(packet, a);
        WriteGeneric(packet, b);
        WriteGeneric(packet, c);
        WriteGeneric(packet, d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WriteGeneric<T>(IPacket packet, T value)
    {
        switch (value)
        {
            case int i: packet.WriteInt(i); break;
            case string s: packet.WriteString(s); break;
        }
    }
}