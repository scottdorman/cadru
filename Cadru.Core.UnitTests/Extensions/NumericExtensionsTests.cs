using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Text;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class NumericExtensionsTests
    {
        [TestMethod]
        public void Between()
        {
            Assert.IsTrue(NumericExtensions.Between((byte)5, (byte)5, (byte)10));
            Assert.IsTrue(NumericExtensions.Between((byte)10, (byte)5, (byte)10));
            Assert.IsTrue(NumericExtensions.Between((byte)6, (byte)5, (byte)10));
            Assert.IsFalse(NumericExtensions.Between((byte)4, (byte)5, (byte)10));
            Assert.IsFalse(NumericExtensions.Between((byte)11, (byte)5, (byte)10));

            Assert.IsFalse(NumericExtensions.Between((byte)5, (byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between((byte)10, (byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between((byte)6, (byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between((byte)4, (byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between((byte)11, (byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(NumericExtensions.Between((byte)5, (byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between((byte)10, (byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(NumericExtensions.Between((byte)6, (byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between((byte)4, (byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between((byte)11, (byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(NumericExtensions.Between((byte)5, (byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between((byte)10, (byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsTrue(NumericExtensions.Between((byte)6, (byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between((byte)4, (byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between((byte)11, (byte)5, (byte)10, NumericComparisonOptions.None));

            Assert.IsTrue(NumericExtensions.Between(5m, 5m, 10m));
            Assert.IsTrue(NumericExtensions.Between(10m, 5m, 10m));
            Assert.IsTrue(NumericExtensions.Between(6m, 5m, 10m));
            Assert.IsFalse(NumericExtensions.Between(4m, 5m, 10m));
            Assert.IsFalse(NumericExtensions.Between(11m, 5m, 10m));

            Assert.IsFalse(NumericExtensions.Between(5m, 5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(10m, 5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(6m, 5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(4m, 5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(11m, 5m, 10m, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(NumericExtensions.Between(5m, 5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(10m, 5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(NumericExtensions.Between(6m, 5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(4m, 5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(11m, 5m, 10m, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(NumericExtensions.Between(5m, 5m, 10m, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(10m, 5m, 10m, NumericComparisonOptions.None));
            Assert.IsTrue(NumericExtensions.Between(6m, 5m, 10m, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(4m, 5m, 10m, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(11m, 5m, 10m, NumericComparisonOptions.None));


            Assert.IsTrue(NumericExtensions.Between(5d, 5d, 10d));
            Assert.IsTrue(NumericExtensions.Between(10d, 5d, 10d));
            Assert.IsTrue(NumericExtensions.Between(6d, 5d, 10d));
            Assert.IsFalse(NumericExtensions.Between(4d, 5d, 10d));
            Assert.IsFalse(NumericExtensions.Between(11d, 5d, 10d));

            Assert.IsFalse(NumericExtensions.Between(5d, 5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(10d, 5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(6d, 5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(4d, 5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(11d, 5d, 10d, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(NumericExtensions.Between(5d, 5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(10d, 5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(NumericExtensions.Between(6d, 5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(4d, 5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(11d, 5d, 10d, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(NumericExtensions.Between(5d, 5d, 10d, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(10d, 5d, 10d, NumericComparisonOptions.None));
            Assert.IsTrue(NumericExtensions.Between(6d, 5d, 10d, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(4d, 5d, 10d, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(11d, 5d, 10d, NumericComparisonOptions.None));


            Assert.IsTrue(NumericExtensions.Between(5f, 5f, 10f));
            Assert.IsTrue(NumericExtensions.Between(10f, 5f, 10f));
            Assert.IsTrue(NumericExtensions.Between(6f, 5f, 10f));
            Assert.IsFalse(NumericExtensions.Between(4f, 5f, 10f));
            Assert.IsFalse(NumericExtensions.Between(11f, 5f, 10f));

            Assert.IsFalse(NumericExtensions.Between(5f, 5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(10f, 5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(6f, 5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(4f, 5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(11f, 5f, 10f, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(NumericExtensions.Between(5f, 5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(10f, 5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(NumericExtensions.Between(6f, 5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(4f, 5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(11f, 5f, 10f, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(NumericExtensions.Between(5f, 5f, 10f, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(10f, 5f, 10f, NumericComparisonOptions.None));
            Assert.IsTrue(NumericExtensions.Between(6f, 5f, 10f, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(4f, 5f, 10f, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(11f, 5f, 10f, NumericComparisonOptions.None));


            Assert.IsTrue(NumericExtensions.Between(5, 5, 10));
            Assert.IsTrue(NumericExtensions.Between(10, 5, 10));
            Assert.IsTrue(NumericExtensions.Between(6, 5, 10));
            Assert.IsFalse(NumericExtensions.Between(4, 5, 10));
            Assert.IsFalse(NumericExtensions.Between(11, 5, 10));

            Assert.IsFalse(NumericExtensions.Between(5, 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(10, 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(6, 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(4, 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(11, 5, 10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(NumericExtensions.Between(5, 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(10, 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(NumericExtensions.Between(6, 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(4, 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(11, 5, 10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(NumericExtensions.Between(5, 5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(10, 5, 10, NumericComparisonOptions.None));
            Assert.IsTrue(NumericExtensions.Between(6, 5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(4, 5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(11, 5, 10, NumericComparisonOptions.None));


            Assert.IsTrue(NumericExtensions.Between(5L, 5L, 10L));
            Assert.IsTrue(NumericExtensions.Between(10L, 5L, 10L));
            Assert.IsTrue(NumericExtensions.Between(6L, 5L, 10L));
            Assert.IsFalse(NumericExtensions.Between(4L, 5L, 10L));
            Assert.IsFalse(NumericExtensions.Between(11L, 5L, 10L));

            Assert.IsFalse(NumericExtensions.Between(5L, 5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(10L, 5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between(6L, 5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(4L, 5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between(11L, 5L, 10L, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(NumericExtensions.Between(5L, 5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(10L, 5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(NumericExtensions.Between(6L, 5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(4L, 5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between(11L, 5L, 10L, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(NumericExtensions.Between(5L, 5L, 10L, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(10L, 5L, 10L, NumericComparisonOptions.None));
            Assert.IsTrue(NumericExtensions.Between(6L, 5L, 10L, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(4L, 5L, 10L, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between(11L, 5L, 10L, NumericComparisonOptions.None));


            Assert.IsTrue(NumericExtensions.Between((short)5, (short)5, (short)10));
            Assert.IsTrue(NumericExtensions.Between((short)10, (short)5, (short)10));
            Assert.IsTrue(NumericExtensions.Between((short)6, (short)5, (short)10));
            Assert.IsFalse(NumericExtensions.Between((short)4, (short)5, (short)10));
            Assert.IsFalse(NumericExtensions.Between((short)11, (short)5, (short)10));

            Assert.IsFalse(NumericExtensions.Between((short)5, (short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between((short)10, (short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(NumericExtensions.Between((short)6, (short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between((short)4, (short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(NumericExtensions.Between((short)11, (short)5, (short)10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(NumericExtensions.Between((short)5, (short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between((short)10, (short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(NumericExtensions.Between((short)6, (short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between((short)4, (short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(NumericExtensions.Between((short)11, (short)5, (short)10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(NumericExtensions.Between((short)5, (short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between((short)10, (short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsTrue(NumericExtensions.Between((short)6, (short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between((short)4, (short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsFalse(NumericExtensions.Between((short)11, (short)5, (short)10, NumericComparisonOptions.None));
        }
        [TestMethod]
        public void GreaterThanOrEqualTo()
        {
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo((byte)5, (byte)5));
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo((byte)10, (byte)5));
            Assert.IsFalse(NumericExtensions.GreaterThanOrEqualTo((byte)4, (byte)5));

            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(5m, 5m));
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(10m, 5m));
            Assert.IsFalse(NumericExtensions.GreaterThanOrEqualTo(4m, 5m));

            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(5d, 5d));
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(10d, 5d));
            Assert.IsFalse(NumericExtensions.GreaterThanOrEqualTo(4d, 5d));

            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(5f, 5f));
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(10f, 5f));
            Assert.IsFalse(NumericExtensions.GreaterThanOrEqualTo(4f, 5f));

            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(5, 5));
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(10, 5));
            Assert.IsFalse(NumericExtensions.GreaterThanOrEqualTo(4, 5));

            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(5L, 5L));
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo(10L, 5L));
            Assert.IsFalse(NumericExtensions.GreaterThanOrEqualTo(4L, 5L));

            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo((short)5, (short)5));
            Assert.IsTrue(NumericExtensions.GreaterThanOrEqualTo((short)10, (short)5));
            Assert.IsFalse(NumericExtensions.GreaterThanOrEqualTo((short)4, (short)5));
        }

        [TestMethod]
        public void GreaterThan()
        {
            Assert.IsFalse(NumericExtensions.GreaterThan((byte)5, (byte)5));
            Assert.IsTrue(NumericExtensions.GreaterThan((byte)10, (byte)5));
            Assert.IsFalse(NumericExtensions.GreaterThan((byte)4, (byte)5));

            Assert.IsFalse(NumericExtensions.GreaterThan(5m, 5m));
            Assert.IsTrue(NumericExtensions.GreaterThan(10m, 5m));
            Assert.IsFalse(NumericExtensions.GreaterThan(4m, 5m));

            Assert.IsFalse(NumericExtensions.GreaterThan(5d, 5d));
            Assert.IsTrue(NumericExtensions.GreaterThan(10d, 5d));
            Assert.IsFalse(NumericExtensions.GreaterThan(4d, 5d));

            Assert.IsFalse(NumericExtensions.GreaterThan(5f, 5f));
            Assert.IsTrue(NumericExtensions.GreaterThan(10f, 5f));
            Assert.IsFalse(NumericExtensions.GreaterThan(4f, 5f));

            Assert.IsFalse(NumericExtensions.GreaterThan(5, 5));
            Assert.IsTrue(NumericExtensions.GreaterThan(10, 5));
            Assert.IsFalse(NumericExtensions.GreaterThan(4, 5));

            Assert.IsFalse(NumericExtensions.GreaterThan(5L, 5L));
            Assert.IsTrue(NumericExtensions.GreaterThan(10L, 5L));
            Assert.IsFalse(NumericExtensions.GreaterThan(4L, 5L));

            Assert.IsFalse(NumericExtensions.GreaterThan((short)5, (short)5));
            Assert.IsTrue(NumericExtensions.GreaterThan((short)10, (short)5));
            Assert.IsFalse(NumericExtensions.GreaterThan((short)4, (short)5));
        }

        [TestMethod]
        public void LessThanOrEqualTo()
        {
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo((byte)5, (byte)5));
            Assert.IsFalse(NumericExtensions.LessThanOrEqualTo((byte)10, (byte)5));
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo((byte)4, (byte)5));

            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(5m, 5m));
            Assert.IsFalse(NumericExtensions.LessThanOrEqualTo(10m, 5m));
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(4m, 5m));

            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(5d, 5d));
            Assert.IsFalse(NumericExtensions.LessThanOrEqualTo(10d, 5d));
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(4d, 5d));

            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(5f, 5f));
            Assert.IsFalse(NumericExtensions.LessThanOrEqualTo(10f, 5f));
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(4f, 5f));

            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(5, 5));
            Assert.IsFalse(NumericExtensions.LessThanOrEqualTo(10, 5));
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(4, 5));

            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(5L, 5L));
            Assert.IsFalse(NumericExtensions.LessThanOrEqualTo(10L, 5L));
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo(4L, 5L));

            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo((short)5, (short)5));
            Assert.IsFalse(NumericExtensions.LessThanOrEqualTo((short)10, (short)5));
            Assert.IsTrue(NumericExtensions.LessThanOrEqualTo((short)4, (short)5));
        }

        [TestMethod]
        public void LessThan()
        {
            Assert.IsFalse(NumericExtensions.LessThan((byte)5, (byte)5));
            Assert.IsFalse(NumericExtensions.LessThan((byte)10, (byte)5));
            Assert.IsTrue(NumericExtensions.LessThan((byte)4, (byte)5));

            Assert.IsFalse(NumericExtensions.LessThan(5m, 5m));
            Assert.IsFalse(NumericExtensions.LessThan(10m, 5m));
            Assert.IsTrue(NumericExtensions.LessThan(4m, 5m));

            Assert.IsFalse(NumericExtensions.LessThan(5d, 5d));
            Assert.IsFalse(NumericExtensions.LessThan(10d, 5d));
            Assert.IsTrue(NumericExtensions.LessThan(4d, 5d));

            Assert.IsFalse(NumericExtensions.LessThan(5f, 5f));
            Assert.IsFalse(NumericExtensions.LessThan(10f, 5f));
            Assert.IsTrue(NumericExtensions.LessThan(4f, 5f));

            Assert.IsFalse(NumericExtensions.LessThan(5, 5));
            Assert.IsFalse(NumericExtensions.LessThan(10, 5));
            Assert.IsTrue(NumericExtensions.LessThan(4, 5));

            Assert.IsFalse(NumericExtensions.LessThan(5L, 5L));
            Assert.IsFalse(NumericExtensions.LessThan(10L, 5L));
            Assert.IsTrue(NumericExtensions.LessThan(4L, 5L));

            Assert.IsFalse(NumericExtensions.LessThan((short)5, (short)5));
            Assert.IsFalse(NumericExtensions.LessThan((short)10, (short)5));
            Assert.IsTrue(NumericExtensions.LessThan((short)4, (short)5));
        }


    }
}
