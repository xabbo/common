using System.Collections;

namespace Xabbo.Common.Generator.Tests;

public class ConcatData<T1, T2> : IEnumerable<object[]>
    where T1 : IEnumerable<object[]>, new()
    where T2 : IEnumerable<object[]>, new()
{
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (object[] array in new T1())
            yield return array;

        foreach (object[] array in new T2())
            yield return array;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
