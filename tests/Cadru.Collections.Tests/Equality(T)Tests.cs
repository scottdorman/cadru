//------------------------------------------------------------------------------
// <copyright file="Equality(T)Tests.cs"
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
    internal struct EqualityTester
    {
        public readonly DateTime Date;
        public readonly int Index;

        public EqualityTester(int index, DateTime date) : this()
        {
            this.Index = index;
            this.Date = date;
        }
    }

    [TestClass, ExcludeFromCodeCoverage]
    public class EqualityOfTTests
    {
        [TestMethod]
        public void Create()
        {
            var comparer = Equality<DateTime>.CreateComparer(x => x.Day);
            Assert.IsNotNull(comparer);
            Assert.IsInstanceOfType(comparer, typeof(IEqualityComparer<DateTime>));
        }

        [TestMethod]
        public void DistinctByTest()
        {
            var list = Enumerable.Range(0, 200).Select(i => new EqualityTester(i, DateTime.Today.AddDays(i % 4))).ToList();

            var distinctList = list.Distinct(Equality<EqualityTester>.CreateComparer(x => x.Date)).ToList();
            Assert.AreEqual(4, distinctList.Count);

            Assert.AreEqual(0, distinctList[0].Index);
            Assert.AreEqual(1, distinctList[1].Index);
            Assert.AreEqual(2, distinctList[2].Index);
            Assert.AreEqual(3, distinctList[3].Index);

            Assert.AreEqual(DateTime.Today, distinctList[0].Date);
            Assert.AreEqual(DateTime.Today.AddDays(1), distinctList[1].Date);
            Assert.AreEqual(DateTime.Today.AddDays(2), distinctList[2].Date);
            Assert.AreEqual(DateTime.Today.AddDays(3), distinctList[3].Date);

            Assert.AreEqual(200, list.Count);
        }

        [TestMethod]
        public void DistinctByWithComparerTest()
        {
            var list = Enumerable.Range(0, 200).Select(i => new EqualityTester(i, DateTime.Today.AddDays(i % 4))).ToList();

            var distinctList = list.Distinct(Equality<EqualityTester>.CreateComparer(x => x.Date, EqualityComparer<DateTime>.Default)).ToList();
            Assert.AreEqual(4, distinctList.Count);

            Assert.AreEqual(0, distinctList[0].Index);
            Assert.AreEqual(1, distinctList[1].Index);
            Assert.AreEqual(2, distinctList[2].Index);
            Assert.AreEqual(3, distinctList[3].Index);

            Assert.AreEqual(DateTime.Today, distinctList[0].Date);
            Assert.AreEqual(DateTime.Today.AddDays(1), distinctList[1].Date);
            Assert.AreEqual(DateTime.Today.AddDays(2), distinctList[2].Date);
            Assert.AreEqual(DateTime.Today.AddDays(3), distinctList[3].Date);

            Assert.AreEqual(200, list.Count);
        }
    }
}