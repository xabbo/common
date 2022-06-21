using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

using Xabbo.Messages;

namespace Xabbo.Serialization;

public class MessageMap
{
    private static readonly Regex
        _regexValidIdentifier = new Regex(@"^[a-z][a-z0-9]*$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public List<MessageMapItem> Incoming { get; set; }
    public List<MessageMapItem> Outgoing { get; set; }

    public MessageMap()
    {
        Incoming = new List<MessageMapItem>();
        Outgoing = new List<MessageMapItem>();
    }

    public static MessageMap Load(string filePath)
    {
        MessageMap map = new();

        using StreamReader reader = new StreamReader(filePath);

        Direction direction = Direction.Unknown;

        string? line; int lineNumber = 0;
        while ((line = reader.ReadLine()) != null)
        {
            lineNumber++;
            line = line.Trim();
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.StartsWith(';'))
            {
                continue;
            }
            else if (line.StartsWith('[') && line.EndsWith(']'))
            {
                string sectionName = line[1..^1];
                direction = sectionName.ToLower() switch
                {
                    "incoming" => Direction.Incoming,
                    "outgoing" => Direction.Outgoing,
                    _ => throw new Exception($"Invalid section name '{sectionName}' on line {lineNumber} in message map file.")
                };
            }
            else
            {
                if (!TryParseEntry(line, out MessageMapItem? item))
                    throw new Exception($"Invalid entry on line {lineNumber}.");

                List<MessageMapItem> list = (direction == Direction.Incoming) ?
                    map.Incoming : map.Outgoing;
                list.Add(item);
            }
        }

        return map;
    }

    private static bool TryParseEntry(string line, [NotNullWhen(true)] out MessageMapItem? item)
    {
        item = null;

        int equalIndex = line.IndexOf('=');
        if (equalIndex <= 0) return false;

        string unityName = line[..equalIndex].Trim();
        string value = line[(equalIndex + 1)..].Trim();

        if (!_regexValidIdentifier.IsMatch(unityName))
            return false;
;
        string? flashName = null;

        int semicolonIndex = value.IndexOf(';');
        if (semicolonIndex > 0)
        {
            flashName = value[(semicolonIndex + 1)..].Trim();
            value = value[..semicolonIndex];

            if (flashName == "*")
            {
                flashName = unityName;
            }
            else if (flashName == "!")
            {
                flashName = null;
            }
        }

        if (!short.TryParse(value, out short headerValue))
            return false;

        item = new MessageMapItem()
        {
            UnityName = unityName,
            Header = headerValue,
            FlashName = flashName
        };

        return true;
    }
}
