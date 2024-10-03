using System.Collections;

namespace Xabbo.Common.Generator.Tests;

public class BooleanData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [ false ];
        yield return [ true ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
