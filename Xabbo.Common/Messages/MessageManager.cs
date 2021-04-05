using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Xabbo.Serialization;
using Xabbo.Utility;

namespace Xabbo.Messages
{
    public class MessageManager : IMessageManager
    {
        private const int
            INDEX_VALUE = 0,
            INDEX_HASH = 1,
            INDEX_ALIASES = 2;

        private readonly List<MessageInfo> _messageInfos = new();

        private readonly Dictionary<string, MessageInfo>
            _incomingHashMap = new(), _outgoingHashMap = new();

        private readonly Dictionary<string, MessageInfo>
            _incomingMessageNames = new(StringComparer.OrdinalIgnoreCase),
            _outgoingMessageNames = new(StringComparer.OrdinalIgnoreCase);

        public Incoming In { get; private set; }
        public Outgoing Out { get; private set; }

        public Header this[Identifier identifier] => GetHeaders(identifier.Destination)[identifier.Name];

        public MessageManager(ClientType clientType, string messagesPath, string harblePath)
        {
            In = new Incoming();
            Out = new Outgoing();

            Load(clientType, messagesPath, harblePath);
        }

        private void LoadMessages(Ini messages, Destination destination)
        {
            string sectionName = destination == Destination.Client ?
                "Incoming" : "Outgoing";

            foreach (var (unityName, value) in messages[sectionName])
            {
                string[] values = value.Split(';');
                if (values.Length == 0)
                    throw new FormatException("Invalid message file format");

                short header = short.Parse(values[INDEX_VALUE]);
                string hash = string.Empty;
                HashSet<string> aliases = new(StringComparer.OrdinalIgnoreCase);

                if (values.Length > INDEX_HASH)
                {
                    hash = values[INDEX_HASH];
                }

                if (values.Length > INDEX_ALIASES)
                {
                    foreach (string alias in values[INDEX_ALIASES].Split(','))
                        aliases.Add(alias);
                }

                _messageInfos.Add(new MessageInfo {
                    Destination = destination,
                    UnityHeader = header,
                    Hash = hash,
                    Name = unityName,
                    Aliases = aliases
                });
            }
        }

        private void Load(ClientType clientType, string messagesPath, string harblePath)
        {
            Ini messagesIni = Ini.Load(messagesPath);

            LoadMessages(messagesIni, Destination.Client);
            LoadMessages(messagesIni, Destination.Server);

            foreach (MessageInfo messageInfo in _messageInfos)
            {
                Dictionary<string, MessageInfo>
                    nameDict = messageInfo.Destination == Destination.Client ? _incomingMessageNames : _outgoingMessageNames,
                    hashDict = messageInfo.Destination == Destination.Client ? _incomingHashMap : _outgoingHashMap;

                nameDict[messageInfo.Name] = messageInfo;
                foreach (string alias in messageInfo.Aliases)
                    nameDict[alias] = messageInfo;
            }

            if (clientType == ClientType.Unity)
            {
                In.Load(messagesIni["Incoming"].ToDictionary(
                    k => k.Key,
                    v => short.Parse(v.Value.Split(';')[0])
                ));
                Out.Load(messagesIni["Outgoing"].ToDictionary(
                    k => k.Key,
                    v => short.Parse(v.Value.Split(';')[0])
                ));
            }
            else if (clientType == ClientType.Flash)
            {
                if (string.IsNullOrWhiteSpace(harblePath))
                {
                    throw new Exception("Harble API cache file path not specified");
                }

                HarbleMessages harbleMessages = HarbleMessages.Load(harblePath);

                Dictionary<string, short>
                    incomingMap = new(),
                    outgoingMap = new();

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
