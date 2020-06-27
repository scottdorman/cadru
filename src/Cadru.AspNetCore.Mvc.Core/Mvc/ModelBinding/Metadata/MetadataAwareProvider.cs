using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    public class MetadataAwareProvider : IDisplayMetadataProvider
    {
        public MetadataAwareProvider() { }

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
