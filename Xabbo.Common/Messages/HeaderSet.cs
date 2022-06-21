using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Xabbo.Messages;

/// <summary>
/// Specifies a set of headers.
/// </summary>
public class HeaderSet : ICollection<Header>
{
    private readonly HashSet<Header> _headers;

    public HeaderSet()
    {
        _headers = new HashSet<Header>();
    }

    public HeaderSet(IEnumerable<Header> headers)
    {
        _headers = new HashSet<Header>(headers);
    }

    public int Count => _headers.Count;
    public bool IsReadOnly => false;

    public void Add(Header item) => _headers.Add(item);

    public void Clear() => _headers.Clear();

    public bool Contains(Header item) => _headers.Contains(item);

    public void CopyTo(Header[] array, int arrayIndex) => _headers.CopyTo(array, arrayIndex);

    public IEnumerator<Header> GetEnumerator() => _headers.GetEnumerator();

    public bool Remove(Header item) => _headers.Remove(item);

    IEnumerator IEnumerable.GetEnumerator() => _headers.GetEnumerator();

    public static implicit operator HeaderSet(Header header) => new HeaderSet() { header };

    public static HeaderSet FromTuple(ITuple tuple)
    {
        HeaderSet set = new();
        for (int i = 0; i < tuple.Length; i++)
        {
            if (tuple[i] is not Header header)
                throw new Exception($"Tuple values must be of type {typeof(Header).FullName}.");
            set.Add(header);
        }
        return set;
    }
}
