﻿// <auto-generated/>

#nullable enable annotations
#nullable disable warnings

// Suppress warnings about [Obsolete] member usage in generated code.
#pragma warning disable CS0612, CS0618

namespace Xabbo
{
    internal partial class SourceGenerationContext
    {
        private global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<string>? _String;
        
        /// <summary>
        /// Defines the source generated JSON serialization contract metadata for a given type.
        /// </summary>
        public global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<string> String
        {
            get => _String ??= (global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<string>)Options.GetTypeInfo(typeof(string));
        }
        
        private global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<string> Create_String(global::System.Text.Json.JsonSerializerOptions options)
        {
            if (!TryGetTypeInfoForRuntimeCustomConverter<string>(options, out global::System.Text.Json.Serialization.Metadata.JsonTypeInfo<string> jsonTypeInfo))
            {
                jsonTypeInfo = global::System.Text.Json.Serialization.Metadata.JsonMetadataServices.CreateValueInfo<string>(options, global::System.Text.Json.Serialization.Metadata.JsonMetadataServices.StringConverter);
            }
        
            jsonTypeInfo.OriginatingResolver = this;
            return jsonTypeInfo;
        }
    }
}
