//------------------------------------------------------------------------------
// <copyright file="Database.cs"
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
    using global::Dapper;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using Cadru.Extensions;

    public abstract partial class Database : IDatabase
    {
        static ConcurrentDictionary<Type, ITableMap> mappings = new ConcurrentDictionary<Type, ITableMap>();
        int commandTimeout;
        DbConnection connection;
        DbTransaction transaction;

        public static ConcurrentDictionary<Type, ITableMap> Mappings => mappings;

        public DbTransaction Transaction => this.transaction;

        public DbConnection Connection => this.connection;

        public bool HasActiveTransaction => this.transaction != null;

        public void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            transaction = connection.BeginTransaction(isolation);
        }

        public void CommitTransaction()
        {
            transaction.Commit();
            transaction = null;
        }


        public int Execute(string sql, dynamic param = null)
        {
            return SqlMapper.Execute(connection, sql, param as object, transaction, commandTimeout: this.commandTimeout);
        }

        public IEnumerable<T> Query<T>(string sql, dynamic param = null, bool buffered = true)
        {
            return SqlMapper.Query<T>(connection, sql, param as object, transaction, buffered, commandTimeout);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null)
        {
            return SqlMapper.Query(connection, sql, map, param as object, transaction, buffered, splitOn);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null)
        {
            return SqlMapper.Query(connection, sql, map, param as object, transaction, buffered, splitOn);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null)
        {
            return SqlMapper.Query(connection, sql, map, param as object, transaction, buffered, splitOn);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null)
        {
            return SqlMapper.Query(connection, sql, map, param as object, transaction, buffered, splitOn);
        }

        public IEnumerable<dynamic> Query(string sql, dynamic param = null, bool buffered = true)
        {
            return SqlMapper.Query(connection, sql, param as object, transaction, buffered);
        }

        public SqlMapper.GridReader QueryMultiple(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return SqlMapper.QueryMultiple(connection, sql, param, transaction, commandTimeout, commandType);
        }

        public void Dispose()
        {
            if (connection.State != ConnectionState.Closed)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                connection.Close();
                connection = null;
            }
        }

        public void RollbackTransaction()
        {
            transaction.Rollback();
            transaction = null;
        }

        internal ITableMap MapTable<T>() where T : class
        {
            string tableName;
            string schemaName;
            var tableType = typeof(T);
            ITableMap map;

            if (!mappings.TryGetValue(tableType, out map))
            {
                map = new TableMap<T>();
                mappings[tableType] = map;
            }

            return map;
        }

        internal void InitializeDatabase(DbConnection connection, int commandTimeout)
        {
            this.connection = connection;
            this.commandTimeout = commandTimeout;
        }

        protected void InitializeTableProperties()
        {
            var setters = GetType().GetProperties()
                .Where(p => p.PropertyType.HasInterface<ITable>())
                .Select(p => Tuple.Create(
                        p.GetSetMethod(true),
                        p.PropertyType
                 ));

            foreach (var setter in setters)
            {
                var instance = Activator.CreateInstance(setter.Item2, this);
                setter.Item1.Invoke(this, new[] { instance });
            }
        }
    }

    /// <summary>
    /// A container for a database, assumes all the tables have an Id column named Id
    /// </summary>
    /// <typeparam name="TDatabase"></typeparam>
    public abstract partial class Database<TDatabase> : Database where TDatabase : Database<TDatabase>, new()
    {
        public static TDatabase Initialize(DbConnection connection, int commandTimeout)
        {
            var db = new TDatabase();
            db.InitializeDatabase(connection, commandTimeout);
            db.InitializeTableProperties();
            return db;
        }
    }
}