#nullable restore

using System;
using System.Diagnostics;

namespace Cadru.StronglyTypedId
{
    /// <summary>
    /// Instructs the Cadru.StronglyTypedId source generator to generate source
    /// code that causes the specified type to represent a strongly typed ID.
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    [Conditional("PRESERVE_STRONGLY_TYPED_ID_ATTRIBUTES")]
    public sealed class StronglyTypedIdAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="StronglyTypedIdAttribute"/>.
        /// </summary>
        public StronglyTypedIdAttribute()
        {
        }

        /// <summary>
        /// The <see cref="Type"/> to use to store the strongly-typed ID value
        /// </summary>
        public BackingType BackingType { get; init; } = BackingType.Default;

        /// <summary>
        /// The <see cref="StronglyTypedIdConverter"/> options to use when
        /// generating the strongly-typed ID value
        /// </summary>
        public StronglyTypedIdConverter Converters { get; init; } = StronglyTypedIdConverter.Default;

        //internal static string? GetNewValueString(BackingType backingType, object? value)
        //{
        //    string? final;

        //    if (value != null)
        //    {
        //        final = value switch
        //        {
        //            string => $@"""{ value }""",
        //            _ => value.ToString()
        //        };
        //    }
        //    else
        //    {
        //        final = backingType switch
        //        {
        //            BackingType.Int => "-1",
        //            BackingType.Long => "-1",
        //            BackingType.Short => "-1",
        //            BackingType.String => "string.Empty",
        //            _ => null
        //        };
        //    }

        //    return final;
        //}

        //internal static string? GetEmptyValueString(BackingType backingType, object? value)
        //{
        //    string? final;

        //    if (value != null)
        //    {
        //        final = value switch
        //        {
        //            string => $@"""{ value }""",
        //            _ => value.ToString()
        //        };
        //    }
        //    else
        //    {
        //        final = backingType switch
        //        {
        //            BackingType.Int => "-1",
        //            BackingType.Long => "-1",
        //            BackingType.Short => "-1",
        //            BackingType.String => "string.Empty",
        //            _ => null
        //        };
        //    }

        //    return final;
        //}
    }
}