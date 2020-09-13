using System.Linq;

using Cadru.Polly.Data;
using Cadru.Polly.Data.SqlServer;
using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Polly.Wrap;

namespace Cadru.Polly.Tests
{
    [TestClass]
    public class SqlStrategyTests
    {
        [TestMethod]
        public void DefaultSqlServerStrategyBuilder()
        {
            var strategy = SqlServerStrategyBuilder.Default.Build();
            Assert.IsNotNull(strategy);
            Assert.That.IsType<SqlStrategy>(strategy);
            var policies = ((PolicyWrap)strategy.SyncPolicy).GetPolicies();
            Assert.IsTrue(policies.Count() != 0);

            Assert.IsTrue(SqlServerStrategyBuilder.Empty.Policies.Count() == 0);
            ;
        }
    }
}
