using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Xabbo;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Hotel))]
[JsonSerializable(typeof(List<Hotel>))]
internal partial class SourceGenerationContext : JsonSerializerContext { }