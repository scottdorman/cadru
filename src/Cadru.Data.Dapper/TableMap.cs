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
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TableMap<T> : ITableMap where T : class
    {
        private IClassMap classMap;
        private Tuple<string, string, string> tuple;

        public TableMap(string schemaName, string tableName)
        {
            var fullyQualifiedTableName = String.Concat(String.IsNullOrWhiteSpace(schemaName) ? "" : $"{schemaName}.", tableName);
            tuple = Tuple.Create(schemaName, tableName, fullyQualifiedTableName);
            classMap = new ClassMap<T>(tableName) { SchemaName = schemaName };
            classMap.Map();
        }

        public IClassMap ClassMap => classMap;

        public string FullyQualifiedTableName => tuple.Item3;

        public string SchemaName => tuple.Item1;

        public string TableName => tuple.Item2;
    }
}
