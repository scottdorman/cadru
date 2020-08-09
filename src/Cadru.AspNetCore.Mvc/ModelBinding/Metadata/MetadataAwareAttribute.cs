using System;

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class MetadataAwareAttribute : Attribute, IMetadataAware
    {
        public abstract string Key { get; }

        public virtual void OnMetadataCreated(DisplayMetadataProviderContext context)
        {
            context.DisplayMetadata.AdditionalValues.Add(this.Key, this);
        }
    }
}
