//------------------------------------------------------------------------------
// <copyright file="CombTypeHandler.cs"
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
using System.Data;

using Dapper;

namespace Cadru.Data.Dapper
{
    /// <summary>
    /// A type handler to convert between a <see cref="Comb" /> and a <see cref="Guid" />.
    /// </summary>
    public class CombTypeHandler : SqlMapper.TypeHandler<Comb>
    {
        /// <inheritdoc/>
        public override Comb Parse(object value)
        {
            return new Comb(value.ToString());
        }

        /// <inheritdoc/>
        public override void SetValue(IDbDataParameter parameter, Comb value)
        {
            parameter.DbType = DbType.Guid;
            parameter.Value = new Guid(value.ToString());
        }
    }
}