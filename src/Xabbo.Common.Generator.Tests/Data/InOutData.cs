using System.Collections;

namespace Xabbo.Common.Generator.Tests;

public class InOutData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [Direction.In];
        yield return [Direction.Out];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}