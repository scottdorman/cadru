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
using System.Text.Json;
using System.Text.Json.Serialization;

namespace {{ ContainingNamespace }}
{
    [System.CodeDom.Compiler.GeneratedCode("Cadru.StronglyTypedId", "{{ AssemblyVersion }}")]
    internal class {{ TypeName }}JsonConverter: JsonConverter<{{ TypeName }}>
    {
        public override {{ TypeName }} Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new {{ TypeName }}(reader.GetByte());
        }

        public override void Write(Utf8JsonWriter writer, {{ TypeName }} value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value.Value);
        }
    }
}