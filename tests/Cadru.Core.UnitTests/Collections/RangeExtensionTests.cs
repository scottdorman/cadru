using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cadru.Collections;
using System.Net;
using System.Diagnostics.CodeAnalysis;
using Cadru.UnitTest.Framework;
using System.Linq;

namespace Cadru.UnitTest.Framework.UnitTests.Collections
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RangeExtensionTests
    {
        [TestMethod]
        public void SetDefaultEnumerator()
        {
            TestHelper(new Range<char>('a', 'e').SetDefaultEnumerator());
            TestHelper(new Range<byte>(0, 255).SetDefaultEnumerator());
            TestHelper(new Range<int>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<short>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<int>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<long>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<float>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<double>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<decimal>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<uint>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<ulong>(0, 9).SetDefaultEnumerator());
            TestHelper(new Range<DateTime>(new DateTime(2015, 11, 7), new DateTime(2015, 11, 11)).SetDefaultEnumerator());
            TestHelper(new Range<DateTimeOffset>(new DateTime(2015, 11, 7), new DateTime(2015, 11, 11)).SetDefaultEnumerator());
        }

        private void TestHelper<T>(Range<T> range)
        {
            var enumerator = range.GetEnumerator();
            Assert.IsNotNull(enumerator);

            var type = enumerator.GetType();
            Assert.IsTrue(type.DeclaringType == typeof(RangeIterator<>));
            Assert.IsTrue(type.GenericTypeArguments[0] == typeof(T));
        }
    }
}
