using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Xabbo.Common.Generators
{
    internal class Ini : IDictionary<string, IniSection>
    {
        private readonly IDictionary<string, IniSection> dict = new Dictionary<string, IniSection>();

        public ICollection<string> Keys => dict.Keys;
        public ICollection<IniSection> Values => dict.Values;
        public int Count => dict.Count;
        public bool IsReadOnly => false;

        public IniSection this[string key]
        {
            get => dict[key];
            set => dict[key] = value;
        }

        public bool ContainsKey(string key) => dict.ContainsKey(key);

        public void Add(string key, IniSection value) => dict.Add(key, value);

        public bool Remove(string key) => dict.Remove(key);

        public bool TryGetValue(string key, out IniSection value) => dict.TryGetValue(key, out value);

        public void Add(KeyValuePair<string, IniSection> item) => dict.Add(item);

        public void Clear() => dict.Clear();

        public bool Contains(KeyValuePair<string, IniSection> item) => dict.Contains(item);

        public void CopyTo(KeyValuePair<string, IniSection>[] array, int arrayIndex) => dict.CopyTo(array, arrayIndex);

        public bool Remove(KeyValuePair<string, IniSection> item) => dict.Remove(item);

        public IEnumerator<KeyValuePair<string, IniSection>> GetEnumerator() => dict.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Ini() { }

        public static Ini Load(string path)
        {
            using StreamReader reader = new(path);
            return Load(reader);
        }

        public static Ini Load(TextReader reader)
        {
            var ini = new Ini();
            IniSection? currentSection = null;

            string? line; int lineNumber = 0;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;

                if (string.IsNullOrWhiteSpace(line)) continue;

                line = line.Trim();
                if (line.StartsWith("#")) continue;
                if (line.StartsWith(";")) continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    string sectionName = line.Substring(1, line.Length - 2);
                    if (string.IsNullOrWhiteSpace(sectionName))
                        throw new Exception($"Section name cannot be empty on line {lineNumber}.");
                    if (ini.ContainsKey(sectionName))
                        throw new Exception($"Duplicate section name on line {lineNumber}.");
                    currentSection = new IniSection();
                    ini.Add(sectionName, currentSection);
                }
                else
                {
                    int index = line.IndexOf('=');
                    string? key = null, value = null;
                    if (index >= 0)
                    {
                        key = line.Substring(0, index).Trim();
                        value = line.Substring(index + 1).Trim();

                        if (string.IsNullOrWhiteSpace(key))
                            throw new Exception($"Invalid entry on line {lineNumber}.");

                        currentSection?.Add(new IniEntry(key, value ?? string.Empty));
                    }
                    else
                    {
                        throw new Exception($"Invalid entry on line {lineNumber}.");
                    }
                }
            }

            return ini;
        }
    }

    internal class IniSection : IList<IniEntry>
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

    internal class IniEntry
    {
        public string Key { get; }
        public string Value { get; set; }

        public IniEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public IniEntry(string key)
            : this(key, string.Empty)
        { }

        public void Deconstruct(out string key, out string value)
        {
            key = Key;
            value = Value;
        }

        public override int GetHashCode() => (Key, Value).GetHashCode();

        public override bool Equals(object? obj)
        {
            return
                obj is IniEntry other &&
                other.Key == Key &&
                other.Value == Value;
        }
    }
}
