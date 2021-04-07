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
            _incomingHeaderMap = new(), _outgoingHeaderMap = new();

        private readonly Dictionary<string, MessageInfo>
            _incomingHashMap = new(), _outgoingHashMap = new();

        private readonly Dictionary<string, MessageInfo>
            _incomingMessageNames = new(StringComparer.OrdinalIgnoreCase),
            _outgoingMessageNames = new(StringComparer.OrdinalIgnoreCase);

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
        private void InitializeMessages(Destination destination)
        {
            List<MessageMapItem>? list = destination switch
            {
                Destination.Client => _messageMap.Incoming,
                Destination.Server => _messageMap.Outgoing,
                _ => null
            };

            if (list is null) return;

            foreach (MessageMapItem item in list)
            {
                _messageInfos.Add(new MessageInfo()
                {
                    Destination = destination,
                    UnityHeader = item.Header,
                    Hash = item.Hash,
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

            foreach (Destination destination in Enum.GetValues<Destination>())
                InitializeMessages(destination);

            foreach (MessageInfo messageInfo in _messageInfos)
            {
                Dictionary<string, MessageInfo>
                    nameDict = messageInfo.IsOutgoing ? _outgoingMessageNames : _incomingMessageNames,
                    hashDict = messageInfo.IsOutgoing ? _outgoingHashMap : _incomingHashMap;

                nameDict[messageInfo.Name] = messageInfo;
                foreach (string alias in messageInfo.Aliases)
                    nameDict[alias] = messageInfo;
            }

            if (clientType == ClientType.Unity)
            {
                In.Load(_messageInfos.Where(x => x.IsIncoming).ToDictionary(
                    k => k.Name,
                    v => v.UnityHeader
                ));
                Out.Load(_messageInfos.Where(x => x.IsOutgoing).ToDictionary(
                    k => k.Name,
                    v => v.UnityHeader
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

                foreach (HarbleMessage message in Enumerable.Concat(harbleMessages.Incoming, harbleMessages.Outgoing))
                {
                    Dictionary<string, short> dict = message.IsOutgoing ? outgoingMap : incomingMap;
                    dict[message.Name] = message.Id;
                }

                foreach (MessageInfo messageInfo in _messageInfos)
                {
                    List<HarbleMessage> list = messageInfo.Destination == Destination.Client
                        ? harbleMessages.Incoming : harbleMessages.Outgoing;

                    Dictionary<string, short> map = messageInfo.Destination == Destination.Client
                        ? incomingMap : outgoingMap;

                    if (!string.IsNullOrWhiteSpace(messageInfo.Hash))
                    {
                        HarbleMessage? message = list.FirstOrDefault(x => x.Hash.Equals(messageInfo.Hash, StringComparison.OrdinalIgnoreCase));
                        if (message is not null)
                        {
                            map.Add(messageInfo.Name, message.Id);
                        }
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

        public bool TryGetHeaderByHash(Destination destination, string hash, [MaybeNullWhen(false)] out Header header)
        {
            Dictionary<string, MessageInfo> hashMap = destination == Destination.Client
                ? _incomingHashMap : _outgoingHashMap;

            if (hashMap.TryGetValue(hash, out MessageInfo? messageInfo))
            {
                return GetHeaders(destination).TryGetHeader(messageInfo.Header, out header);
            }
            else
            {
                header = Header.Unknown;
                return false;
            }
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
