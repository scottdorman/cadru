using System;

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    /// <summary>
    /// Represents the base class for attributes which implement the <see
    /// cref="IMetadataAware"/> interface in order to support additional
    /// metadata.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class MetadataAwareAttribute : Attribute, IMetadataAware
    {
        /// <summary>
        /// The key used to reference this attribute in the <see
        /// cref="DisplayMetadata.AdditionalValues"/> dictionary.
        /// </summary>
        public abstract string Key { get; }

        /// <inheritdoc/>
        public virtual void OnMetadataCreated(DisplayMetadataProviderContext context)
        {
            context.DisplayMetadata.AdditionalValues.Add(this.Key, this);
        }
    }
}