using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    /// <summary>
    /// Provides an interface for exposing attributes to the <see cref="DisplayMetadataProviderContext"/> class.
    /// </summary>
    public interface IMetadataAware
    {
        /// <summary>
        /// Provides metadata to the model metadata creation process.
        /// </summary>
        /// <param name="context">The display metadata context</param>
        void OnMetadataCreated(DisplayMetadataProviderContext context);
    }
}