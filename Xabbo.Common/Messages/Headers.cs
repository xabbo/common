using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

using Xabbo.Common;

namespace Xabbo.Messages
{
    public abstract class Headers
    {
        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private readonly Type _selfType;

        private readonly Dictionary<(ClientType, short), Header> _headerMap = new();
        private readonly Dictionary<string, Header> _nameMap = new(StringComparer.OrdinalIgnoreCase);

        public Destination Destination { get; }

        public Headers(Destination destination)
        {
            _selfType = GetType();

            Destination = destination;

            ResetProperties();
        }

        private void ResetProperties()
        {
            var props = _selfType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.PropertyType.Equals(typeof(Header)) &&
                    prop.GetMethod?.GetParameters().Length == 0)
                {
                    Header header = new(Destination, name: prop.Name); 
                    _nameMap[prop.Name] = header;
                    prop.SetValue(this, header);
                }
            }
        }

        public void Load(IEnumerable<IMessageInfo> messages)
        {
            _lock.EnterWriteLock();
            try
            {
                _headerMap.Clear();
                _nameMap.Clear();

                ResetProperties();

                foreach (IMessageInfo info in messages)
                {
                    Header? header;
                    ClientHeader?
                        flashHeader = null,
                        unityHeader = null;

                    if (info.FlashHeader >= 0)
                    {
                        flashHeader = new ClientHeader
                        {
                            Client = ClientType.Flash,
                            Destination = info.Destination,
                            Value = info.FlashHeader,
                            Name = info.FlashName ?? string.Empty
                        };
                    }

                    if (info.UnityHeader >= 0)
                    {
                        unityHeader = new ClientHeader
                        {
                            Client = ClientType.Unity,
                            Destination = info.Destination,
                            Value = info.UnityHeader,
                            Name = info.UnityName ?? string.Empty
                        };
                    }

                    header = new Header(info.Destination, unityHeader, flashHeader);
                    /*{
                        Destination = info.Destination,
                        Name = info.UnityName ?? info.FlashName ?? "unknown",
                        Flash = flashHeader,
                        Unity = unityHeader
                    };*/

                    if (flashHeader is not null)
                    {
                        _headerMap[(flashHeader.Client, flashHeader.Value)] = header;
                        _nameMap[flashHeader.Name] = header;
                    }

                    if (unityHeader is not null)
                    {
                        _headerMap[(unityHeader.Client, unityHeader.Value)] = header;
                        _nameMap[unityHeader.Name] = header;

                        PropertyInfo? prop = _selfType.GetProperty(unityHeader.Name, typeof(Header));
                        if (prop is not null)
                            prop.SetValue(this, header);
                    }
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

        public bool TryGetName(ClientType clientType, short value, out string? name)
        {
            _lock.EnterReadLock();
            try
            {
                name = null;
                if (!_headerMap.TryGetValue((clientType, value), out Header? header))
                    return false;

                name = clientType switch
                {
                    ClientType.Flash => header.Flash?.Name ?? header.Unity?.Name ?? string.Empty,
                    ClientType.Unity => header.Unity?.Name ?? header.Flash?.Name ?? string.Empty,
                    _ => string.Empty
                };
                return true;
            }
            finally { _lock.ExitReadLock(); }
        }

        public bool TryGetHeader(ClientType clientType, short value, out Header? header)
        {
            _lock.EnterReadLock();
            try { return _headerMap.TryGetValue((clientType, value), out header); }
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
                    {
                        return header;
                    }
                    else
                    {
                        throw new Exception($"Unknown header: \"{name}\".");
                    }
                }
                finally { _lock.ExitReadLock(); }
            }
        }
    }
}
