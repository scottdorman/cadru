using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    public interface IMetadataAware
    {
        void OnMetadataCreated(DisplayMetadataProviderContext context);
    }
}
