//------------------------------------------------------------------------------
// <copyright file="IObjectMap.cs"
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

using System.Collections.Generic;
using System.Reflection;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents the type mapping information between the database object and it's entity.
    /// </summary>
    public interface IObjectMap
    {
        /// <summary>
        /// A dictionary containing additional metadata information about the entity.
        /// </summary>
        IReadOnlyDictionary<string, object> AdditionalValues { get; }

        /// <summary>
        /// The <see cref="ICommandAdapter" /> used by this object to create SQL statements.
        /// </summary>
        ICommandAdapter CommandAdapter { get; }

        /// <summary>
        /// Gets the <see cref="TypeInfo" /> for entity model of the database object.
        /// </summary>
        TypeInfo EntityType { get; }

        /// <summary>
        /// Gets the fully qualified identifier for the database object.
        /// </summary>
        string FullyQualifiedObjectName { get; }

        /// <summary>
        /// Gets the identifier for the database object.
        /// </summary>
        string ObjectName { get; }

        /// <summary>
        /// Gets a value indicating if the database object is a View or a Table.
        /// </summary>
        DatabaseObjectType ObjectType { get; }

        /// <summary>
        /// A collection of properties that will map to columns in the database table.
        /// </summary>
        IList<IPropertyMap> Properties { get; }

        /// <summary>
        /// Gets the schema identifier for the database object.
        /// </summary>
        string Schema { get; }

        /// <summary>
        /// Maps the database object to it's entity.
        /// </summary>
        void Map();
    }
}