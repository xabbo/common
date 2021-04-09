using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.Extensions.Configuration;

using Xabbo.Serialization;

namespace Xabbo.Messages
{
    /// <summary>
    /// Manages messages of the Unity and Flash clients
    /// using a message map file and a Harble API file if needed.
    /// </summary>
    public class UnifiedMessageManager : IMessageManager
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        private readonly string _messageMapPath;
        private readonly MessageMap _messageMap;

        private readonly List<MessageInfo> _messageInfos = new();

        private readonly Dictionary<short, MessageInfo>
            _incomingHeaderMap = new(),
            _outgoingHeaderMap = new();

        private readonly Dictionary<string, MessageInfo>
            _incomingNameMap = new(StringComparer.Ordinal),
            _outgoingNameMap = new(StringComparer.Ordinal);

        public Incoming In { get; private set; }
        public Outgoing Out { get; private set; }

        public Header this[Identifier identifier] => GetHeaders(identifier.Destination)[identifier.Name];

        public UnifiedMessageManager(IConfiguration config)
        {
            In = new Incoming();
            Out = new Outgoing();

            _messageMapPath = config.GetValue("Messages:MapFilePath", "messages.json");

            if (!File.Exists(_messageMapPath))
                throw new FileNotFoundException("Unable to find message map file.", _messageMapPath);

            _messageMap = JsonSerializer.Deserialize<MessageMap>(File.ReadAllText(_messageMapPath), _jsonSerializerOptions)
                ?? throw new InvalidOperationException("Failed to load message map.");
        }

        /// <summary>
        /// Initializes the messages from the message map
        /// </summary>
        private void InitializeMessages(Direction direction)
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
                _messageInfos.Add(new MessageInfo()
                {
                    Direction = direction,
                    Header = item.Header,
                    Name = item.Name,
                    Aliases = new HashSet<string>(
                        item.Aliases,
                        StringComparer.OrdinalIgnoreCase
                    )
                });
            }
        }

        public void Load(ClientType clientType, string? messagesPath)
        {
            _messageInfos.Clear();
            _incomingHeaderMap.Clear();
            _outgoingHeaderMap.Clear();
            _incomingNameMap.Clear();
            _outgoingNameMap.Clear();

            InitializeMessages(Direction.Incoming);
            InitializeMessages(Direction.Outgoing);

            foreach (MessageInfo messageInfo in _messageInfos)
            {
                Dictionary<string, MessageInfo> nameMap =
                    messageInfo.IsIncoming ? _incomingNameMap : _outgoingNameMap;

                if (nameMap.ContainsKey(messageInfo.Name))
                {
                    throw new Exception($"Duplicate {messageInfo.Direction.ToString().ToLower()} message name: '{messageInfo.Name}'");
                }

                nameMap[messageInfo.Name] = messageInfo;

                foreach (string alias in messageInfo.Aliases)
                {
                    if (nameMap.ContainsKey(alias))
                    {
                        throw new Exception($"{messageInfo.Direction} message name conflict for alias '{alias}'");
                    }

                    nameMap[alias] = messageInfo;
                }

                if (clientType != ClientType.Unity)
                {
                    messageInfo.Header = -1;
                }
            }

            if (clientType == ClientType.Unity)
            {
                In.Load(_messageInfos.Where(x => x.IsIncoming).ToDictionary(
                    k => k.Name,
                    v => v.Header
                ));

                Out.Load(_messageInfos.Where(x => x.IsOutgoing).ToDictionary(
                    k => k.Name,
                    v => v.Header
                ));
            }
            else if (clientType == ClientType.Flash)
            {
                if (string.IsNullOrWhiteSpace(messagesPath))
                {
                    throw new Exception("Harble API cache file path not specified");
                }

                HarbleMessages harbleMessages = HarbleMessages.Load(messagesPath);

                Dictionary<string, short>
                    incomingMap = new(),
                    outgoingMap = new();

                foreach (HarbleMessage harbleMessage in Enumerable.Concat(harbleMessages.Incoming, harbleMessages.Outgoing))
                {
                    Dictionary<string, MessageInfo> nameMap =
                        harbleMessage.IsOutgoing ? _outgoingNameMap : _incomingNameMap;

                    Dictionary<string, short> headerMap =
                        harbleMessage.IsOutgoing ? outgoingMap : incomingMap;

                    // Attempt to convert to the Unity message name
                    if (nameMap.TryGetValue(harbleMessage.Name, out MessageInfo? messageInfo))
                    {
                        headerMap[messageInfo.Name] = harbleMessage.Id;
                    }
                    else
                    {
                        // Not found, use the harble message name
                        headerMap[harbleMessage.Name] = harbleMessage.Id;

                        messageInfo = new MessageInfo
                        {
                            Direction = harbleMessage.IsOutgoing ? Direction.Outgoing : Direction.Incoming,
                            Name = harbleMessage.Name,
                            Header = harbleMessage.Id
                        };

                        _messageInfos.Add(messageInfo);
                        nameMap[harbleMessage.Name] = messageInfo;
                    }
                }

                In.Load(incomingMap);
                Out.Load(outgoingMap);
            }
            else
            {
                throw new Exception("Unknown client type");
            }
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

        public bool IdentifierExists(Identifier identifier)
        {
            return GetHeaders(identifier.Destination).TryGetHeader(identifier.Name, out _);
        }

        public bool TryGetHeader(Identifier identifier, [MaybeNullWhen(false)] out Header header)
        {
            return TryGetHeaderByName(identifier.Destination, identifier.Name, out header);
        }

        public bool TryGetHeaderByValue(Destination destination, short value, [MaybeNullWhen(false)] out Header header)
        {
            return GetHeaders(destination).TryGetHeader(value, out header);
        }

        public bool TryGetHeaderByName(Destination destination, string name, [MaybeNullWhen(false)] out Header header)
        {
            return GetHeaders(destination).TryGetHeader(name, out header);
        }
    }
}
