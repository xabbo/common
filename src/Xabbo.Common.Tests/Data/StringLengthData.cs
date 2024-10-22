using System.Collections;
using System.Collections.Generic;

namespace Xabbo.Common.Tests;

public class StringLengthData : IEnumerable<object[]>
{
    private static readonly int[] _lengths = [
        0, 1, 10, 100, 1000, 4000,
        // B64.MaxValue boundary
        4095, 4096,
        // short.MaxValue boundary
        32767, 32768,
        // ushort.MaxValue boundary
        65535, 65536,
        100000
    ];

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var value in _lengths)
            yield return [value];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}