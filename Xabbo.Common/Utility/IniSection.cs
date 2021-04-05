using System;
using System.Collections;
using System.Collections.Generic;

namespace Xabbo.Utility
{
    public class IniSection : IList<IniEntry>
    {
        private readonly List<IniEntry> list = new List<IniEntry>();

        public IniSection() { }

        public IniEntry this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(IniEntry item) => list.Add(item);

        public void Clear() => list.Clear();

        public bool Contains(IniEntry item) => list.Contains(item);

        public void CopyTo(IniEntry[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);

        public IEnumerator<IniEntry> GetEnumerator() => list.GetEnumerator();

        public int IndexOf(IniEntry item) => list.IndexOf(item);

        public void Insert(int index, IniEntry item) => list.Insert(index, item);

        public bool Remove(IniEntry item) => list.Remove(item);

        public void RemoveAt(int index) => list.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}
