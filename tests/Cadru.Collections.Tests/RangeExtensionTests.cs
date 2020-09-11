//------------------------------------------------------------------------------
// <copyright file="RangeExtensionTests.cs"
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Collections.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RangeExtensionTests
    {
        [TestMethod]
        public void DefaultEnumeration()
        {
            Assert.IsTrue(new[] { 'a', 'b', 'c', 'd', 'e' }.SequenceEqual(new Range<char>('a', 'e').SetDefaultEnumerator()));
            Assert.IsTrue(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<byte>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new short[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<short>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<int>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new long[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<long>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new float[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<float>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new double[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<double>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new decimal[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<decimal>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<uint>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new ulong[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.SequenceEqual(new Range<ulong>(0, 9).SetDefaultEnumerator()));
            Assert.IsTrue(new DateTime[] { new DateTime(2015, 11, 7), new DateTime(2015, 11, 8), new DateTime(2015, 11, 9), new DateTime(2015, 11, 10), new DateTime(2015, 11, 11) }.SequenceEqual(new Range<DateTime>(new DateTime(2015, 11, 7), new DateTime(2015, 11, 11)).SetDefaultEnumerator()));
            Assert.IsTrue(new DateTimeOffset[] { new DateTime(2015, 11, 7), new DateTime(2015, 11, 8), new DateTime(2015, 11, 9), new DateTime(2015, 11, 10), new DateTime(2015, 11, 11) }.SequenceEqual(new Range<DateTimeOffset>(new DateTime(2015, 11, 7), new DateTime(2015, 11, 11)).SetDefaultEnumerator()));


            Assert.AreEqual(5, new Range<char>('a', 'e').SetDefaultEnumerator().Count());
            Assert.AreNotEqual(4, new Range<char>('a', 'e').SetDefaultEnumerator().Count());
            Assert.AreNotEqual(6, new Range<char>('a', 'e').SetDefaultEnumerator().Count());

            Assert.AreEqual(10, new Range<int>(0, 9).SetDefaultEnumerator().Count());
            Assert.AreNotEqual(11, new Range<int>(0, 9).SetDefaultEnumerator().Count());
            Assert.AreNotEqual(9, new Range<int>(0, 9).SetDefaultEnumerator().Count());
        }

        [TestMethod]
        public void SetDefaultEnumerator()
        {
            this.TestHelper(new Range<char>('a', 'e').SetDefaultEnumerator());
            this.TestHelper(new Range<byte>(0, 255).SetDefaultEnumerator());
            this.TestHelper(new Range<short>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<int>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<long>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<float>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<double>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<decimal>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<uint>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<ulong>(0, 9).SetDefaultEnumerator());
            this.TestHelper(new Range<DateTime>(new DateTime(2015, 11, 7), new DateTime(2015, 11, 11)).SetDefaultEnumerator());
            this.TestHelper(new Range<DateTimeOffset>(new DateTime(2015, 11, 7), new DateTime(2015, 11, 11)).SetDefaultEnumerator());
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