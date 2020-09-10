//------------------------------------------------------------------------------
// <copyright file="PredicateTests.cs"
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
using System.Linq;

using Cadru.Data.Dapper.Predicates;

using Dapper;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Dapper.Tests
{
    public class PlanComponent
    {
        public int FiscalYear { get; set; }
        public string PlanID { get; set; }
    }

    [TestClass, Ignore("Incomplete test")]
    public class PredicateTests
    {
        [TestMethod]
        public void FieldComparison()
        {
            var plans = Enumerable.Empty<string>();
            var planComponents = new List<PlanComponent>
            {
                new PlanComponent{ FiscalYear = 2019, PlanID = "P003"},
                new PlanComponent{ FiscalYear = 2019, PlanID = "P005"},
            };

            var fiscalYear = 2019;
            var parameters = new DynamicParameters();

            var predicate = Predicate.And(new[] {
                        Predicate.FieldComparison<PlanComponent, int>(m => m.FiscalYear, Operator.Equal, fiscalYear),
                        Predicate.FieldComparison<PlanComponent, string>(m => m.PlanID, Operator.In, plans),
            });

            predicate = Predicate.And(new[] {
                        Predicate.FieldComparison<PlanComponent, int>(m => m.FiscalYear, Operator.Equal, fiscalYear),
                        Predicate.FieldComparison<PlanComponent, string>(m => m.PlanID, Operator.Equal, (string)null),
            });
        }
    }
}