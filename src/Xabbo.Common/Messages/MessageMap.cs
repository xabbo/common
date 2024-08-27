using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Collections;

namespace Xabbo.Messages;

/// <summary>
/// Maps message names between clients.
/// </summary>
public sealed partial class MessageMap : IReadOnlyDictionary<Identifier, MessageNames>
{
    private enum ParseResult
    {
        Invalid,
        DuplicateClient,
        Ok,
    }

    private readonly Dictionary<Identifier, MessageNames> _names = [];

    public IEnumerable<Identifier> Keys => _names.Keys;
    public IEnumerable<MessageNames> Values => _names.Values;
    public int Count => _names.Count;

    public MessageNames this[Identifier key] => _names[key];

    public bool ContainsKey(Identifier key) => _names.ContainsKey(key);
    public bool TryGetValue(Identifier key, [MaybeNullWhen(false)] out MessageNames value) => _names.TryGetValue(key, out value);
    public IEnumerator<KeyValuePair<Identifier, MessageNames>> GetEnumerator() => _names.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static MessageMap Load(string filePath)
    {
        MessageMap map = new();

        using StreamReader reader = new(filePath);

        Direction direction = Direction.None;

        string? line; int lineNumber = 0;
        while ((line = reader.ReadLine()) != null)
        {
            lineNumber++;
            line = line.Trim();
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(';'))
                continue;

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                string sectionName = line[1..^1];
                direction = sectionName.ToUpperInvariant() switch
                {
                    "INCOMING" => Direction.In,
                    "OUTGOING" => Direction.Out,
                    _ => throw new Exception($"Invalid section name '{sectionName}' on line {lineNumber} in message map file.")
                };
            }
            else if (direction == Direction.None)
            {
                throw new Exception($"Entry not in section on line {lineNumber}.");
            }
            else
            {
                if (!TryParseEntry(line, out MessageNames item, out ParseResult result))
                {
                    throw result switch
                    {
                        ParseResult.DuplicateClient => new Exception($"Duplicate client identifier on line {lineNumber}."),
                        _ => new Exception($"Invalid entry on line {lineNumber}."),
                    };
                }

                foreach (var client in new[] { ClientType.Unity, ClientType.Flash, ClientType.Shockwave })
                {
                    string? name = item.GetName(client);
                    if (name is not null && !map._names.TryAdd((client, direction, name), item))
                        throw new Exception($"Duplicate {direction} client identifier {name} on line {lineNumber}.");
                }
            }
        }

        return map;
    }

    private static bool TryParseEntry(string line, out MessageNames item, out ParseResult result)
    {
        item = new();
        result = ParseResult.Invalid;
        var processed = ClientType.None;

        var commentSplit = line.Split(';', 2, StringSplitOptions.TrimEntries);
        if (commentSplit.Length == 0)
            return false;

        var fieldSplit = commentSplit[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        foreach (var field in fieldSplit)
        {
            var split = field.Split(':');
            if (split.Length != 2)
                return false;

            if (split[0].StartsWith('!'))
                continue;

            foreach (char c in split[0])
            {
                var client = GetClientFromChar(c);
                if (client == ClientType.None)
                    return false;
                if ((processed & client) > 0)
                {
                    result = ParseResult.DuplicateClient;
                    return false;
                }
                if (split[1] == "-") continue;
                item = item.WithName(client, split[1]);
                processed |= client;
            }
        }

        if (processed == ClientType.None)
            return false;

        result = ParseResult.Ok;
        return true;
    }

    private static ClientType GetClientFromChar(char c) => c switch
    {
        'u' => ClientType.Unity,
        'f' => ClientType.Flash,
        's' => ClientType.Shockwave,
        _ => ClientType.None,
    };
}
