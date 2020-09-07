//------------------------------------------------------------------------------
// <copyright file="SqlServerStrategy.cs"
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
using System.Linq;

using Cadru.Extensions;
using Cadru.Polly.Resources;

namespace Cadru.Polly.Sql.SqlServer
{
    /// <summary>
    /// Represents the policies used for performing SQL Server database operations.
    /// </summary>
    public class SqlServerStrategy : SqlStrategy
    {
        /// <inheritdoc/>
        protected override void ValidatePolicies()
        {
            if (this.Policies.Any(x => x.PolicyKey.StartsWith(SqlServerPolicyKeys.TimeoutPerRetryPolicy)
                && !this.Policies.Any(x => x.PolicyKey.StartsWithAny(new[] { SqlServerPolicyKeys.CommonTransientErrorsPolicy, SqlServerPolicyKeys.TransactionPolicy }))))
            {
                throw new InvalidOperationException(Strings.SqlServer_InvalidTimeoutConfiguration);
            }
        }
    }
}