using System.Collections;

namespace Xabbo.Common.Generator.Tests;

public class HeaderIdentifierData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [ new Constant("Header", "(Direction.Out, 0)") ];
        yield return [ new Constant("Identifier", "(ClientType.Flash, Direction.Out, \"Identifier\")") ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
