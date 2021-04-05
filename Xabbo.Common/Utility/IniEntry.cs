using System;

namespace Xabbo.Utility
{
    public class IniEntry
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
