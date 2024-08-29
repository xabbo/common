using System.Collections;
using System.Collections.Generic;

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
        yield return new object[] { "this is a very long string", string.Empty };
        yield return new object[] { string.Empty, "this is a very long string" };
        yield return new object[] { string.Empty, string.Empty };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}