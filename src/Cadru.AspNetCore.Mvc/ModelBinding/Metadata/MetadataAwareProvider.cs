using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    /// <summary>
    /// A provider to insert additional metadata into the
    /// <see cref="DisplayMetadata"/>.
    /// </summary>
    public class MetadataAwareProvider : IDisplayMetadataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataAwareProvider"/> class.
        /// </summary>
        public MetadataAwareProvider() { }

        /// <inheritdoc/>
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            if (context.PropertyAttributes != null)
            {
                foreach (var propAttr in context.PropertyAttributes)
                {
                    if (propAttr is IMetadataAware metadataAware)
                    {
                        metadataAware.OnMetadataCreated(context);
                    }
                }
            }
        }
    }
}