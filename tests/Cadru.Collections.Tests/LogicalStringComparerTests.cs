﻿//------------------------------------------------------------------------------
// <copyright file="LogicalStringComparerTests.cs"
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

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Collections.Tests
{
    /// <summary>
    ///This is a test class for CadruCollections.LogicalStringComparer and is intended
    ///to contain all CadruCollections.LogicalStringComparer Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class LogicalStringComparerTests
    {
        /// <summary>
        ///A test for Compare (object, object)
        ///</summary>
        [TestMethod]
        public void Compare()
        {
            var comparer = LogicalStringComparer.Default;

            Assert.IsTrue(comparer.Compare("abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "abc") > 0);
            Assert.IsTrue(comparer.Compare("abc", "abc") == 0);

            Assert.IsTrue(comparer.Compare("abc1", "abc10") < 0);
            Assert.IsTrue(comparer.Compare("abc10", "abc1") > 0);
            Assert.IsTrue(comparer.Compare("abc1", "abc1") == 0);

            Assert.IsTrue(comparer.Compare("=1", "[1") < 0);
            Assert.IsTrue(comparer.Compare("[1", "=1") > 0);
            Assert.IsTrue(comparer.Compare("=1", "=1") == 0);

            Assert.IsFalse(comparer.Compare("abc", "[1") < 0);
            Assert.IsFalse(comparer.Compare("[1", "abc") > 0);
            Assert.IsTrue(comparer.Compare("[1", "[1") == 0);

            Assert.IsTrue(comparer.Compare("abc", String.Empty) == 1);
            Assert.IsTrue(comparer.Compare(String.Empty, "abc") == -1);
            Assert.IsTrue(comparer.Compare(String.Empty, String.Empty) == 0);

            Assert.IsTrue(comparer.Compare("abc", null) == 1);
            Assert.IsTrue(comparer.Compare(null, "abc") == -1);
            Assert.IsTrue(comparer.Compare(null, null) == 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bc") < 0);
            Assert.IsFalse(comparer.Compare("a†bc", "abc") > 0);
            Assert.IsTrue(comparer.Compare("a†bc", "a†bc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("1abc", "1abc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "2abc") < 0);
            Assert.IsTrue(comparer.Compare("2abc", "1abc") > 0);

            Assert.IsTrue(comparer.Compare("1abc", "10abc") < 0);
            Assert.IsTrue(comparer.Compare("10abc", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("11abc", "12abc") < 0);
            Assert.IsTrue(comparer.Compare("11abc", "11abc") == 0);

            Assert.IsTrue(comparer.Compare("abc", "defg") < 0);
            Assert.IsTrue(comparer.Compare("defg", "abc") > 0);

            Assert.IsTrue(comparer.Compare("abc1", "abcd10") < 0);
            Assert.IsTrue(comparer.Compare("abcd10", "abc1") > 0);

            Assert.IsTrue(comparer.Compare("=1", "[1a") < 0);
            Assert.IsTrue(comparer.Compare("[1a", "=1") > 0);

            Assert.IsFalse(comparer.Compare("abc", "[1a") < 0);
            Assert.IsFalse(comparer.Compare("[1a", "abc") > 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bcd") < 0);
            Assert.IsFalse(comparer.Compare("a†bcd", "abc") > 0);

            comparer = LogicalStringComparer.DefaultInvariant;

            Assert.IsTrue(comparer.Compare("abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "abc") > 0);
            Assert.IsTrue(comparer.Compare("abc", "abc") == 0);

            Assert.IsTrue(comparer.Compare("abc1", "abc10") < 0);
            Assert.IsTrue(comparer.Compare("abc10", "abc1") > 0);
            Assert.IsTrue(comparer.Compare("abc1", "abc1") == 0);

            Assert.IsTrue(comparer.Compare("=1", "[1") < 0);
            Assert.IsTrue(comparer.Compare("[1", "=1") > 0);
            Assert.IsTrue(comparer.Compare("=1", "=1") == 0);

            Assert.IsFalse(comparer.Compare("abc", "[1") < 0);
            Assert.IsFalse(comparer.Compare("[1", "abc") > 0);
            Assert.IsTrue(comparer.Compare("[1", "[1") == 0);

            Assert.IsTrue(comparer.Compare("abc", String.Empty) == 1);
            Assert.IsTrue(comparer.Compare(String.Empty, "abc") == -1);
            Assert.IsTrue(comparer.Compare(String.Empty, String.Empty) == 0);

            Assert.IsTrue(comparer.Compare("abc", null) == 1);
            Assert.IsTrue(comparer.Compare(null, "abc") == -1);
            Assert.IsTrue(comparer.Compare(null, null) == 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bc") < 0);
            Assert.IsFalse(comparer.Compare("a†bc", "abc") > 0);
            Assert.IsTrue(comparer.Compare("a†bc", "a†bc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("1abc", "1abc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "2abc") < 0);
            Assert.IsTrue(comparer.Compare("2abc", "1abc") > 0);

            Assert.IsTrue(comparer.Compare("1abc", "10abc") < 0);
            Assert.IsTrue(comparer.Compare("10abc", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("11abc", "12abc") < 0);
            Assert.IsTrue(comparer.Compare("11abc", "11abc") == 0);

            Assert.IsTrue(comparer.Compare("abc", "defg") < 0);
            Assert.IsTrue(comparer.Compare("defg", "abc") > 0);

            Assert.IsTrue(comparer.Compare("abc1", "abcd10") < 0);
            Assert.IsTrue(comparer.Compare("abcd10", "abc1") > 0);

            Assert.IsTrue(comparer.Compare("=1", "[1a") < 0);
            Assert.IsTrue(comparer.Compare("[1a", "=1") > 0);

            Assert.IsFalse(comparer.Compare("abc", "[1a") < 0);
            Assert.IsFalse(comparer.Compare("[1a", "abc") > 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bcd") < 0);
            Assert.IsFalse(comparer.Compare("a†bcd", "abc") > 0);

            Assert.ThrowsException<ArgumentNullException>(() => new LogicalStringComparer(null));
        }

        /// <summary>
        ///A test for Compare (string, string)
        ///</summary>
        [TestMethod]
        public void Compare1()
        {
            var comparer = LogicalStringComparer.Default as LogicalStringComparer;

            Assert.IsTrue(comparer.Compare("abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "abc") > 0);
            Assert.IsTrue(comparer.Compare("abc", "abc") == 0);

            Assert.IsTrue(comparer.Compare("abc1", "abc10") < 0);
            Assert.IsTrue(comparer.Compare("abc10", "abc1") > 0);
            Assert.IsTrue(comparer.Compare("abc1", "abc1") == 0);

            Assert.IsTrue(comparer.Compare("abc1d", "abc10d") < 0);
            Assert.IsTrue(comparer.Compare("abc10d", "abc1d") > 0);
            Assert.IsTrue(comparer.Compare("abc1d", "abc1d") == 0);

            Assert.IsTrue(comparer.Compare("abc1d", "abcd") < 0);
            Assert.IsTrue(comparer.Compare("abcd", "abc1d") > 0);
            Assert.IsTrue(comparer.Compare("10abc1d", "abcd") < 0);
            Assert.IsTrue(comparer.Compare("abcd", "10abc1d") > 0);
            Assert.IsTrue(comparer.Compare("10abc1d", "abcd") < 0);

            Assert.IsTrue(comparer.Compare("abc1d", "abc11d") < 0);
            Assert.IsTrue(comparer.Compare("abc11d", "abc1d") > 0);

            Assert.IsTrue(comparer.Compare("=1", "[1") < 0);
            Assert.IsTrue(comparer.Compare("[1", "=1") > 0);
            Assert.IsTrue(comparer.Compare("=1", "=1") == 0);

            Assert.IsFalse(comparer.Compare("abc", "[1") < 0);
            Assert.IsFalse(comparer.Compare("[1", "abc") > 0);
            Assert.IsTrue(comparer.Compare("[1", "[1") == 0);

            Assert.IsTrue(comparer.Compare("abc", String.Empty) == 1);
            Assert.IsTrue(comparer.Compare(String.Empty, "abc") == -1);
            Assert.IsTrue(comparer.Compare(String.Empty, String.Empty) == 0);

            Assert.IsTrue(comparer.Compare("abc", null) == 1);
            Assert.IsTrue(comparer.Compare(null, "abc") == -1);
            Assert.IsTrue(comparer.Compare(null, null) == 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bc") < 0);
            Assert.IsFalse(comparer.Compare("a†bc", "abc") > 0);
            Assert.IsTrue(comparer.Compare("a†bc", "a†bc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("1abc", "1abc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "2abc") < 0);
            Assert.IsTrue(comparer.Compare("2abc", "1abc") > 0);

            Assert.IsTrue(comparer.Compare("1abc", "10abc") < 0);
            Assert.IsTrue(comparer.Compare("10abc", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("11abc", "12abc") < 0);
            Assert.IsTrue(comparer.Compare("11abc", "11abc") == 0);

            Assert.IsTrue(comparer.Compare("abc", "defg") < 0);
            Assert.IsTrue(comparer.Compare("defg", "abc") > 0);

            Assert.IsTrue(comparer.Compare("abc1", "abcd10") < 0);
            Assert.IsTrue(comparer.Compare("abcd10", "abc1") > 0);

            Assert.IsTrue(comparer.Compare("=1", "[1a") < 0);
            Assert.IsTrue(comparer.Compare("[1a", "=1") > 0);

            Assert.IsFalse(comparer.Compare("abc", "[1a") < 0);
            Assert.IsFalse(comparer.Compare("[1a", "abc") > 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bcd") < 0);
            Assert.IsFalse(comparer.Compare("a†bcd", "abc") > 0);

            Assert.IsTrue(comparer.Compare("10", "11") < 0);
            Assert.IsTrue(comparer.Compare("11", "10") > 0);

            Assert.IsTrue(comparer.Compare("05", "09") < 0);
            Assert.IsTrue(comparer.Compare("09", "0") > 0);

            Assert.IsTrue(comparer.Compare("05", "9") < 0);
            Assert.IsTrue(comparer.Compare("09", "00") > 0);

            Assert.IsTrue(comparer.Compare("05abc", "9abc") < 0);
            Assert.IsTrue(comparer.Compare("9abc", "05abc") > 0);
            Assert.IsTrue(comparer.Compare("5abc", "09abc") < 0);
            Assert.IsTrue(comparer.Compare("09abc", "5abc") > 0);

            Assert.IsTrue(comparer.Compare("10", "100") < 0);
            Assert.IsTrue(comparer.Compare("100", "10") > 0);

            Assert.IsTrue(comparer.Compare("q\u8030R0P\0", "q\u8030r0pR") == -1);
            Assert.IsTrue(comparer.Compare("q\u8030\u8030B0\u80300", "q\u8030\u8030b0\u80300") == 0);

            comparer = LogicalStringComparer.DefaultInvariant as LogicalStringComparer;

            Assert.IsTrue(comparer.Compare("abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "abc") > 0);
            Assert.IsTrue(comparer.Compare("abc", "abc") == 0);

            Assert.IsTrue(comparer.Compare("abc1", "abc10") < 0);
            Assert.IsTrue(comparer.Compare("abc10", "abc1") > 0);
            Assert.IsTrue(comparer.Compare("abc1", "abc1") == 0);

            Assert.IsTrue(comparer.Compare("=1", "[1") < 0);
            Assert.IsTrue(comparer.Compare("[1", "=1") > 0);
            Assert.IsTrue(comparer.Compare("=1", "=1") == 0);

            Assert.IsFalse(comparer.Compare("abc", "[1") < 0);
            Assert.IsFalse(comparer.Compare("[1", "abc") > 0);
            Assert.IsTrue(comparer.Compare("[1", "[1") == 0);

            Assert.IsTrue(comparer.Compare("abc", String.Empty) == 1);
            Assert.IsTrue(comparer.Compare(String.Empty, "abc") == -1);
            Assert.IsTrue(comparer.Compare(String.Empty, String.Empty) == 0);

            Assert.IsTrue(comparer.Compare("abc", null) == 1);
            Assert.IsTrue(comparer.Compare(null, "abc") == -1);
            Assert.IsTrue(comparer.Compare(null, null) == 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bc") < 0);
            Assert.IsFalse(comparer.Compare("a†bc", "abc") > 0);
            Assert.IsTrue(comparer.Compare("a†bc", "a†bc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "def") < 0);
            Assert.IsTrue(comparer.Compare("def", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("1abc", "1abc") == 0);

            Assert.IsTrue(comparer.Compare("1abc", "2abc") < 0);
            Assert.IsTrue(comparer.Compare("2abc", "1abc") > 0);

            Assert.IsTrue(comparer.Compare("1abc", "10abc") < 0);
            Assert.IsTrue(comparer.Compare("10abc", "1abc") > 0);
            Assert.IsTrue(comparer.Compare("11abc", "12abc") < 0);
            Assert.IsTrue(comparer.Compare("11abc", "11abc") == 0);

            Assert.IsTrue(comparer.Compare("abc", "defg") < 0);
            Assert.IsTrue(comparer.Compare("defg", "abc") > 0);

            Assert.IsTrue(comparer.Compare("abc1", "abcd10") < 0);
            Assert.IsTrue(comparer.Compare("abcd10", "abc1") > 0);

            Assert.IsTrue(comparer.Compare("=1", "[1a") < 0);
            Assert.IsTrue(comparer.Compare("[1a", "=1") > 0);

            Assert.IsFalse(comparer.Compare("abc", "[1a") < 0);
            Assert.IsFalse(comparer.Compare("[1a", "abc") > 0);

            Assert.IsFalse(comparer.Compare("abc", "a†bcd") < 0);
            Assert.IsFalse(comparer.Compare("a†bcd", "abc") > 0);
        }

        [TestMethod]
        public void Equals()
        {
            var comparer = LogicalStringComparer.Default as LogicalStringComparer;

            Assert.IsFalse(comparer.Equals("abc", "def"));
            Assert.IsTrue(comparer.Equals("abc", "abc"));

            object x = "abc";
            object y = "def";

            Assert.IsFalse(Equals(x, y));
            Assert.IsTrue(Equals(x, x));
            Assert.IsFalse(((IEqualityComparer)comparer).Equals(x, y));
            Assert.IsTrue(((IEqualityComparer)comparer).Equals(x, x));
        }

        [TestMethod]
        public void HashCode()
        {
            var comparer = LogicalStringComparer.Default as LogicalStringComparer;
            var x = "abc";

            Assert.AreEqual(x.GetHashCode(), comparer.GetHashCode("abc"));

            object y = "def";
            Assert.AreEqual(y.GetHashCode(), comparer.GetHashCode(y));

            Assert.ThrowsException<ArgumentNullException>(() => comparer.GetHashCode(((object)null)));
            Assert.ThrowsException<ArgumentException>(() => comparer.GetHashCode(((object)DateTime.Today)));
            Assert.ThrowsException<ArgumentNullException>(() => comparer.GetHashCode(((string)null)));
        }
    }
}