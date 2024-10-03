using System.Collections;

namespace Xabbo.Common.Generator.Tests;

public class ObjectConstantData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [ new Constant("object", "new object()") ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}