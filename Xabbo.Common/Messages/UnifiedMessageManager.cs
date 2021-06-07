using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        private readonly MessageMap _messageMap;

        private readonly List<MessageInfo> _messageInfos = new();

        private readonly Dictionary<short, MessageInfo>
            _incomingHeaderMap = new(),
            _outgoingHeaderMap = new();

        private readonly Dictionary<string, MessageInfo>
            _incomingNameMap = new(StringComparer.OrdinalIgnoreCase),
            _outgoingNameMap = new(StringComparer.OrdinalIgnoreCase);

        public Incoming In { get; private set; }
        public Outgoing Out { get; private set; }

        public Header this[Identifier identifier] => GetHeaders(identifier.Destination)[identifier.Name];

        public UnifiedMessageManager(IConfiguration config)
            : this(config.GetValue("Xabbo:Messages:MapFilePath", "messages.ini"))
        { }

        public UnifiedMessageManager(string filePath)
        {
            In = new Incoming();
            Out = new Outgoing();

            if (!File.Exists(filePath))
                throw new FileNotFoundException("Unable to find message map file.", filePath);

            _messageMap = MessageMap.Load(filePath);
        }

        private HeaderDictionary GetHeaders(Destination destination)
        {
            return destination switch
            {
                Destination.Client => In,
                Destination.Server => Out,
                _ => throw new Exception("Invalid destination")
            };
        }

        private Dictionary<short, MessageInfo> GetHeaderMap(Direction direction)
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

        private void Initialize(ClientType clientType)
        {
            _messageInfos.Clear();
            _incomingHeaderMap.Clear();
            _outgoingHeaderMap.Clear();
            _incomingNameMap.Clear();
            _outgoingNameMap.Clear();

            InitializeMessages(clientType, Direction.Incoming);
            InitializeMessages(clientType, Direction.Outgoing);
        }

        /// <summary>
        /// Initializes the messages from the message map
        /// </summary>
        private void InitializeMessages(ClientType clientType, Direction direction)
        {
            List<MessageMapItem>? list = direction switch
            {
                Direction.Incoming => _messageMap.Incoming,
                Direction.Outgoing => _messageMap.Outgoing,
                _ => null
            };

            if (list is null) return;

            foreach (MessageMapItem item in list)
            {
                AddOrMergeMessage(new MessageInfo()
                {
                    Direction = direction,
                    Header = clientType == ClientType.Unity ? item.Header : -1,
                    Name = item.UnityName,
                    UnityName = item.UnityName,
                    FlashName = item.FlashName
                });
            }
        }

        private void AddOrMergeMessage(MessageInfo info)
        {
            var nameMap = info.IsOutgoing ? _outgoingNameMap : _incomingNameMap;
            var headerMap = info.IsOutgoing ? _outgoingHeaderMap : _incomingHeaderMap;

            if (nameMap.TryGetValue(info.Name, out MessageInfo? existingInfo))
            {
                if (existingInfo.Header != -1 &&
                   existingInfo.Header != info.Header)
                {
                    Debug.WriteLine(
                        $"Conflicting duplicate {info.Direction.ToString().ToLower()}"
                        + $" message name '{info.Name}' (overwriting header value"
                        + $" {existingInfo.Header} -> {info.Header})"
                    );
                }

                existingInfo.Name = info.Name;
                existingInfo.Header = info.Header;
                existingInfo.UnityName ??= info.UnityName;
                existingInfo.FlashName ??= info.FlashName;

                info = existingInfo;
            }
            else
            {
                nameMap.Add(info.Name, info);
                _messageInfos.Add(info);
            }

            nameMap.TryAdd(info.Name, info);

            if (!string.IsNullOrWhiteSpace(info.UnityName))
                nameMap.TryAdd(info.UnityName, info);

            if (!string.IsNullOrWhiteSpace(info.FlashName))
                nameMap.TryAdd(info.FlashName, info);

            if (info.Header >= 0)
                headerMap[info.Header] = info;
        }

        public void LoadMessages(ClientType clientType, IEnumerable<MessageInfo>? messages = null)
        {
            Initialize(clientType);

            if (messages is null)
                messages = Enumerable.Empty<MessageInfo>();

            foreach (MessageInfo info in messages)
            {
                AddOrMergeMessage(info);
            }

            Dictionary<string, short>
                inDict = new(),
                outDict = new();

            foreach (MessageInfo info in messages)
            {
                var dict = info.IsOutgoing ? outDict : inDict;
                dict[info.Name] = info.Header;
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

        public bool TryGetHeaderByValue(Destination destination, short value, [NotNullWhen(true)] out Header? header)
        {
            return GetHeaders(destination).TryGetHeader(value, out header);
        }

        public bool TryGetHeaderByName(Destination destination, string name, [NotNullWhen(true)] out Header? header)
        {
            return GetHeaders(destination).TryGetHeader(name, out header);
        }

        public bool TryGetInfoByHeader(Direction direction, short header, [NotNullWhen(true)] out MessageInfo? info)
        {
            return GetHeaderMap(direction).TryGetValue(header, out info);
        }

        public bool TryGetInfoByName(Direction direction, string name, [NotNullWhen(true)] out MessageInfo? info)
        {
            return GetNameMap(direction).TryGetValue(name, out info);
        }
    }
}
