//------------------------------------------------------------------------------
// <copyright file="DatabaseObject{TEntity}.cs"
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

using Dapper;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// Represents a database object.
    /// </summary>
    /// <typeparam name="TEntity">The underlying entity type which this database object maps to.</typeparam>
    public abstract partial class DatabaseObject<TEntity> : IDatabaseObject where TEntity : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseObject{TDatabase}"/> class.
        /// </summary>
        /// <param name="context">The context which contains this entity.</param>
        /// <param name="databaseObjectType">The database object type.</param>
        protected DatabaseObject(IDapperContext context, DatabaseObjectType databaseObjectType)
        {
            this.Context = context;
            this.ObjectMap = context.MapObject<TEntity>(databaseObjectType);
            this.CommandBuilder = new DapperCommandBuilder(this);
        }

        /// <summary>
        /// The context which contains this database object.
        /// </summary>
        public IDapperContext Context { get; private set; }

        /// <inheritdoc/>
        public IObjectMap ObjectMap { get; }

        /// <summary>
        /// The <see cref="IDapperCommandBuilder"/> used by this object to
        /// create <see cref="CommandDefinition"/> instances.
        /// </summary>
        protected IDapperCommandBuilder CommandBuilder { get; set; }
    }
}