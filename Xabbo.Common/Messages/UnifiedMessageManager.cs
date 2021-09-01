using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Xabbo.Serialization;

namespace Xabbo.Messages
{
    /// <summary>
    /// Manages messages of the Unity and Flash clients using a message mapping file.
    /// </summary>
    public class UnifiedMessageManager : IMessageManager
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

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

        public Incoming In { get; private set; }
        public Outgoing Out { get; private set; }

        public Header this[Identifier identifier] => GetHeaders(identifier.Destination)[identifier.Name];

        public UnifiedMessageManager(IConfiguration config)
            : this(config.GetValue("Xabbo:Messages:MapFilePath", "messages.ini"))
        { }

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
                using HttpClient http = new();
                using Stream ins = await http.GetStreamAsync("https://raw.githubusercontent.com/b7c/Xabbo.Messages/master/messages.ini", cancellationToken);
                using Stream outs = File.OpenWrite(_mapFilePath);
                await ins.CopyToAsync(outs, cancellationToken);
            }

            _messageMap = MessageMap.Load(_mapFilePath);

            _initialized = true;
        }

        private Headers GetHeaders(Destination destination)
        {
            return destination switch
            {
                Destination.Client => In,
                Destination.Server => Out,
                _ => throw new Exception("Invalid destination")
            };
        }

        private Dictionary<(ClientType, short), MessageInfo> GetHeaderMap(Direction direction)
        {
            return direction switch
            {
                Direction.Incoming => _incomingHeaderMap,
                Direction.Outgoing => _outgoingHeaderMap,
                _ => throw new Exception("Invalid direction")
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

        private void AddOrMergeMessage(MessageInfo info)
        {
            var nameMap = info.IsOutgoing ? _outgoingNameMap : _incomingNameMap;
            var headerMap = info.IsOutgoing ? _outgoingHeaderMap : _incomingHeaderMap;

            if ((info.FlashName is not null &&
                nameMap.TryGetValue(info.FlashName, out MessageInfo? existingInfo)) ||
                (info.UnityName is not null &&
                nameMap.TryGetValue(info.UnityName, out existingInfo)))
            {
                if (existingInfo.UnityHeader != -1 &&
                   existingInfo.UnityHeader != info.UnityHeader)
                {
                    Debug.WriteLine(
                        $"Conflicting duplicate {info.Direction.ToString().ToLower()}"
                        + $" message name '{info.UnityName}' (overwriting header value"
                        + $" {existingInfo.UnityHeader} -> {info.UnityHeader})"
                    );
                }

                if (info.UnityHeader >= 0) existingInfo.UnityHeader = info.UnityHeader;
                if (info.FlashHeader >= 0) existingInfo.FlashHeader = info.FlashHeader;
                existingInfo.UnityName ??= info.UnityName;
                existingInfo.FlashName ??= info.FlashName;

                info = existingInfo;
            }
            else
            {
                _messageInfos.Add(info);
            }

            if (!string.IsNullOrWhiteSpace(info.UnityName)) nameMap.TryAdd(info.UnityName, info);
            if (!string.IsNullOrWhiteSpace(info.FlashName)) nameMap.TryAdd(info.FlashName, info);
            if (info.UnityHeader >= 0)
                headerMap[(ClientType.Unity, info.UnityHeader)] = info;
            if (info.FlashHeader >= 0)
                headerMap[(ClientType.Flash, info.FlashHeader)] = info;
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
            return GetHeaders(identifier.Destination).TryGetHeader(identifier.Name, out _);
        }

        public bool TryGetHeader(Identifier identifier, [NotNullWhen(true)] out Header? header)
        {
            return TryGetHeaderByName(identifier.Destination, identifier.Name, out header);
        }

        public bool TryGetHeaderByValue(Destination destination, ClientType clientType, short value, [NotNullWhen(true)] out Header? header)
        {
            return GetHeaders(destination).TryGetHeader(clientType, value, out header);
        }

        public bool TryGetHeaderByName(Destination destination, string name, [NotNullWhen(true)] out Header? header)
        {
            return GetHeaders(destination).TryGetHeader(name, out header);
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
}
