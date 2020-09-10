//------------------------------------------------------------------------------
// <copyright file="MetadataAwareAttribute.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Cadru.AspNetCore.Mvc.ModelBinding.Metadata
{
    /// <summary>
    /// Represents the base class for attributes which implement the <see
    /// cref="IMetadataAware" /> interface in order to support additional
    /// metadata.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
    public abstract class MetadataAwareAttribute : Attribute, IMetadataAware
    {
        /// <summary>
        /// The key used to reference this attribute in the <see
        /// cref="DisplayMetadata.AdditionalValues" /> dictionary.
        /// </summary>
        public abstract string Key { get; }

        /// <inheritdoc/>
        public virtual void OnMetadataCreated(DisplayMetadataProviderContext context)
        {
            context.DisplayMetadata.AdditionalValues.Add(this.Key, this);
        }
    }
}