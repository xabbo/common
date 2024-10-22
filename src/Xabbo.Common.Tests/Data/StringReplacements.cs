using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xabbo.Common.Tests.Data;

public class StringReplacements : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "hello", "world" };
        yield return new object[] { "hello", "bye" };
        yield return new object[] { "hello", "universe" };
        yield return new object[] { "this is a very long string", "hi" };
        yield return new object[] { "hi", "this is a very long string" };
        // Note: fails on outgoing Shockwave if the string length is > 4095, the maximum value of a B64
        yield return new object[] { $"this is a ver{new string('y', 4000)} long string", string.Empty };
        yield return new object[] { string.Empty, $"this is a ver{new string('y', 4000)} long string" };
        yield return new object[] { string.Empty, string.Empty };
        // Latin1 characters 0xa0 ~ 0xff
        yield return new object[] {
            "Latin1 Encoding Test",
            Encoding.Latin1.GetString(Enumerable.Range(0xa0, 0x100-0xa0).Select(x => (byte)x).ToArray())
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
