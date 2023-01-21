using System;

using Scriban.Runtime;

namespace Cadru.StronglyTypedId
{
    class EnumFunctions : ScriptObject
    {
        public static bool HasConverter(StronglyTypedIdConverter? converters, StronglyTypedIdConverter flag)
        {
            if (converters.HasValue)
            {
                return converters.Value.HasFlag(flag);
            }

            return false;
        }
    }

    internal static class Defaults
    {
        public static StronglyTypedIdConverter Converters => StronglyTypedIdConverter.TypeConverter | StronglyTypedIdConverter.SystemTextJson;
        public static BackingType BackingType => BackingType.Guid;
        public static string? GetNewValueExpression(BackingType backingType) => backingType switch
        {
            BackingType.Int => "-1",
            BackingType.Long => "-1",
            BackingType.Short => "-1",
            BackingType.String => "string.Empty",
            _ => null
        };

        public static string? GetEmptyValueExpression(BackingType backingType) => backingType switch
        {
            BackingType.Int => "0",
            BackingType.Long => "0",
            BackingType.Short => "0",
            BackingType.String => "string.Empty",
            _ => null
        };
    }
}
