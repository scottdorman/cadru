//------------------------------------------------------------------------------
// <copyright file="Table{TEntity}.cs"
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
    using Contracts;
    using Cadru.Data.Dapper.Predicates;
    using global::Dapper;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    public partial class View<TEntity> : IDatabaseObject where TEntity : class
    {
        private Database database;
        private IObjectMap viewMap;
        private Type underlyingType = typeof(TEntity);
        static ConcurrentDictionary<Type, List<string>> paramNameCache = new ConcurrentDictionary<Type, List<string>>();

        public View(Database database)
        {
            this.database = database;
            this.viewMap = database.MapObject<TEntity>(this.ObjectType);
        }

        public string Schema => this.viewMap.Schema;

        public string ObjectName => this.viewMap.ObjectName;

        public string FullyQualifiedObjectName => this.viewMap.FullyQualifiedObjectName;

        public DatabaseObjectType ObjectType => DatabaseObjectType.View;

        protected Database Database => this.database;

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity Get(IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            return database.Query<TEntity>($"SELECT * FROM {this.FullyQualifiedObjectName} WHERE {predicate.GetSql(parameters)}", parameters).FirstOrDefault();
        }

        /// <summary>
        /// Gets the first record.
        /// </summary>
        /// <returns></returns>
        public virtual TEntity First()
        {
            return database.Query<TEntity>($"SELECT TOP 1 * FROM {this.FullyQualifiedObjectName}").FirstOrDefault();
        }

        /// <summary>
        /// Gets the first record which matches the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual TEntity First(IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            return database.Query<TEntity>($"SELECT TOP 1 * FROM {this.FullyQualifiedObjectName} WHERE {predicate.GetSql(parameters)}").FirstOrDefault();
        }

        /// <summary>
        /// Gets all of the records.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> All()
        {
            return database.Query<TEntity>($"SELECT * FROM {this.FullyQualifiedObjectName}");
        }

        /// <summary>
        /// Gets all of the records which match the given predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> All(IPredicate predicate)
        {
            Requires.NotNull(predicate, "predicate");
            var parameters = new DynamicParameters();
            return database.Query<TEntity>($"SELECT * FROM {this.FullyQualifiedObjectName} WHERE {predicate.GetSql(parameters)}", parameters);
        }
    }
}
