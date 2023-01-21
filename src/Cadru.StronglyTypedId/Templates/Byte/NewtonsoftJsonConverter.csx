﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the Cadru.StronglyTypedId source generator
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
{{ if NullableEnabled }}#nullable restore{{ end }}

using System;
using Newtonsoft.Json;

namespace {{ ContainingNamespace }}
{
    [System.CodeDom.Compiler.GeneratedCode("Cadru.StronglyTypedId", "{{ AssemblyVersion }}")]
    internal class {{ TypeName }}NewtonsoftJsonConverter: JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof({{ TypeName }});
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = serializer.Deserialize<byte?>(reader);
            return result.HasValue ? new {{ TypeName }}(result.Value) : null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var id = ({{ TypeName }})value;
            serializer.Serialize(writer, id.Value);
        }
    }
}