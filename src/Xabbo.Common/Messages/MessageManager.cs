using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Xabbo.Messages;

/// <summary>
/// Manages messages between multiple clients using a mapping file.
/// </summary>
public sealed class MessageManager(string? filePath = null, ILoggerFactory? loggerFactory = null) : IMessageManager
{
    const string MessagesFileUrl = "https://raw.githubusercontent.com/xabbo/messages/next/messages.ini";

    private readonly string _mapFilePath = filePath ??
        Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "xabbo", "messages.ini");

    private readonly ILogger Log = (ILogger?)loggerFactory?.CreateLogger<MessageManager>() ?? NullLogger.Instance;

    private readonly SemaphoreSlim _init = new(1);
    private readonly ReaderWriterLockSlim _lock = new();

    private MessageMap? _messageMap;

    private readonly Dictionary<Identifier, HashSet<MessageNames>> _identifierNames = [];
    private readonly Dictionary<(Direction Direction, MessageNames Names), Header> _headers = [];
    private readonly Dictionary<Header, MessageNames> _headerNames = [];

    /// <summary>
    /// Whether to fetch the message map file from the xabbo/messages
    /// GitHub repo upon initialization if it does not exist locally.
    /// </summary>
    public bool Fetch { get; set; } = true;

    /// <summary>
    /// The maximum age of the message map file after which it is invalidated.
    /// </summary>
    public TimeSpan MaxAge { get; set; } = TimeSpan.FromDays(1);

    public bool Available { get; private set; }
    public event Action? Loaded;

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (!_init.Wait(0, CancellationToken.None))
            throw new InvalidOperationException("InitializeAsync may only be called once.");

        Log.LogInformation("Initializing message manager...");
        Log.LogDebug("Map file path is '{MapFilePath}'.", _mapFilePath);

        bool fetchMapFile = false;
        FileInfo mapFileInfo = new(_mapFilePath);

        if (!mapFileInfo.Exists)
        {
            if (!Fetch)
            {
                Log.LogError("Map file does not exist and Fetch is false.");
                throw new FileNotFoundException($"Message map file not found: '{_mapFilePath}'.");
            }
            Log.LogDebug("Map file does not exist.");
            fetchMapFile = true;
        }
        else if ((DateTime.Now - mapFileInfo.LastWriteTime) >= MaxAge)
        {
            Log.LogDebug("Map file has reached max age {MaxAge}.", MaxAge);
            fetchMapFile = true;
        }
        else if (mapFileInfo.Length == 0)
        {
            Log.LogDebug("Map file is empty.");
            fetchMapFile = true;
        }

        if (fetchMapFile)
        {
            try
            {
                mapFileInfo.Directory?.Create();

                using HttpClient http = new();
                Log.LogInformation("Fetching message map file from '{MapFileUrl}'...", MessagesFileUrl);
                using Stream ins = await http.GetStreamAsync(MessagesFileUrl, cancellationToken);
                using Stream outs = File.OpenWrite(_mapFilePath);
                await ins.CopyToAsync(outs, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, "Failed to fetch message map file: {Error}.", ex.Message);
                try
                {
                    File.Delete(_mapFilePath);
                }
                catch (Exception ex2) { Log.LogError("Failed to remove message map file: {Error}.", ex2.Message); }
                throw;
            }
        }
        else
        {
            Log.LogDebug("Loading map file from disk.");
        }

        _lock.EnterWriteLock();
        try
        {
            _messageMap = MessageMap.Load(_mapFilePath);
            Log.LogInformation("Loaded {Count} identifiers from message map.", _messageMap.Count);
        }
        catch (Exception ex)
        {
            Log.LogError(ex, "Failed to load message map: {Error}.", ex.Message);
            throw;
        }
        finally
        {
            _lock.ExitWriteLock();
        }
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

        Log.LogDebug("Message map reset.");
    }

    public void LoadMessages(IEnumerable<ClientMessage> messages)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_messageMap is null)
                throw new InvalidOperationException("Message map has not been loaded.");

            Reset();

            // First populate the Identifier -> HashSet<MessageNames> mapping from the message map file entries.
            foreach (var (identifier, names) in _messageMap)
            {
                // Each identifier will have a single client per the message map file specification.
                _identifierNames.Add(identifier, [names]);
                // Also add a key with a non-client-targeted identifier.
                if (!_identifierNames.TryGetValue(identifier with { Client = ClientType.None }, out var existingNameSet))
                {
                    // Add the current MessageNames instance if it does not already exist.
                    _identifierNames.Add(identifier with { Client = ClientType.None }, [names]);
                }
                else
                {
                    // Otherwise add the current MessageNames instance to the existing entry.
                    existingNameSet.Add(names);
                }
            }

            // Iterate over the provided ClientMessage information.
            foreach (var message in messages)
            {
                // Create an Identifier and Header for this ClientMessage.
                Identifier identifier = (message.Client, message.Direction, message.Name);
                Header header = new(message.Client, message.Direction, message.Header);

                // Create a new HashSet<MessageNames> if it does not exist for this identifier.
                if (!_identifierNames.TryGetValue(identifier, out var nameSet))
                {
                    nameSet = [new MessageNames().WithName(message.Client, message.Name)];
                    _identifierNames[identifier] = nameSet;
                }

                if (nameSet.Count > 1)
                    throw new Exception("Multiple message names conflict.");

                // Create a two-way mapping between Header and Direction/MessageNames.
                var names = nameSet.Single();
                _headers[(message.Direction, names)] = header;
                _headerNames[header] = names;

                // Also add or merge this entry into the Identifer -> HashSet<MessageNames> map
                // with a non-client-targeted identifier.
                if (_identifierNames.TryGetValue(identifier with { Client = ClientType.None }, out var existingNameSet))
                {
                    existingNameSet.Add(names);
                }
                else
                {
                    _identifierNames.Add(identifier with { Client = ClientType.None }, [names]);
                }
            }
        }
        catch (Exception ex)
        {
            Log.LogError(ex, "Failed to load client messages: {Error}.", ex.Message);
            throw;
        }
        finally
        {
            _lock.ExitWriteLock();
        }

        Log.LogInformation("Loaded {Count} message headers.", _headers.Count);
        Available = true;
        Loaded?.Invoke();
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
            return _headers.TryGetValue((identifier.Direction, sets.Single()), out header);
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
