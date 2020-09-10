//------------------------------------------------------------------------------
// <copyright file="TypeExtensionsTests.cs"
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Cadru.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void HasInterface()
        {
            Assert.IsFalse(typeof(string).HasInterface<IDisposable>());
            Assert.IsTrue(typeof(StringComparer).HasInterface<IComparer>());
            Assert.IsTrue(typeof(StringComparer).HasInterface<IEqualityComparer<string>>());
        }

        [TestMethod]
        public void IsDate()
        {
            Assert.IsFalse(typeof(string).IsDate());
            Assert.IsTrue(typeof(DateTime).IsDate());
            Assert.IsTrue(typeof(DateTime?).IsDate());
        }

        [TestMethod]
        public void IsNullable()
        {
            Assert.IsTrue(typeof(bool?).IsNullable());
            Assert.IsFalse(typeof(bool).IsNullable());
        }

        [TestMethod]
        public void IsNumeric()
        {
            Assert.IsFalse(typeof(string).IsNumeric());
            Assert.IsTrue(typeof(int).IsNumeric());
            Assert.IsTrue(typeof(int?).IsNumeric());
        }
    }
}