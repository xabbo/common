using System.Collections;
using System.Collections.Generic;

namespace Xabbo.Common.Tests.Data;

public class Directions : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { Direction.In };
        yield return new object[] { Direction.Out };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}