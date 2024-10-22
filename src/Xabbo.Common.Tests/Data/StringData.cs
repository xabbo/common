using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xabbo.Common.Tests;

public class StringData : IEnumerable<object[]>
{
    private static readonly string[] _strings = [
        "",
        "test",
        $"a ver{new string('y', 4000)} long string",
        // Printable ASCII 0x20 ~ 0x7e
        Encoding.ASCII.GetString(Enumerable.Range(0x20, 0x7f-0x20).Select(x => (byte)x).ToArray()),
        // Latin1 characters 0xa0 ~ 0xff
        Encoding.Latin1.GetString(Enumerable.Range(0xa0, 0x100-0xa0).Select(x => (byte)x).ToArray()),
        // Undefined Latin1 range 0x7f ~ 0x9f
        Encoding.Latin1.GetString(Enumerable.Range(0x7f, 0xa0-0x7f).Select(x => (byte)x).ToArray())
        // "💀", fails with Latin1 encoding
    ];

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var value in _strings)
            yield return [value];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}