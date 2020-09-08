//------------------------------------------------------------------------------
// <copyright file="IDatabase.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2017 Scott Dorman.
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
    using System.Data;
    using System.Data.Common;

    using global::Dapper;

#nullable disable
#pragma warning disable 1591
    [Obsolete("Use IDapperContext.")]
    public interface IDatabase : IDisposable
    {
        DbConnection Connection { get; }
        bool HasActiveTransaction { get; }

        void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        int Execute(string sql, dynamic param = null, int? commandTimeout = default);
        System.Collections.Generic.IEnumerable<dynamic> Query(string sql, dynamic param = null, bool buffered = true, int? commandTimeout = default);
        System.Collections.Generic.IEnumerable<T> Query<T>(string sql, dynamic param = null, bool buffered = true, int? commandTimeout = default);
        System.Collections.Generic.IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, System.Func<TFirst, TSecond, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default);
        System.Collections.Generic.IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, System.Func<TFirst, TSecond, TThird, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default);
        System.Collections.Generic.IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, System.Func<TFirst, TSecond, TThird, TFourth, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default);
        System.Collections.Generic.IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, System.Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, dynamic param = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = default);
        SqlMapper.GridReader QueryMultiple(string sql, dynamic param = null, int? commandTimeout = default, CommandType? commandType = default);
        void RollbackTransaction();
    }
#pragma warning restore 1591
#nullable enable
}