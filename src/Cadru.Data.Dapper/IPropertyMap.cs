//------------------------------------------------------------------------------
// <copyright file="%FileName%"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2015 Scott Dorman.
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

namespace Cadru.Data.Dapper
{
    using System.Reflection;

    public interface IPropertyMap
    {
        /// <summary>
        /// Gets the column name for the current property.
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        /// Gets the ignore status of the current property. If ignored, the current property will not be included in queries.
        /// </summary>
        bool Ignored { get; }

        bool IsCalculated { get; }

        bool IsIdentity { get; }

        /// <summary>
        /// Gets the read-only status of the current property. 
        /// If read-only, the current property will not be included in INSERT and UPDATE queries.
        /// </summary>
        bool IsReadOnly { get; }

        bool IsUpdatable { get; }

        /// <summary>
        /// Gets the key type for the current property.
        /// </summary>
        KeyType KeyType { get; }

        /// <summary>
        /// Gets the name of the property by using the specified <see cref="PropertyInfo"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the <see cref="System.Reflection.PropertyInfo"/> for the current property.
        /// </summary>
        PropertyInfo PropertyInfo { get; }
    }
}