//------------------------------------------------------------------------------
// <copyright file="AdditionalMetadataAttribute.cs"
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
    /// Provides a class that implements the <see cref="IMetadataAware" />
    /// interface in order to support additional metadata.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class AdditionalMetadataAttribute : MetadataAwareAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdditionalMetadataAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the model metadata.</param>
        /// <param name="value">The value of the model metadata.</param>
        public AdditionalMetadataAttribute(string name, object value)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Value = value;
        }

        /// <inheritdoc/>
        /// <remarks>This property is not used by the <see cref="AdditionalMetadataAttribute" />.</remarks>
        public override string Key => throw new NotImplementedException();

        /// <summary>
        /// Gets the name of the additional metadata attribute.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the value of the of the additional metadata attribute.
        /// </summary>
        public object Value { get; private set; }

        /// <inheritdoc/>
        public override void OnMetadataCreated(DisplayMetadataProviderContext context)
        {
            context.DisplayMetadata.AdditionalValues.Add(this.Name, this.Value);
        }
    }
}