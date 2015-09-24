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
    using System;
    using System.Collections.Generic;

    public interface IClassMap
    {
        Type EntityType { get; }

        /// <summary>
        /// A collection of properties that will map to columns in the database table.
        /// </summary>
        IList<IPropertyMap> Properties { get; }

        /// <summary>
        /// Gets or sets the schema to use when referring to the corresponding table name in the database.
        /// </summary>
        string SchemaName { get; set; }

        /// <summary>
        /// Gets or sets the table to use in the database.
        /// </summary>
        string TableName { get; set; }

        void Map();
    }
}
