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
using System.Data;
using Dapper;

namespace {{ ContainingNamespace }}
{
    [System.CodeDom.Compiler.GeneratedCode("Cadru.StronglyTypedId", "{{ AssemblyVersion }}")]
    internal class {{ TypeName }}DapperTypeHandler : SqlMapper.TypeHandler<{{ TypeName }}>
    {
        public override {{ TypeName }} Parse(object value)
        {
            return value switch
            {
                char byteValue => new {{ TypeName }}(byteValue),
                string stringValue when  !string.IsNullOrEmpty(stringValue) && char.TryParse(stringValue, out var result) => new {{ TypeName }}(result),
                _ => throw new System.InvalidCastException($"Unable to cast object of type {value.GetType()} to {{ TypeName }}"),
            };
        }

        public override void SetValue(IDbDataParameter parameter, {{ TypeName }} value)
        {
            parameter.Value = value.Value;
        }
    }
}