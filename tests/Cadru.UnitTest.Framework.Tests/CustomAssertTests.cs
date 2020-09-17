//------------------------------------------------------------------------------
// <copyright file="CustomAssertTests.cs"
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
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.UnitTest.Framework.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CustomAssertTests
    {
        [TestMethod]
        public void CaseInsensitiveCompare()
        {
            Assert.That.AreEqualIgnoringCase("name", "NAME");
            Assert.That.AreEqualIgnoringCase("name", "NAME", "test message");
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void CaseInsensitiveCompareFails()
        {
            Assert.That.AreEqualIgnoringCase("Name", "NAMES");
            Assert.That.AreEqualIgnoringCase("Name", "NAMES", "test message");
        }

        [TestMethod]
        public void IsEmpty()
        {
            Assert.That.IsEmpty("", "Failed on empty String");
            CollectionAssert.That.IsEmpty(Array.Empty<int>(), "Failed on empty Array");
            CollectionAssert.That.IsEmpty(new ArrayList(), "Failed on empty ArrayList");
            CollectionAssert.That.IsEmpty(new Hashtable(), "Failed on empty Hashtable");
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsEmptyFailsOnNonEmptyArray()
        {
            CollectionAssert.That.IsEmpty(new int[] { 1, 2, 3 });
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsEmptyFailsOnNullString()
        {
            Assert.That.IsEmpty((string)null);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsEmptyFailsOnString()
        {
            Assert.That.IsEmpty("Hi!");
        }

        [TestMethod]
        public void IsNotEmpty()
        {
            var array = new int[] { 1, 2, 3 };
            var list = new ArrayList(array);
            var hash = new Hashtable
            {
                { "array", array }
            };

            Assert.That.IsNotEmpty("Hi!", "Failed on String");
            CollectionAssert.That.IsNotEmpty(array, "Failed on Array");
            CollectionAssert.That.IsNotEmpty(list, "Failed on ArrayList");
            CollectionAssert.That.IsNotEmpty(hash, "Failed on Hashtable");
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsNotEmptyFailsOnEmptyArray()
        {
            CollectionAssert.That.IsNotEmpty(Array.Empty<int>());
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsNotEmptyFailsOnEmptyArrayList()
        {
            CollectionAssert.That.IsNotEmpty(new ArrayList());
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsNotEmptyFailsOnEmptyHashTable()
        {
            CollectionAssert.That.IsNotEmpty(new Hashtable());
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsNotEmptyFailsOnEmptyString()
        {
            Assert.That.IsNotEmpty("");
        }
    }
}