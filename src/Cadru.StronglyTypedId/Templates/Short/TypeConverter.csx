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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace {{ ContainingNamespace }}
{
    [System.CodeDom.Compiler.GeneratedCode("Cadru.StronglyTypedId", "{{ AssemblyVersion }}")]
    internal class {{ TypeName }}TypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        {
            return sourceType == typeof(short) || sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            return value switch
            {
                short numValue => new {{ TypeName }}(numValue),
                string stringValue when !string.IsNullOrEmpty(stringValue) && short.TryParse(stringValue, out var result) => new {{ TypeName }}(result),
                _ => base.ConvertFrom(context, culture, value),
            };
        }

        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? sourceType)
        {
            return sourceType == typeof(short) || sourceType == typeof(string) || base.CanConvertTo(context, sourceType);
        }

        public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (value is {{ TypeName }} idValue)
            {
                if (destinationType == typeof(short))
                {
                    return idValue.Value;
                }

                if (destinationType == typeof(string))
                {
                    return idValue.Value.ToString();
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}