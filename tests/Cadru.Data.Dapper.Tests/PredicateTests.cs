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

    [TestClass]
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

            var x = predicate.GetSql(parameters);
        }
    }
}
