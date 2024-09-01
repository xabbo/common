using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xabbo.Common.Tests.Data;

public class Matrix<T1, T2> : IEnumerable<object[]>
    where T1 : IEnumerable<object[]>, new()
    where T2 : IEnumerable<object[]>, new()
{
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (object[] a1 in new T1())
        {
            foreach (object[] a2 in new T2())
            {
                yield return a1.Concat(a2).ToArray();
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class Matrix<T1, T2, T3> : IEnumerable<object[]>
    where T1 : IEnumerable<object[]>, new()
    where T2 : IEnumerable<object[]>, new()
    where T3 : IEnumerable<object[]>, new()
{
    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (object[] a1 in new T1())
        {
            foreach (object[] a2 in new T2())
            {
                foreach (object[] a3 in new T3())
                {
                    yield return a1.Concat(a2).Concat(a3).ToArray();
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}