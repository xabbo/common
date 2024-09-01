using System.Collections;
using System.Collections.Generic;

namespace Xabbo.Common.Tests.Data;

public class Clients : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { ClientType.Unity };
        yield return new object[] { ClientType.Flash };
        yield return new object[] { ClientType.Shockwave };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}