using System.Collections;

namespace Xabbo.Common.Generator.Tests;

public class PacketPrimitives : IEnumerable<object[]>
{
    private static readonly Constant[] TypeConstants = [
        new("bool", "true", true),
        new("byte", "123", true),
        new("short", "123", true),
        new("int", "123", true),
        new("long", "123", true),
        new("string", "\"hello\"", true),
        new("Id", "123", true),
        new("Length", "123", true),
    ];

    public class TypeNames : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var constant in TypeConstants)
                yield return [ constant.Type ];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TypeValues : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var constant in TypeConstants)
                yield return [ constant.Value ];
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var constant in TypeConstants)
            yield return [ constant ];
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
