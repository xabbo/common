using System;
using System.Collections.Generic;
using System.IO;
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

    private readonly Dictionary<Identifier, MessageNames> _identifierNames = [];
    private readonly Dictionary<MessageNames, Header> _headers = [];
    private readonly Dictionary<Header, MessageNames> _headerNames = [];

    /// <summary>
    /// Whether to fetch the message map file from the xabbo/messages
    /// GitHub repo upon initialization if it does not exist locally.
    /// </summary>
    public bool Fetch { get; set; } = true;

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

    public Header Resolve(Identifier identifier)
    {
        if (!TryGetHeader(identifier, out Header header))
            throw new UnresolvedIdentifiersException([identifier]);
        return header;
    }

    public Header[] Resolve(ReadOnlySpan<Identifier> identifiers)
    {
        if (identifiers.Length == 0)
            throw new ArgumentException("Length of identifiers cannot be zero.", nameof(identifiers));

        var headers = new HashSet<Header>();
        var unresolved = new Identifiers();
        foreach (var identifier in identifiers)
        {
            if (TryGetHeader(identifier, out Header header))
                headers.Add(header);
            else
                unresolved.Add(identifier);
        }
        if (unresolved.Count > 0)
            throw new UnresolvedIdentifiersException(unresolved);
        return [.. headers];
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

            foreach (var (identifier, identifiers) in _messageMap)
                _identifierNames.Add(identifier, identifiers);

            foreach (var message in messages)
            {
                Identifier identifier = (message.Client, message.Direction, message.Name);
                Header header = new(message.Client, message.Direction, message.Header);
                if (!_identifierNames.TryGetValue(identifier, out MessageNames identifiers))
                {
                    identifiers = new MessageNames(message.Direction).WithName(message.Client, message.Name);
                    if (!_identifierNames.TryAdd(identifier, identifiers))
                        throw new Exception("Failed to add client identifiers.");
                }
                _headers[identifiers] = header;
                _headerNames[header] = identifiers;
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool TryGetHeader(Identifier identifier, out Header header)
    {
        if (BitOperations.PopCount((uint)identifier.Client) > 1)
            throw new ArgumentException($"Identifier may only specify a single client. ({identifier.Client})", nameof(identifier));

        _lock.EnterReadLock();
        try
        {
            if (!_identifierNames.TryGetValue(identifier, out MessageNames key))
            {
                header = Header.Unknown;
                return false;
            }
            return _headers.TryGetValue(key, out header);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public bool TryGetNames(Identifier identifier, out MessageNames identifiers)
    {
        _lock.EnterReadLock();
        try { return _identifierNames.TryGetValue(identifier, out identifiers); }
        finally { _lock.ExitReadLock(); }
    }

    public bool TryGetNames(Header header, out MessageNames identifiers)
    {
        _lock.EnterReadLock();
        try { return _headerNames.TryGetValue(header, out identifiers); }
        finally { _lock.ExitReadLock(); }
    }
}
