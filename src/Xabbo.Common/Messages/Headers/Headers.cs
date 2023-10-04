using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Xabbo.Messages;

/// <summary>
/// Represents a dictionary that stores <see cref="Header"/>s.
/// Any children of this class may expose <see cref="Header"/> properties
/// which will internally map to the dictionary by their name.
/// </summary>
public abstract class Headers
{
    private readonly ReaderWriterLockSlim _lock = new();

    private readonly Dictionary<(ClientType, short), Header> _headerMap = new();
    private readonly Dictionary<string, Header> _nameMap = new(StringComparer.OrdinalIgnoreCase);

    protected Header Get(string name)
    {
        _lock.EnterReadLock();
        try
        {
            if (!_nameMap.TryGetValue(name, out Header? header))
                return Header.Unknown;
            return header;
        }
        finally { _lock.ExitReadLock(); }
    }

    /// <summary>
    /// The direction of the headers stored in this dictionary.
    /// </summary>
    public Direction Direction { get; }

    /// <summary>
    /// Constructs a new header dictionary with the specified destination.
    /// </summary>
    public Headers(Direction direction)
    {
        Direction = direction;
    }

    /// <summary>
    /// Resets this dictionary and initializes it from the specified message information.
    /// </summary>
    public void Load(IEnumerable<IMessageInfo> messages)
    {
        _lock.EnterWriteLock();
        try
        {
            _headerMap.Clear();
            _nameMap.Clear();

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
                        Direction = info.Direction,
                        Value = info.FlashHeader,
                        Name = info.FlashName ?? string.Empty
                    };
                }

                if (info.UnityHeader >= 0)
                {
                    unityHeader = new ClientHeader
                    {
                        Client = ClientType.Unity,
                        Direction = info.Direction,
                        Value = info.UnityHeader,
                        Name = info.UnityName ?? string.Empty
                    };
                }

                header = new Header(info.Direction, unityHeader, flashHeader);

                if (flashHeader is not null)
                {
                    _headerMap[(flashHeader.Client, flashHeader.Value)] = header;
                    _nameMap[flashHeader.Name] = header;
                }

                if (unityHeader is not null)
                {
                    _headerMap[(unityHeader.Client, unityHeader.Value)] = header;
                    _nameMap[unityHeader.Name] = header;
                }
            }
        }
        finally { _lock.ExitWriteLock(); }
    }

    /// <summary>
    /// Gets if a message with the specified name exists in this dictionary.
    /// </summary>
    public bool MessageExists(string name)
    {
        _lock.EnterReadLock();
        try { return _nameMap.ContainsKey(name); }
        finally { _lock.ExitReadLock(); }
    }

    /// <summary>
    /// Attempts to get the name of the message with the specified client type and header value.
    /// </summary>
    /// <returns><see langword="true"/> if the name of the message was found.</returns>
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

    /// <summary>
    /// Attempts to get the <see cref="Header"/> with the specified client type and value.
    /// </summary>
    /// <returns><see langword="true"/> if the header was found.</returns>
    public bool TryGetHeader(ClientType clientType, short value, out Header? header)
    {
        _lock.EnterReadLock();
        try { return _headerMap.TryGetValue((clientType, value), out header); }
        finally { _lock.ExitReadLock(); }
    }

    /// <summary>
    /// Attempts to get the <see cref="Header"/> with the specified name.
    /// </summary>
    /// <returns><see langword="true"/> if the header was found.</returns>
    public bool TryGetHeader(string name, out Header? header)
    {
        _lock.EnterReadLock();
        try { return _nameMap.TryGetValue(name, out header); }
        finally { _lock.ExitReadLock(); }
    }

    /// <summary>
    /// Gets the <see cref="Header"/> with the specified name, or throws if it does not exist.
    /// </summary>
    /// <exception cref="UnknownHeaderException">If a header with the specified name does not exist.</exception>
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
                    throw new UnknownHeaderException(new Identifier(Direction, name));
                }
            }
            finally { _lock.ExitReadLock(); }
        }
    }
}
