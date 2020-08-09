using System;

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AdditionalMetadataAttribute : Attribute, IMetadataAware
    {
        public AdditionalMetadataAttribute(string name, object value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }

        public void OnMetadataCreated(DisplayMetadataProviderContext context)
        {
            context.DisplayMetadata.AdditionalValues.Add(this.Name, this.Value);
        }
    }
}