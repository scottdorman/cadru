#nullable restore

using System;
using System.Diagnostics;

namespace Cadru.StronglyTypedId
{
    /// <summary>
    /// Provides assembly-global defaults for the Cadru.StronglyTypedId source
    /// generator to use when generating a strongly typed ID.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    [Conditional("PRESERVE_STRONGLY_TYPED_ID_ATTRIBUTES")]
    public sealed class StronglyTypedIdDefaultsAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance of the <see cref="StronglyTypedIdDefaultsAttribute"/>.
        /// </summary>
        public StronglyTypedIdDefaultsAttribute()
        {
        }

        /// <summary>
        /// The <see cref="BackingType"/> to use for storing the strongly-typed
        /// ID value
        /// </summary>
        public BackingType BackingType { get; init; } = BackingType.Default;

        /// <summary>
        /// The <see cref="StronglyTypedIdConverter"/> options to use when
        /// generating the strongly-typed ID value
        /// </summary>
        public StronglyTypedIdConverter Converters { get; init; } = StronglyTypedIdConverter.Default;
    }
}