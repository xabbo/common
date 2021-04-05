using System;
using System.Collections.Generic;
using System.Threading;

namespace Xabbo.Messages
{
    public abstract class HeaderDictionary
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private readonly Type _type;

        private readonly Dictionary<short, Header> _valueMap = new Dictionary<short, Header>();
        private readonly Dictionary<string, Header> _nameMap = new Dictionary<string, Header>(StringComparer.OrdinalIgnoreCase);

        public Destination Destination { get; }

        public HeaderDictionary(Destination destination)
        {
            Destination = destination;

            _type = GetType();
        }

        public HeaderDictionary(Destination destination, IReadOnlyDictionary<string, short> values)
            : this(destination)
        {
            ResetProperties();
            Load(values);
        }

        private void ResetProperties()
        {
            var props = _type.GetProperties();
            foreach (var prop in props)
            {
                if (prop.PropertyType.Equals(typeof(Header)) &&
                    prop.GetMethod?.GetParameters().Length == 0)
                {
                    var header = new Header(Destination, -1, prop.Name);
                    _nameMap[prop.Name] = header;
                    prop.SetValue(this, header);
                }
            }
        }

        public void Load(IReadOnlyDictionary<string, short> values)
        {
            _lock.EnterWriteLock();
            try
            {
                _valueMap.Clear();
                _nameMap.Clear();

                ResetProperties();

                foreach (var pair in values)
                {
                    var header = new Header(Destination, pair.Value, pair.Key);

                    _nameMap[pair.Key] = header;
                    if (pair.Value >= 0)
                        _valueMap[pair.Value] = header;

                    var prop = _type.GetProperty(pair.Key, typeof(Header));
                    if (prop != null)
                        prop.SetValue(this, new Header(Destination, pair.Value, prop.Name));
                }
            }
            finally { _lock.ExitWriteLock(); }
        }

        public bool MessageExists(string name)
        {
            _lock.EnterReadLock();
            try { return _nameMap.ContainsKey(name); }
            finally { _lock.ExitReadLock(); }
        }

        public bool TryGetIdentifier(short value, out string? name)
        {
            _lock.EnterReadLock();
            try
            {
                name = null;
                if (!_valueMap.TryGetValue(value, out Header? header))
                    return false;

                name = header.Name;
                return true;
            }
            finally { _lock.ExitReadLock(); }
        }

        public bool TryGetHeader(short value, out Header? header)
        {
            _lock.EnterReadLock();
            try { return _valueMap.TryGetValue(value, out header); }
            finally { _lock.ExitReadLock(); }
        }

        public bool TryGetHeader(string name, out Header? header)
        {
            _lock.EnterReadLock();
            try { return _nameMap.TryGetValue(name, out header); }
            finally { _lock.ExitReadLock(); }
        }

        public Header this[string name]
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    if (_nameMap.TryGetValue(name, out Header? header))
                        return header;
                    else
                        return new Header(Destination, -1, name);
                }
                finally { _lock.ExitReadLock(); }
            }
        }
    }
}
