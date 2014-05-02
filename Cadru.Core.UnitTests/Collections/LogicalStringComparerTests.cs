using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cadru.Collections;
using System.Diagnostics.CodeAnalysis;
using Cadru.UnitTest.Framework;

namespace Cadru.UnitTests.Collections
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
            IComparer comparer = LogicalStringComparer.Default;

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

            ExceptionAssert.Throws<ArgumentNullException>(() => new LogicalStringComparer(null));
        }

        /// <summary>
        ///A test for Compare (string, string)
        ///</summary>
        [TestMethod]
        public void Compare1()
        {
            LogicalStringComparer comparer = LogicalStringComparer.Default as LogicalStringComparer;

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
            LogicalStringComparer comparer = LogicalStringComparer.Default as LogicalStringComparer;
 
            Assert.IsFalse(comparer.Equals("abc", "def"));
            Assert.IsTrue(comparer.Equals("abc", "abc"));

            object x = "abc";
            object y = "def";

            Assert.IsFalse(LogicalStringComparer.Equals(x, y));
            Assert.IsTrue(LogicalStringComparer.Equals(x, x));
            Assert.IsFalse(((IEqualityComparer)comparer).Equals(x, y));
            Assert.IsTrue(((IEqualityComparer)comparer).Equals(x, x));
        }

        [TestMethod]
        public void HashCode()
        {
            LogicalStringComparer comparer = LogicalStringComparer.Default as LogicalStringComparer;
            string x = "abc";

            Assert.AreEqual(x.GetHashCode(), comparer.GetHashCode("abc"));

            object y = "def";
            Assert.AreEqual(y.GetHashCode(), comparer.GetHashCode(y));

            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.GetHashCode(((object)null)));
            ExceptionAssert.Throws<ArgumentException>(() => comparer.GetHashCode(((object)DateTime.Today)));
            ExceptionAssert.Throws<ArgumentNullException>(() => comparer.GetHashCode(((string)null)));
        }
    }
}
