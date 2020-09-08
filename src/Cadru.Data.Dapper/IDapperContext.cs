//------------------------------------------------------------------------------
// <copyright file="Database.cs"
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

using System.Data;

using Cadru.Data.Dapper.Configuration;

using Microsoft.Extensions.Logging;

namespace Cadru.Data.Dapper
{
    public interface IDapperContext
    {
        IDbConnection? Connection { get; }
        ObjectMappingDictionary Mappings { get; }
        ICommandAdapter CommandAdapter { get; }
        DapperContextOptions Options { get; }

        ILogger<IDapperContext> Logger { get; }

        bool HasActiveTransaction { get; }
        void BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted);
        void CommitTransaction();
        void RollbackTransaction();
    }
}