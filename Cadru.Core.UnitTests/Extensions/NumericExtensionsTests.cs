using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Text;
using System.Diagnostics.CodeAnalysis;
using Cadru.Extensions;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class Tests
    {
        [TestMethod]
        public void Between()
        {
            Assert.IsTrue(((byte)5).Between((byte)5, (byte)10));
            Assert.IsTrue(((byte)10).Between((byte)5, (byte)10));
            Assert.IsTrue(((byte)6).Between((byte)5, (byte)10));
            Assert.IsFalse(((byte)4).Between((byte)5, (byte)10));
            Assert.IsFalse(((byte)11).Between((byte)5, (byte)10));

            Assert.IsFalse(((byte)5).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(((byte)10).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(((byte)6).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(((byte)4).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(((byte)1).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(((byte)5).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(((byte)10).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(((byte)6).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(((byte)4).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(((byte)1).Between((byte)5, (byte)10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(((byte)5).Between((byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsFalse(((byte)10).Between((byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsTrue(((byte)6).Between((byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsFalse(((byte)4).Between((byte)5, (byte)10, NumericComparisonOptions.None));
            Assert.IsFalse(((byte)11).Between((byte)5, (byte)10, NumericComparisonOptions.None));

            Assert.IsTrue(5m.Between(5m, 10m));
            Assert.IsTrue(10m.Between(5m, 10m));
            Assert.IsTrue(6m.Between(5m, 10m));
            Assert.IsFalse(4m.Between(5m, 10m));
            Assert.IsFalse(11m.Between(5m, 10m));

            Assert.IsFalse(5m.Between(5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(10m.Between(5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(6m.Between(5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(4m.Between(5m, 10m, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(11m.Between(5m, 10m, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(5m.Between(5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(10m.Between(5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(6m.Between(5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(4m.Between(5m, 10m, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(11m.Between(5m, 10m, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(5m.Between(5m, 10m, NumericComparisonOptions.None));
            Assert.IsFalse(10m.Between(5m, 10m, NumericComparisonOptions.None));
            Assert.IsTrue(6m.Between(5m, 10m, NumericComparisonOptions.None));
            Assert.IsFalse(4m.Between(5m, 10m, NumericComparisonOptions.None));
            Assert.IsFalse(11m.Between(5m, 10m, NumericComparisonOptions.None));

            Assert.IsTrue(5d.Between(5d, 10d));
            Assert.IsTrue(10d.Between(5d, 10d));
            Assert.IsTrue(6d.Between(5d, 10d));
            Assert.IsFalse(4d.Between(5d, 10d));
            Assert.IsFalse(11d.Between(5d, 10d));

            Assert.IsFalse(5d.Between(5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(10d.Between(5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(6d.Between(5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(4d.Between(5d, 10d, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(11d.Between(5d, 10d, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(5d.Between(5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(10d.Between(5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(6d.Between(5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(4d.Between(5d, 10d, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(11d.Between(5d, 10d, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(5d.Between(5d, 10d, NumericComparisonOptions.None));
            Assert.IsFalse(10d.Between(5d, 10d, NumericComparisonOptions.None));
            Assert.IsTrue(6d.Between(5d, 10d, NumericComparisonOptions.None));
            Assert.IsFalse(4d.Between(5d, 10d, NumericComparisonOptions.None));
            Assert.IsFalse(11d.Between(5d, 10d, NumericComparisonOptions.None));

            Assert.IsTrue(5f.Between(5f, 10f));
            Assert.IsTrue(10f.Between(5f, 10f));
            Assert.IsTrue(6f.Between(5f, 10f));
            Assert.IsFalse(4f.Between(5f, 10f));
            Assert.IsFalse(11f.Between(5f, 10f));

            Assert.IsFalse(5f.Between(5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(10f.Between(5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(6f.Between(5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(4f.Between(5f, 10f, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(11f.Between(5f, 10f, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(5f.Between(5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(10f.Between(5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(6f.Between(5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(4f.Between(5f, 10f, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(11f.Between(5f, 10f, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(5f.Between(5f, 10f, NumericComparisonOptions.None));
            Assert.IsFalse(10f.Between(5f, 10f, NumericComparisonOptions.None));
            Assert.IsTrue(6f.Between(5f, 10f, NumericComparisonOptions.None));
            Assert.IsFalse(4f.Between(5f, 10f, NumericComparisonOptions.None));
            Assert.IsFalse(11f.Between(5f, 10f, NumericComparisonOptions.None));

            Assert.IsTrue(5.Between(5, 10));
            Assert.IsTrue(10.Between(5, 10));
            Assert.IsTrue(6.Between(5, 10));
            Assert.IsFalse(4.Between(5, 10));
            Assert.IsFalse(11.Between(5, 10));

            Assert.IsFalse(5.Between(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(10.Between(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(6.Between(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(4.Between(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(11.Between(5, 10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(5.Between(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(10.Between(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(6.Between(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(4.Between(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(11.Between(5, 10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(5.Between(5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(10.Between(5, 10, NumericComparisonOptions.None));
            Assert.IsTrue(6.Between(5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(4.Between(5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(11.Between(5, 10, NumericComparisonOptions.None));

            Assert.IsTrue(5L.Between(5L, 10L));
            Assert.IsTrue(10L.Between(5L, 10L));
            Assert.IsTrue(6L.Between(5L, 10L));
            Assert.IsFalse(4L.Between(5L, 10L));
            Assert.IsFalse(11L.Between(5L, 10L));

            Assert.IsFalse(5L.Between(5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(10L.Between(5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(6L.Between(5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(4L.Between(5L, 10L, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(11L.Between(5L, 10L, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(5L.Between(5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(10L.Between(5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(6L.Between(5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(4L.Between(5L, 10L, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(11L.Between(5L, 10L, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(5L.Between(5L, 10L, NumericComparisonOptions.None));
            Assert.IsFalse(10L.Between(5L, 10L, NumericComparisonOptions.None));
            Assert.IsTrue(6L.Between(5L, 10L, NumericComparisonOptions.None));
            Assert.IsFalse(4L.Between(5L, 10L, NumericComparisonOptions.None));
            Assert.IsFalse(11L.Between(5L, 10L, NumericComparisonOptions.None));

            Assert.IsTrue(((short)5).Between((short)5, (short)10));
            Assert.IsTrue(((short)10).Between((short)5, (short)10));
            Assert.IsTrue(((short)6).Between((short)5, (short)10));
            Assert.IsFalse(((short)4).Between((short)5, (short)10));
            Assert.IsFalse(((short)11).Between((short)5, (short)10));

            Assert.IsFalse(((short)5).Between((short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(((short)10).Between((short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(((short)6).Between((short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(((short)4).Between((short)5, (short)10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(((short)11).Between((short)5, (short)10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(((short)5).Between((short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(((short)10).Between((short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(((short)6).Between((short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(((short)4).Between((short)5, (short)10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(((short)11).Between((short)5, (short)10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(((short)5).Between((short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsFalse(((short)10).Between((short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsTrue(((short)6).Between((short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsFalse(((short)4).Between((short)5, (short)10, NumericComparisonOptions.None));
            Assert.IsFalse(((short)11).Between((short)5, (short)10, NumericComparisonOptions.None));
        }

        [TestMethod]
        public void GreaterThanOrEqualTo()
        {
            Assert.IsTrue(((byte)5).GreaterThanOrEqualTo((byte)5));
            Assert.IsTrue(((byte)10).GreaterThanOrEqualTo((byte)5));
            Assert.IsFalse(((byte)4).GreaterThanOrEqualTo((byte)5));

            Assert.IsTrue(5m.GreaterThanOrEqualTo(5m));
            Assert.IsTrue(10m.GreaterThanOrEqualTo(5m));
            Assert.IsFalse(4m.GreaterThanOrEqualTo(5m));

            Assert.IsTrue(5d.GreaterThanOrEqualTo(5d));
            Assert.IsTrue(10d.GreaterThanOrEqualTo(5d));
            Assert.IsFalse(4d.GreaterThanOrEqualTo(5d));

            Assert.IsTrue(5f.GreaterThanOrEqualTo(5f));
            Assert.IsTrue(10f.GreaterThanOrEqualTo(5f));
            Assert.IsFalse(4f.GreaterThanOrEqualTo(5f));

            Assert.IsTrue(5.GreaterThanOrEqualTo(5));
            Assert.IsTrue(10.GreaterThanOrEqualTo(5));
            Assert.IsFalse(4.GreaterThanOrEqualTo(5));

            Assert.IsTrue(5L.GreaterThanOrEqualTo(5L));
            Assert.IsTrue(10L.GreaterThanOrEqualTo(5L));
            Assert.IsFalse(4L.GreaterThanOrEqualTo(5L));

            Assert.IsTrue(((short)5).GreaterThanOrEqualTo((short)5));
            Assert.IsTrue(((short)10).GreaterThanOrEqualTo((short)5));
            Assert.IsFalse(((short)4).GreaterThanOrEqualTo((short)5));
        }

        [TestMethod]
        public void GreaterThan()
        {
            Assert.IsFalse(((byte)5).GreaterThan((byte)5));
            Assert.IsTrue(((byte)10).GreaterThan((byte)5));
            Assert.IsFalse(((byte)4).GreaterThan((byte)5));

            Assert.IsFalse(5m.GreaterThan(5m));
            Assert.IsTrue(10m.GreaterThan(5m));
            Assert.IsFalse(4m.GreaterThan(5m));

            Assert.IsFalse(5d.GreaterThan(5d));
            Assert.IsTrue(10d.GreaterThan(5d));
            Assert.IsFalse(4d.GreaterThan(5d));

            Assert.IsFalse(5f.GreaterThan(5f));
            Assert.IsTrue(10f.GreaterThan(5f));
            Assert.IsFalse(4f.GreaterThan(5f));

            Assert.IsFalse(5.GreaterThan(5));
            Assert.IsTrue(10.GreaterThan(5));
            Assert.IsFalse(4.GreaterThan(5));

            Assert.IsFalse(5L.GreaterThan(5L));
            Assert.IsTrue(10L.GreaterThan(5L));
            Assert.IsFalse(4L.GreaterThan(5L));

            Assert.IsFalse(((short)5).GreaterThan((short)5));
            Assert.IsTrue(((short)10).GreaterThan((short)5));
            Assert.IsFalse(((short)4).GreaterThan((short)5));
        }

        [TestMethod]
        public void LessThanOrEqualTo()
        {
            Assert.IsTrue(((byte)5).LessThanOrEqualTo((byte)5));
            Assert.IsFalse(((byte)10).LessThanOrEqualTo((byte)5));
            Assert.IsTrue(((byte)4).LessThanOrEqualTo((byte)5));

            Assert.IsTrue(5m.LessThanOrEqualTo(5m));
            Assert.IsFalse(10m.LessThanOrEqualTo(5m));
            Assert.IsTrue(4m.LessThanOrEqualTo(5m));

            Assert.IsTrue(5d.LessThanOrEqualTo(5d));
            Assert.IsFalse(10d.LessThanOrEqualTo(5d));
            Assert.IsTrue(4d.LessThanOrEqualTo(5d));

            Assert.IsTrue(5f.LessThanOrEqualTo(5f));
            Assert.IsFalse(10f.LessThanOrEqualTo(5f));
            Assert.IsTrue(4f.LessThanOrEqualTo(5f));

            Assert.IsTrue(5.LessThanOrEqualTo(5));
            Assert.IsFalse(10.LessThanOrEqualTo(5));
            Assert.IsTrue(4.LessThanOrEqualTo(5));

            Assert.IsTrue(5L.LessThanOrEqualTo(5L));
            Assert.IsFalse(10L.LessThanOrEqualTo(5L));
            Assert.IsTrue(4L.LessThanOrEqualTo(5L));

            Assert.IsTrue(((short)5).LessThanOrEqualTo((short)5));
            Assert.IsFalse(((short)10).LessThanOrEqualTo((short)5));
            Assert.IsTrue(((short)4).LessThanOrEqualTo((short)5));
        }

        [TestMethod]
        public void LessThan()
        {
            Assert.IsFalse(((byte)5).LessThan((byte)5));
            Assert.IsFalse(((byte)10).LessThan((byte)5));
            Assert.IsTrue(((byte)4).LessThan((byte)5));

            Assert.IsFalse(5m.LessThan(5m));
            Assert.IsFalse(10m.LessThan(5m));
            Assert.IsTrue(4m.LessThan(5m));

            Assert.IsFalse(5d.LessThan(5d));
            Assert.IsFalse(10d.LessThan(5d));
            Assert.IsTrue(4d.LessThan(5d));

            Assert.IsFalse(5f.LessThan(5f));
            Assert.IsFalse(10f.LessThan(5f));
            Assert.IsTrue(4f.LessThan(5f));

            Assert.IsFalse(5.LessThan(5));
            Assert.IsFalse(10.LessThan(5));
            Assert.IsTrue(4.LessThan(5));

            Assert.IsFalse(5L.LessThan(5L));
            Assert.IsFalse(10L.LessThan(5L));
            Assert.IsTrue(4L.LessThan(5L));

            Assert.IsFalse(((short)5).LessThan((short)5));
            Assert.IsFalse(((short)10).LessThan((short)5));
            Assert.IsTrue(((short)4).LessThan((short)5));
        }

        [TestMethod]
        public void IsEven()
        {
            Assert.IsTrue(((decimal)4).IsEven());
            Assert.IsTrue(((double)4).IsEven());
            Assert.IsTrue(((float)4).IsEven());
            Assert.IsTrue(((int)4).IsEven());
            Assert.IsTrue(((long)4).IsEven());
            Assert.IsTrue(((short)4).IsEven());

            Assert.IsFalse(((decimal)3).IsEven());
            Assert.IsFalse(((double)3).IsEven());
            Assert.IsFalse(((float)3).IsEven());
            Assert.IsFalse(((int)3).IsEven());
            Assert.IsFalse(((long)3).IsEven());
            Assert.IsFalse(((short)3).IsEven());
        }

        [TestMethod]
        public void IsOdd()
        {
            Assert.IsFalse(((decimal)4).IsOdd());
            Assert.IsFalse(((double)4).IsOdd());
            Assert.IsFalse(((float)4).IsOdd());
            Assert.IsFalse(((int)4).IsOdd());
            Assert.IsFalse(((long)4).IsOdd());
            Assert.IsFalse(((short)4).IsOdd());

            Assert.IsTrue(((decimal)3).IsOdd());
            Assert.IsTrue(((double)3).IsOdd());
            Assert.IsTrue(((float)3).IsOdd());
            Assert.IsTrue(((int)3).IsOdd());
            Assert.IsTrue(((long)3).IsOdd());
            Assert.IsTrue(((short)3).IsOdd());
        }
    }
}