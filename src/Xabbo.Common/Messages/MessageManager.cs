using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace Xabbo.Messages;

/// <summary>
/// Manages messages between multiple clients using a mapping file.
/// </summary>
public sealed class MessageManager(string filePath) : IMessageManager
{
    const string MessagesFileUrl = "https://raw.githubusercontent.com/xabbo/messages/next/messages.ini";

    private readonly string _mapFilePath = filePath;
    private readonly SemaphoreSlim _init = new(1);
    private readonly ReaderWriterLockSlim _lock = new();

    private MessageMap? _messageMap;

    private readonly Dictionary<Identifier, HashSet<MessageNames>> _identifierNames = [];
    private readonly Dictionary<MessageNames, Header> _headers = [];
    private readonly Dictionary<Header, MessageNames> _headerNames = [];

    /// <summary>
    /// Whether to fetch the message map file from the xabbo/messages
    /// GitHub repo upon initialization if it does not exist locally.
    /// </summary>
    public bool Fetch { get; set; } = true;

    public bool Available { get; private set; }
    public event Action? Loaded;

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_init.Wait(0, CancellationToken.None))
            throw new InvalidOperationException("InitializeAsync may only be called once.");

        if (!File.Exists(_mapFilePath))
        {
            if (!Fetch)
                throw new FileNotFoundException($"Message map file not found: \"{_mapFilePath}\".");

            bool success = false;
            try
            {
                using HttpClient http = new();
                using Stream ins = await http.GetStreamAsync(MessagesFileUrl, cancellationToken);
                using Stream outs = File.OpenWrite(_mapFilePath);
                await ins.CopyToAsync(outs, cancellationToken).ConfigureAwait(false);
                success = true;
            }
            finally
            {
                if (!success)
                    File.Delete(_mapFilePath);
            }
        }

        _lock.EnterWriteLock();
        try { _messageMap = MessageMap.Load(_mapFilePath); }
        finally { _lock.ExitWriteLock(); }
    }

    public bool TryResolve(ReadOnlySpan<Identifier> identifiers,
        [NotNullWhen(true)] out Headers? headers,
        [NotNullWhen(false)] out Identifiers? unresolved)
    {
        if (identifiers.Length == 0)
            throw new ArgumentException("At least one identifier must be specified.", nameof(identifiers));

        headers = null;
        unresolved = null;

        foreach (var identifier in identifiers)
        {
            if (TryGetHeader(identifier, out Header header)) (headers ??= []).Add(header);
            else (unresolved ??= []).Add(identifier);
        }

        return unresolved is null;
    }

    public Headers Resolve(ReadOnlySpan<Identifier> identifiers)
    {
         if (TryResolve(identifiers, out Headers? headers, out Identifiers? unresolved))
            return headers;
         else
            throw new UnresolvedIdentifiersException(unresolved);
    }

    public Header Resolve(Identifier identifier)
    {
        if (!TryGetHeader(identifier, out Header header))
            throw new UnresolvedIdentifiersException([identifier]);
        return header;
    }

    public void Clear()
    {
        _lock.EnterWriteLock();
        try
        {
            Reset();
        }
        finally
        {
            Available = false;
            _lock.ExitWriteLock();
        }
    }

    private void Reset()
    {
        _headers.Clear();
        _headerNames.Clear();
        _identifierNames.Clear();
    }

    public void LoadMessages(IEnumerable<ClientMessage> messages)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_messageMap is null)
                throw new InvalidOperationException("Message map has not been loaded.");

            Reset();

            foreach (var (identifier, names) in _messageMap)
            {
                _identifierNames.Add(identifier, [names]);
                if (_identifierNames.TryGetValue(identifier with { Client = ClientType.None }, out var existing))
                    existing.Add(names);
                else
                    _identifierNames.Add(identifier with { Client = ClientType.None }, [names]);
            }

            foreach (var message in messages)
            {
                Identifier identifier = (message.Client, message.Direction, message.Name);
                Header header = new(message.Client, message.Direction, message.Header);
                if (!_identifierNames.TryGetValue(identifier, out var nameSet))
                {
                    nameSet = [new MessageNames(message.Direction).WithName(message.Client, message.Name)];
                    if (!_identifierNames.TryAdd(identifier, nameSet))
                        throw new Exception("Failed to add client identifiers.");
                }

                if (nameSet.Count > 1)
                    throw new Exception("Multiple message names conflict");

                var name = nameSet.Single();
                _headers[name] = header;
                _headerNames[header] = name;

                if (_identifierNames.TryGetValue(identifier with { Client = ClientType.None }, out var existing))
                    existing.Add(name);
                else
                    _identifierNames.Add(identifier with { Client = ClientType.None }, [name]);
            }
        }
        finally
        {
            Available = true;
            _lock.ExitWriteLock();

            Loaded?.Invoke();
        }
    }

    public bool TryGetHeader(Identifier identifier, out Header header)
    {
        if (BitOperations.PopCount((uint)identifier.Client) > 1)
            throw new ArgumentException($"Identifier may only specify a single client. ({identifier})", nameof(identifier));

        _lock.EnterReadLock();
        try
        {
            if (!_identifierNames.TryGetValue(identifier, out var sets))
            {
                header = Header.Unknown;
                return false;
            }
            if (sets.Count > 1)
                throw new AmbiguousIdentifierException(identifier, sets);
            return _headers.TryGetValue(sets.Single(), out header);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public bool TryGetNames(Identifier identifier, out MessageNames names)
    {
        if (BitOperations.PopCount((uint)identifier.Client) > 1)
            throw new ArgumentException($"Identifier may only specify a single client. ({identifier})", nameof(identifier));

        _lock.EnterReadLock();
        try
        {
            if (_identifierNames.TryGetValue(identifier, out var set))
            {
                if (set.Count > 1)
                    throw new AmbiguousIdentifierException(identifier, set);
                names = set.Single();
                return true;
            }
            else
            {
                names = default;
                return false;
            }
        }
        finally { _lock.ExitReadLock(); }
    }

    public bool TryGetNames(Header header, out MessageNames identifiers)
    {
        _lock.EnterReadLock();
        try { return _headerNames.TryGetValue(header, out identifiers); }
        finally { _lock.ExitReadLock(); }
    }

    public bool Is(Header header, ReadOnlySpan<Identifier> identifiers)
    {
        foreach (var identifier in identifiers)
        {
            if (TryGetHeader(identifier, out Header h) && h.Equals(header))
                return true;
        }
        return false;
    }
}
