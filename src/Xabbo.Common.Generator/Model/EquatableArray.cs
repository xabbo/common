using System.Collections;

namespace Xabbo.Common.Generator.Model;

internal readonly struct EquatableArray<T>(T[] array) : IEquatable<EquatableArray<T>>, IEnumerable<T>
    where T : IEquatable<T>
{
    public static readonly EquatableArray<T> Empty = new([]);

    private readonly T[] _array = array;

    public T this[int index] => _array[index];
    public int Length => _array.Length;

    public bool Equals(EquatableArray<T> array) => AsSpan().SequenceEqual(array.AsSpan());
    public override bool Equals(object? obj) => obj is EquatableArray<T> array && Equals(this, array);

    public override int GetHashCode()
    {
        int hashCode = 0;
        foreach (T item in _array)
            hashCode = (hashCode, item).GetHashCode();
        return hashCode;
    }

    public ReadOnlySpan<T> AsSpan() => _array.AsSpan();

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)(_array ?? [])).GetEnumerator();
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => ((IEnumerable<T>)(_array ?? [])).GetEnumerator();

    public static bool operator ==(EquatableArray<T> left, EquatableArray<T> right) => left.Equals(right);
    public static bool operator !=(EquatableArray<T> left, EquatableArray<T> right) => !left.Equals(right);

    public static implicit operator EquatableArray<T>(T[] array) => new(array);
}

internal static class ArrayExtensions
{
    public static EquatableArray<T> ToEquatableArray<T>(this T[] array) where T : IEquatable<T> => new(array);
    public static EquatableArray<T> ToEquatableArray<T>(this IEnumerable<T> enumerable) where T : IEquatable<T> => new(enumerable.ToArray());
}