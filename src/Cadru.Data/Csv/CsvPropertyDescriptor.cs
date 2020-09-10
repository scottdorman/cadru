//------------------------------------------------------------------------------
// <copyright file="CsvPropertyDescriptor.cs"
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
using System.ComponentModel;

namespace Cadru.Data.Csv
{
#if !NETSTANDARD1_3
    /// <summary>
    /// Represents a CSV field property descriptor.
    /// </summary>
    public class CsvPropertyDescriptor : PropertyDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the CsvPropertyDescriptor class.
        /// </summary>
        /// <param name="fieldName">The field name.</param>
        /// <param name="index">The field index.</param>
        public CsvPropertyDescriptor(string fieldName, int index) : base(fieldName, null)
        {
            this.Index = index;
        }

        /// <summary>
        /// Gets the field index.
        /// </summary>
        /// <value>The field index.</value>
        public int Index { get; private set; }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            return ((string[]) component)[this.Index];
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override Type ComponentType => typeof(CachedCsvReader);

        public override bool IsReadOnly => true;

        public override Type PropertyType => typeof(string);
    }
#endif
}