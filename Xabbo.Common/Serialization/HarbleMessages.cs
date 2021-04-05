using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Xabbo.Serialization
{
    public class HarbleMessages
    {
        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public string Revision { get; set; }
        public long FileLength { get; set; }
        public List<HarbleMessage> Incoming { get; set; }
        public List<HarbleMessage> Outgoing { get; set; }

        public HarbleMessages()
        {
            Revision = string.Empty;
            FileLength = -1;
            Incoming = new List<HarbleMessage>();
            Outgoing = new List<HarbleMessage>();
        }

        public static HarbleMessages Load(string path)
        {
            return JsonSerializer.Deserialize<HarbleMessages>(
                File.ReadAllText(path),
                _jsonSerializerOptions
            ) ?? throw new Exception("Failed to load");
        }
    }
}
