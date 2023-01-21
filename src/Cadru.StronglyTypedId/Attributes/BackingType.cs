using System;

namespace Cadru.StronglyTypedId
{
    /// <summary>
    /// The type used for the value of a strongly-typed ID
    /// </summary>
    public enum BackingType
    {
        /// <summary>
        /// Use the default backing type, either the globally configured default
        /// or <see cref="System.Guid"/>.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Use <see cref="System.Guid"/> as the backing type.
        /// </summary>
        Guid = 1,

        /// <summary>
        /// Use <see cref="Int32"/> as the backing type.
        /// </summary>
        Int = 2,

        /// <summary>
        /// Use <see cref="System.String"/> as the backing type.
        /// </summary>
        String = 3,

        /// <summary>
        /// Use <see cref="Int64"/> as the backing type.
        /// </summary>
        Long = 4,

        /// <summary>
        /// Use a nullable <see cref="System.String"/> as the backing type.
        /// </summary>
        NullableString = 5,

        /// <summary>
        /// Use <see cref="Int16"/> as the backing type.
        /// </summary>
        Short = 6,

        /// <summary>
        /// Use <see cref="System.Byte"/> as the backing type.
        /// </summary>
        Byte = 7,

        /// <summary>
        /// Use <see cref="System.Char"/> as the backing type.
        /// </summary>
        Char = 8,
    }
}