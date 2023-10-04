using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Xabbo.Serialization;

namespace Xabbo.Messages;

/// <summary>
/// Manages messages of the Unity and Flash clients using a message mapping file.
/// </summary>
public sealed class UnifiedMessageManager : IMessageManager
{
    private readonly string _mapFilePath;
    private readonly List<MessageInfo> _messageInfos = new();

    private readonly Dictionary<(ClientType, short), MessageInfo>
        _incomingHeaderMap = new(),
        _outgoingHeaderMap = new();

    private readonly Dictionary<string, MessageInfo>
        _incomingNameMap = new(StringComparer.OrdinalIgnoreCase),
        _outgoingNameMap = new(StringComparer.OrdinalIgnoreCase);

    private bool _initialized;
    private MessageMap? _messageMap;

    /// <summary>
    /// Whether to fetch the message map file from the Xabbo.Messages GitHub repo
    /// upon initialization if it does not exist locally.
    /// </summary>
    public bool AutoFetch { get; set; } = true;

    public Incoming In { get; private set; }
    public Outgoing Out { get; private set; }

    /// <summary>
    /// Gets a header by its identifier.
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public Header this[Identifier identifier] => GetHeaders(identifier.Direction)[identifier.Name];

    /// <summary>
    /// Constructs a new <see cref="UnifiedMessageManager"/> using the specified configuration.
    /// </summary>
    public UnifiedMessageManager(IConfiguration config)
        : this(config.GetValue("Xabbo:Messages:MapFilePath", "messages.ini"))
    { }

    /// <summary>
    /// Constructs a new <see cref="UnifiedMessageManager"/> using the message map file path.
    /// </summary>
    public UnifiedMessageManager(string filePath)
    {
        _mapFilePath = filePath;

        In = new Incoming();
        Out = new Outgoing();
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_initialized) return;

        if (!File.Exists(_mapFilePath))
        {
            if (!AutoFetch)
            {
                throw new FileNotFoundException($"Message map file not found: '{_mapFilePath}'.");
            }

            using HttpClient http = new();
            using Stream ins = await http.GetStreamAsync("https://raw.githubusercontent.com/b7c/Xabbo.Messages/master/messages.ini", cancellationToken);
            using Stream outs = File.OpenWrite(_mapFilePath);
            await ins.CopyToAsync(outs, cancellationToken);
        }

        _messageMap = MessageMap.Load(_mapFilePath);

        _initialized = true;
    }

    private Headers GetHeaders(Direction direction)
    {
        return direction switch
        {
            Direction.Incoming => In,
            Direction.Outgoing => Out,
            _ => throw new Exception("Invalid direction.")
        };
    }

    private Dictionary<(ClientType, short), MessageInfo> GetHeaderMap(Direction direction)
    {
        return direction switch
        {
            Direction.Incoming => _incomingHeaderMap,
            Direction.Outgoing => _outgoingHeaderMap,
            _ => throw new Exception("Invalid direction.")
        };
    }

    private Dictionary<string, MessageInfo> GetNameMap(Direction direction)
    {
        return direction switch
        {
            Direction.Incoming => _incomingNameMap,
            Direction.Outgoing => _outgoingNameMap,
            _ => throw new Exception("Invalid direction")
        };
    }

    private void Reset()
    {
        _messageInfos.Clear();
        _incomingHeaderMap.Clear();
        _outgoingHeaderMap.Clear();
        _incomingNameMap.Clear();
        _outgoingNameMap.Clear();

        InitializeMessages(Direction.Incoming);
        InitializeMessages(Direction.Outgoing);
    }

    /// <summary>
    /// Initializes the messages from the message map
    /// </summary>
    private void InitializeMessages(Direction direction)
    {
        if (_messageMap is null)
        {
            throw new InvalidOperationException("Message map has not been initialized.");
        }

        List<MessageMapItem>? list = direction switch
        {
            Direction.Incoming => _messageMap.Incoming,
            Direction.Outgoing => _messageMap.Outgoing,
            _ => null
        };

        if (list is null) return;

        foreach (MessageMapItem item in list)
        {
            MessageInfo messageInfo = new MessageInfo
            {
                Direction = direction,
                UnityHeader = item.Header,
                UnityName = item.UnityName,
                FlashName = item.FlashName
            };

            GetHeaderMap(direction).Add((ClientType.Unity, item.Header), messageInfo);
            GetNameMap(direction).Add(item.UnityName, messageInfo);
            if (!string.IsNullOrWhiteSpace(item.FlashName) &&
                item.FlashName != item.UnityName)
            {
                GetNameMap(direction).Add(item.FlashName, messageInfo);
            }

            _messageInfos.Add(messageInfo);
        }
    }

    private void Merge(IClientMessageInfo info)
    {
        var nameMap = info.Direction == Direction.Outgoing ? _outgoingNameMap : _incomingNameMap;
        var headerMap = info.Direction == Direction.Outgoing ? _outgoingHeaderMap : _incomingHeaderMap;

        if (!nameMap.TryGetValue(info.Name, out MessageInfo? messageInfo))
        {
            messageInfo = new MessageInfo { Direction = info.Direction };
            nameMap[info.Name] = messageInfo;
            _messageInfos.Add(messageInfo);
        }

        if (info.Client == ClientType.Flash)
        {
            messageInfo.FlashHeader = info.Header;
            messageInfo.FlashName = info.Name;
        }
        else if (info.Client == ClientType.Unity)
        {
            messageInfo.UnityHeader = info.Header;
            messageInfo.UnityName = info.Name;
        }

        headerMap[(info.Client, info.Header)] = messageInfo;
    }


    public void LoadMessages(IEnumerable<IClientMessageInfo> messages)
    {
        Reset();

        foreach (IClientMessageInfo message in messages)
        {
            Merge(message);
        }

        In.Load(_messageInfos.Where(x => x.IsIncoming));
        Out.Load(_messageInfos.Where(x => x.IsOutgoing));
    }

    public bool IdentifierExists(Identifier identifier)
    {
        return GetHeaders(identifier.Direction).TryGetHeader(identifier.Name, out _);
    }

    public bool TryGetHeader(Identifier identifier, [NotNullWhen(true)] out Header? header)
    {
        return TryGetHeaderByName(identifier.Direction, identifier.Name, out header);
    }

    public bool TryGetHeaderByValue(Direction direction, ClientType clientType, short value, [NotNullWhen(true)] out Header? header)
    {
        return GetHeaders(direction).TryGetHeader(clientType, value, out header);
    }

    public bool TryGetHeaderByName(Direction direction, string name, [NotNullWhen(true)] out Header? header)
    {
        return GetHeaders(direction).TryGetHeader(name, out header);
    }

    public bool TryGetInfoByHeader(Direction direction, ClientType clientType, short header, [NotNullWhen(true)] out MessageInfo? info)
    {
        return GetHeaderMap(direction).TryGetValue((clientType, header), out info);
    }

    public bool TryGetInfoByName(Direction direction, string name, [NotNullWhen(true)] out MessageInfo? info)
    {
        return GetNameMap(direction).TryGetValue(name, out info);
    }
}
