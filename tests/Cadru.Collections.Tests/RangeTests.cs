//------------------------------------------------------------------------------
// <copyright file="RangeTests.cs"
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Collections.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class RangeTests
    {
        [TestMethod]
        public void AvoidWrapAround()
        {
            // Every byte value is valid, so we'll wrap on overflow.
            var range = new Range<byte>(0, 255, RangeEndpointOption.Open);
            range.SetDefaultEnumerator();
            var expected = Enumerable.Range(0, 256).Select(x => (byte)x);
            Assert.AreEqual(256, range.Take(300).Count());
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void ClosedCharRange()
        {
            var range = new Range<char>('a', 'e', RangeEndpointOption.Closed);
            Assert.IsFalse(range.Contains('x'));
            Assert.IsFalse(range.Contains('a'));
            Assert.IsTrue(range.Contains('b'));
            Assert.IsFalse(range.Contains('e'));
        }

        [TestMethod]
        public void ClosedCharRangeSimpleIteration()
        {
            var range = new Range<char>('a', 'e', RangeEndpointOption.Closed);
            range.SetDefaultEnumerator();
            //range.SetEnumerator(RangeEnumerator.CreateDefault(range));
            var expected = new[] { 'b', 'c', 'd' };
            range.ToList();
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void ClosedDateRangeSimpleIteration()
        {
            var range = new Range<DateTime>(new DateTime(2015, 11, 7), new DateTime(2015, 11, 11), RangeEndpointOption.Closed);
            range.SetDefaultEnumerator();
            var expected = new[] { new DateTime(2015, 11, 8), new DateTime(2015, 11, 9), new DateTime(2015, 11, 10) };
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void ClosedRange()
        {
            var range = new Range<int>(5, 10, RangeEndpointOption.Closed);
            Assert.IsFalse(range.Contains(4));
            Assert.IsFalse(range.Contains(5));
            Assert.IsTrue(range.Contains(6));
            Assert.IsFalse(range.Contains(10));
            Assert.IsFalse(range.Contains(11));
        }

        [TestMethod]
        public void ClosedRangeIntersection()
        {
            var range = new Range<int>(-10, 6, RangeEndpointOption.Closed).Intersect(new Range<int>(1, 10, RangeEndpointOption.Closed));
            Assert.AreEqual("(1, 6)", range.ToString());
        }

        [TestMethod]
        public void ClosedRangeSimpleIteration()
        {
            var range = new Range<int>(0, 5, RangeEndpointOption.Closed);
            range.SetDefaultEnumerator();
            var expected = new[] { 1, 2, 3, 4 };
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void ClosedRangeUnion()
        {
            var range = new Range<int>(3, 9, RangeEndpointOption.Closed).Union(new Range<int>(7, 11, RangeEndpointOption.Closed));
            Assert.AreEqual("(3, 11)", range.ToString());
        }

        [TestMethod]
        public void ContainsRange()
        {
            Assert.IsTrue(new Range<int>(0, 9).Contains(new Range<int>(0, 9)));
            Assert.IsFalse(new Range<int>(0, 9).Contains(new Range<int>(0, 10)));
            Assert.IsFalse(new Range<int>(0, 9).Contains(new Range<int>(-1, 9)));
            Assert.IsFalse(new Range<int>(0, 9).Contains(new Range<int>(-1, 10)));
            Assert.IsTrue(new Range<int>(0, 10).Contains(new Range<int>(0, 9)));
            Assert.IsTrue(new Range<int>(-1, 9).Contains(new Range<int>(0, 9)));
            Assert.IsTrue(new Range<int>(-1, 10).Contains(new Range<int>(0, 9)));
        }

        [TestMethod]
        public void CustomComparer()
        {
            // Should contain any number with a final digit of 3, 4, 5 or 6
            var range = new Range<int>(13, 7, new LastDigitComparer(), RangeEndpointOption.LeftHalfOpen);
            Assert.IsTrue(range.Contains(45));
            Assert.IsTrue(range.Contains(23));
            Assert.IsFalse(range.Contains(37));
        }

        [TestMethod]
        public void HandleNoOpSteppingFunction()
        {
            var range = new Range<int>(0, 5);
            range.SetEnumerator(new RangeIterator<int>(range, x => x));
            var expected = new[] { 0 };
            var actual = range.ToArray();

            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void InclusiveLowerExclusiveUpperWithOvershoot()
        {
            var range = new Range<int>(0, 5);
            range.SetEnumerator(new RangeIterator<int>(range, x => x + 2));
            var expected = new[] { 0, 2, 4 };
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void IsContainedBy()
        {
            Assert.IsFalse(new Range<int>(0, 9).IsContainedBy(new Range<int>(-10, 1)));
            Assert.IsFalse(new Range<int>(0, 9).IsContainedBy(new Range<int>(-10, 0)));
            Assert.IsFalse(new Range<int>(0, 9).IsContainedBy(new Range<int>(-10, -1)));

            Assert.IsTrue(new Range<int>(-10, 1).IsContainedBy(new Range<int>(-10, 1)));
            Assert.IsTrue(new Range<int>(0, 1).IsContainedBy(new Range<int>(-10, 1)));
            Assert.IsFalse(new Range<int>(-10, -1).IsContainedBy(new Range<int>(0, 1)));
        }

        [TestMethod]
        public void IsContiguousWith()
        {
            Assert.IsTrue(new Range<int>(0, 9).IsContiguousWith(new Range<int>(-1, 10)));
            Assert.IsTrue(new Range<int>(0, 9).IsContiguousWith(new Range<int>(5, 10)));
            Assert.IsTrue(new Range<int>(0, 9).IsContiguousWith(new Range<int>(5, 6)));
            Assert.IsTrue(new Range<int>(0, 9).IsContiguousWith(new Range<int>(9, 10)));
            Assert.IsTrue(new Range<int>(0, 9).IsContiguousWith(new Range<int>(9, 10)));
            Assert.IsTrue(new Range<int>(0, 9).IsContiguousWith(new Range<int>(-3, 0)));
            Assert.IsTrue(new Range<int>(0, 9).IsContiguousWith(new Range<int>(-3, 0)));

            Assert.IsFalse(new Range<int>(0, 9).IsContiguousWith(new Range<int>(10, 11)));
            Assert.IsFalse(new Range<int>(0, 9).IsContiguousWith(new Range<int>(-3, -1)));
        }

        [TestMethod]
        public void LeftHalfOpenRange()
        {
            var range = new Range<int>(5, 10, RangeEndpointOption.LeftHalfOpen);
            Assert.IsFalse(range.Contains(4));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(6));
            Assert.IsFalse(range.Contains(10));
            Assert.IsFalse(range.Contains(11));
        }

        [TestMethod]
        public void LeftHalfOpenRangeIntersection()
        {
            var range = new Range<int>(-10, 6, RangeEndpointOption.LeftHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 6)", range.ToString());
        }

        [TestMethod]
        public void LeftHalfOpenRangeSimpleIteration()
        {
            var range = new Range<int>(0, 5, RangeEndpointOption.LeftHalfOpen);
            range.SetDefaultEnumerator();
            var expected = new[] { 0, 1, 2, 3, 4 };
            //var actual = range.ToArray();
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void LeftHalfOpenRangeUnion()
        {
            var range = new Range<int>(3, 9, RangeEndpointOption.LeftHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[3, 11)", range.ToString());
        }

        [TestMethod]
        public void MixedRangeIntersection()
        {
            var range = new Range<int>(-10, 6, RangeEndpointOption.Open).Intersect(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 6)", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.Open).Intersect(new Range<int>(1, 10, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("[1, 6]", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.Open).Intersect(new Range<int>(1, 10, RangeEndpointOption.Closed));
            Assert.AreEqual("[1, 6)", range.ToString());

            range = new Range<int>(-10, 6, RangeEndpointOption.Closed).Intersect(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("(1, 6)", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.Closed).Intersect(new Range<int>(1, 10, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("(1, 6]", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.Closed).Intersect(new Range<int>(1, 10, RangeEndpointOption.Open));
            Assert.AreEqual("(1, 6]", range.ToString());

            range = new Range<int>(-10, 6, RangeEndpointOption.LeftHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.Closed));
            Assert.AreEqual("[1, 6)", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.LeftHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.Open));
            Assert.AreEqual("[1, 6]", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.LeftHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("[1, 6]", range.ToString());

            range = new Range<int>(-10, 6, RangeEndpointOption.RightHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.Closed));
            Assert.AreEqual("(1, 6)", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.RightHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.Open));
            Assert.AreEqual("(1, 6]", range.ToString());
            range = new Range<int>(-10, 6, RangeEndpointOption.RightHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("(1, 6)", range.ToString());

            range = new Range<int>(1, 6, RangeEndpointOption.Open).Intersect(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 6)", range.ToString());

            range = new Range<int>(1, 6, RangeEndpointOption.Open).Intersect(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 6)", range.ToString());
            range = new Range<int>(1, 6, RangeEndpointOption.Open).Intersect(new Range<int>(-10, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 6)", range.ToString());

            range = new Range<int>(1, 12, RangeEndpointOption.Open).Intersect(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 10)", range.ToString());
            range = new Range<int>(1, 12, RangeEndpointOption.Open).Intersect(new Range<int>(-10, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 10)", range.ToString());
        }

        [TestMethod]
        public void MixedRangeUnion()
        {
            var range = new Range<int>(3, 9, RangeEndpointOption.Open).Union(new Range<int>(7, 11, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[3, 11)", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.Open).Union(new Range<int>(7, 11, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("[3, 11]", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.Open).Union(new Range<int>(7, 11, RangeEndpointOption.Closed));
            Assert.AreEqual("[3, 11)", range.ToString());

            range = new Range<int>(3, 9, RangeEndpointOption.Closed).Union(new Range<int>(7, 11, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("(3, 11)", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.Closed).Union(new Range<int>(7, 11, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("(3, 11]", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.Closed).Union(new Range<int>(7, 11, RangeEndpointOption.Open));
            Assert.AreEqual("(3, 11]", range.ToString());

            range = new Range<int>(3, 9, RangeEndpointOption.LeftHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.Closed));
            Assert.AreEqual("[3, 11)", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.LeftHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.Open));
            Assert.AreEqual("[3, 11]", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.LeftHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("[3, 11]", range.ToString());

            range = new Range<int>(3, 9, RangeEndpointOption.RightHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.Closed));
            Assert.AreEqual("(3, 11)", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.RightHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.Open));
            Assert.AreEqual("(3, 11]", range.ToString());
            range = new Range<int>(3, 9, RangeEndpointOption.RightHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("(3, 11)", range.ToString());

            range = new Range<int>(3, 9, RangeEndpointOption.RightHalfOpen).Union(new Range<int>(4, 8, RangeEndpointOption.Closed));
            Assert.AreEqual("(3, 9]", range.ToString());

            range = new Range<int>(3, 9, RangeEndpointOption.RightHalfOpen).Union(new Range<int>(1, 10, RangeEndpointOption.Closed));
            Assert.AreEqual("(1, 10)", range.ToString());

            range = new Range<int>(1, 6, RangeEndpointOption.Open).Union(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 10)", range.ToString());
            range = new Range<int>(1, 6, RangeEndpointOption.Open).Union(new Range<int>(-10, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[-10, 10)", range.ToString());

            range = new Range<int>(1, 12, RangeEndpointOption.Open).Union(new Range<int>(1, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[1, 12]", range.ToString());
            range = new Range<int>(1, 12, RangeEndpointOption.Open).Union(new Range<int>(-10, 10, RangeEndpointOption.LeftHalfOpen));
            Assert.AreEqual("[-10, 12)", range.ToString());
        }

        [TestMethod]
        public void OpenCharRangeSimpleIteration()
        {
            var range = new Range<char>('a', 'e', RangeEndpointOption.Open);
            range.SetDefaultEnumerator();
            var expected = new[] { 'a', 'b', 'c', 'd', 'e' };
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void OpenRange()
        {
            var range = new Range<int>(5, 10, RangeEndpointOption.Open);
            Assert.IsFalse(range.Contains(4));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(6));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(11));

            range = new Range<int>(5, 10);
            Assert.IsFalse(range.Contains(4));
            Assert.IsTrue(range.Contains(5));
            Assert.IsTrue(range.Contains(6));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(11));
        }

        [TestMethod]
        public void OpenRangeIntersection()
        {
            var range = new Range<int>(-10, 6).Intersect(new Range<int>(1, 10));
            Assert.AreEqual("[1, 6]", range.ToString());

            range = new Range<int>(-10, 6, RangeEndpointOption.Open).Intersect(new Range<int>(1, 10, RangeEndpointOption.Open));
            Assert.AreEqual("[1, 6]", range.ToString());
        }

        [TestMethod]
        public void OpenRangeSimpleIteration()
        {
            var range = new Range<int>(0, 5, RangeEndpointOption.Open);
            range.SetDefaultEnumerator();
            var expected = new[] { 0, 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.SequenceEqual(range));

            range = new Range<int>(0, 5);
            range.SetDefaultEnumerator();
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void OpenRangeUnion()
        {
            var range = new Range<int>(3, 9).Union(new Range<int>(7, 11));
            Assert.AreEqual("[3, 11]", range.ToString());

            range = new Range<int>(3, 9, RangeEndpointOption.Open).Union(new Range<int>(7, 11, RangeEndpointOption.Open));
            Assert.AreEqual("[3, 11]", range.ToString());
        }

        [TestMethod]
        public void Overlaps()
        {
            Assert.IsTrue(new Range<int>(0, 9).Overlaps(new Range<int>(-10, 1)));
            Assert.IsTrue(new Range<int>(0, 9).Overlaps(new Range<int>(-10, 0)));
            Assert.IsFalse(new Range<int>(0, 9).Overlaps(new Range<int>(-10, -1)));

            Assert.IsTrue(new Range<int>(-10, 1).Overlaps(new Range<int>(0, 1)));
            Assert.IsTrue(new Range<int>(-10, 1).Overlaps(new Range<int>(0, 1)));
            Assert.IsFalse(new Range<int>(-10, -1).Overlaps(new Range<int>(0, 1)));
        }

        [TestMethod]
        public void RightHalfOpenRange()
        {
            var range = new Range<int>(5, 10, RangeEndpointOption.RightHalfOpen);
            Assert.IsFalse(range.Contains(4));
            Assert.IsFalse(range.Contains(5));
            Assert.IsTrue(range.Contains(6));
            Assert.IsTrue(range.Contains(10));
            Assert.IsFalse(range.Contains(11));
        }

        [TestMethod]
        public void RightHalfOpenRangeIntersection()
        {
            var range = new Range<int>(-10, 6, RangeEndpointOption.RightHalfOpen).Intersect(new Range<int>(1, 10, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("(1, 6]", range.ToString());
        }

        [TestMethod]
        public void RightHalfOpenRangeSimpleIteration()
        {
            var range = new Range<int>(0, 5, RangeEndpointOption.RightHalfOpen);
            range.SetDefaultEnumerator();
            var expected = new[] { 1, 2, 3, 4, 5 };
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void RightHalfOpenRangeUnion()
        {
            var range = new Range<int>(3, 9, RangeEndpointOption.RightHalfOpen).Union(new Range<int>(7, 11, RangeEndpointOption.RightHalfOpen));
            Assert.AreEqual("(3, 11]", range.ToString());
        }

        [TestMethod]
        public void SteppingWithCustomComparer()
        {
            var range = new Range<int>(33, 29, new LastDigitComparer(), RangeEndpointOption.Open);
            range.SetEnumerator(new RangeIterator<int>(range, x => x + 2));
            var expected = new[] { 33, 35, 37, 39 };
            var actual = range.ToArray();
            Assert.IsTrue(expected.SequenceEqual(range));
        }

        [TestMethod]
        public void ToStringTests()
        {
            Assert.AreEqual("(1, 6)", new Range<int>(1, 6, RangeEndpointOption.Closed).ToString());
            Assert.AreEqual("[1, 6]", new Range<int>(1, 6, RangeEndpointOption.Open).ToString());
            Assert.AreEqual("[1, 6)", new Range<int>(1, 6, RangeEndpointOption.LeftHalfOpen).ToString());
            Assert.AreEqual("(1, 6]", new Range<int>(1, 6, RangeEndpointOption.RightHalfOpen).ToString());
        }

        /// <summary>
        /// Comparer which just compares the last digit of each operand
        /// (but doesn't try to handle negative numbers intelligently).
        /// </summary>
        private sealed class LastDigitComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return (x % 10).CompareTo(y % 10);
            }
        }
    }
}