using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Xabbo.Utility
{
    public class Ini : IDictionary<string, IniSection>
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
            using var reader = new StreamReader(path);

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

                    string key = line[..index].Trim();
                    string value = line[(index + 1)..].Trim();

                    if (string.IsNullOrWhiteSpace(key))
                        throw new Exception($"Invalid entry on line {lineNumber}.");

                    if (currentSection is null)
                        throw new Exception($"Entry outside section on line {lineNumber}.");

                    currentSection.Add(new IniEntry(key, value));
                }
            }

            return ini;
        }
    }
}
