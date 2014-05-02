using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru;
using Cadru.Text;
using System.Diagnostics.CodeAnalysis;
using Cadru.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void Contains()
        {
            Assert.IsTrue("this is a test".Contains("is", StringComparison.CurrentCultureIgnoreCase));
            Assert.IsFalse("this is a test".Contains("not", StringComparison.CurrentCultureIgnoreCase));
        }

        [TestMethod]
        public void Truncate()
        {
            Assert.AreEqual("this", "this is a test".Truncate(4));
            Assert.AreEqual("this is a test", "this is a test".Truncate(15));
            Assert.AreEqual("", "".Truncate(15));
            Assert.AreEqual("", "".Truncate(0));
            Assert.AreEqual("", "this is a test".Truncate(0));
        }

        [TestMethod]
        public void RemoveWhiteSpace()
        {
            Assert.AreEqual("thisisatest.", "this  is  a  test.".RemoveWhiteSpace());
            Assert.AreEqual("thisisatest.", "this is a test.".RemoveWhiteSpace());
            Assert.AreEqual("thisisatest.", "thisisatest.".RemoveWhiteSpace());
            Assert.AreEqual("", "".RemoveWhiteSpace());
            Assert.AreEqual("a", "a".RemoveWhiteSpace());
        }

        [TestMethod]
        public void IsEmpty()
        {
            Assert.IsTrue(((string)null).IsNullOrWhiteSpace());
            Assert.IsTrue("".IsNullOrWhiteSpace());
            Assert.IsTrue("   ".IsNullOrWhiteSpace());
            Assert.IsTrue("\n".IsNullOrWhiteSpace());
            Assert.IsTrue("\t".IsNullOrWhiteSpace());
            Assert.IsFalse("abc".IsNullOrWhiteSpace());
            Assert.IsFalse("\0".IsNullOrWhiteSpace());
            Assert.IsFalse("\u1680d\u2004c\u205fb\u2028a\u00a0".IsNullOrWhiteSpace());
            Assert.IsTrue("\u1680 \u2004 \u205f \u2028 \u00a0".IsNullOrWhiteSpace());

            Assert.IsFalse(((string)null).IsNotNullOrWhiteSpace());
            Assert.IsFalse("".IsNotNullOrWhiteSpace());
            Assert.IsFalse("   ".IsNotNullOrWhiteSpace());
            Assert.IsFalse("\n".IsNotNullOrWhiteSpace());
            Assert.IsFalse("\t".IsNotNullOrWhiteSpace());
            Assert.IsTrue("abc".IsNotNullOrWhiteSpace());
            Assert.IsTrue("\0".IsNotNullOrWhiteSpace());
            Assert.IsTrue("\u1680d\u2004c\u205fb\u2028a\u00a0".IsNotNullOrWhiteSpace());
            Assert.IsFalse("\u1680 \u2004 \u205f \u2028 \u00a0".IsNotNullOrWhiteSpace());
        }

        [TestMethod]
        public void SubstringBetween()
        {
            string testValue = "abcdefg";

            Assert.AreEqual("d", testValue.SubstringBetween('c', 'e'));
            Assert.AreEqual("d", testValue.SubstringBetween("c", "e"));
            Assert.AreEqual("cde", testValue.SubstringBetween('c', 'e', true));
            Assert.AreEqual("cde", testValue.SubstringBetween("c", "e", true));
            Assert.AreEqual("cde", testValue.SubstringBetween("c", "e", true, StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual("d", testValue.SubstringBetween('c', 'e', false));
            Assert.AreEqual("d", testValue.SubstringBetween("c", "e", false));
            Assert.AreEqual("d", testValue.SubstringBetween("c", "e", false, StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual("abcde", testValue.SubstringBetween(String.Empty, "e", true));
            Assert.AreEqual("abcd", testValue.SubstringBetween(String.Empty, "e", false));
            Assert.AreEqual("c", testValue.SubstringBetween("c", String.Empty, true));
            Assert.AreEqual("", testValue.SubstringBetween("c", String.Empty, false));
            Assert.AreEqual("a", testValue.SubstringBetween(String.Empty, String.Empty, true));
            Assert.AreEqual("", testValue.SubstringBetween("h", "j", true));
            Assert.AreEqual("", testValue.SubstringBetween("h", "j", false));
            Assert.AreEqual("", testValue.SubstringBetween("c", "j", true));
            Assert.AreEqual("", testValue.SubstringBetween("c", "j", false));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).SubstringBetween('c', 'e', false)).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).SubstringBetween("c", "e", false)).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.SubstringBetween(null, "e", false)).WithParameter("start");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.SubstringBetween("c", null, false)).WithParameter("end");
        }

        [TestMethod]
        public void LastCharacter()
        {
            string testValue = "abcdefg";

            Assert.AreEqual('g', testValue.LastCharacter());
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LastCharacter());
        }

        [TestMethod]
        public void LeftSubstring()
        {
            string testValue = "abcdefgabcdefg";

            Assert.AreEqual("abc", testValue.LeftSubstring('c'));
            Assert.AreEqual("abcd", testValue.LeftSubstring(3));
            Assert.AreEqual("abc", testValue.LeftSubstring(3, false));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring("q"));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring("q", 1));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring("q", 2));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring('q'));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring('q', 1));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring('q', 2));
            Assert.AreEqual("abc", testValue.LeftSubstring("c"));
            Assert.AreEqual("abcdefgabcdefg", testValue.LeftSubstring('c', 3));
            Assert.AreEqual("abc", testValue.LeftSubstring('c', 1));
            Assert.AreEqual("abcdefgabc", testValue.LeftSubstring('c', 2));
            Assert.AreEqual("abc", testValue.LeftSubstring("c", 1));
            Assert.AreEqual("abcdefgabc", testValue.LeftSubstring("c", 2));
            Assert.AreEqual("abcdefgabcdefg", testValue.LeftSubstring("c", 3));
            Assert.AreEqual("abc", testValue.LeftSubstring("c", 1, StringComparison.InvariantCultureIgnoreCase));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LeftSubstring("c")).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.LeftSubstring(null)).WithParameter("value");
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LeftSubstring(null, 3)).WithParameter("source");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.LeftSubstring(-1));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.LeftSubstring(0));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.LeftSubstring(20));
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LeftSubstring('c')).WithParameter("source");
        }

        [TestMethod]
        public void Clean()
        {
            string testValue = " abc   defg abcdefg ";

            Assert.AreEqual("abc defg abcdefg", testValue.Clean());
            Assert.AreEqual("abc defg abcdefg", testValue.Clean(NormalizationOptions.Whitespace));

            testValue = " abc\tdefg\n\u001Fabcdefg ";
            Assert.AreEqual(" abcdefgabcdefg ", testValue.Clean(NormalizationOptions.ControlCharacters));
            Assert.AreEqual(testValue, testValue.Clean(NormalizationOptions.None));
            Assert.AreEqual("abcdefgabcdefg", "abcdefgabcdefg ".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("abc defgabcdefg", "abc  defgabcdefg ".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("abc defgabcdefg", "  abc  defgabcdefg ".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("abc defga bcdefg", "  abc  defga\nbcdefg ".Clean(NormalizationOptions.Whitespace));

            Assert.AreEqual("\ufeff", "\ufeff".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff", "\ufeff".Clean(NormalizationOptions.All));
            Assert.AreEqual("", "".Clean(NormalizationOptions.None));
            Assert.AreEqual("\0", "\0".Clean(NormalizationOptions.None));
            Assert.AreEqual("\0", "\0".Clean(NormalizationOptions.ControlCharacters));
            Assert.AreEqual("\0", "\0\0".Clean(NormalizationOptions.ControlCharacters));
            Assert.AreEqual("\0", "\0".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\0\0", "\0\0".Clean(NormalizationOptions.None));
            Assert.AreEqual("\0", "\0\0\0".Clean(NormalizationOptions.ControlCharacters));
            Assert.AreEqual("\0\ufeff", "\0\ufeff".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff\ufeff \u0100\u0100\u0100 \b", "\ufeff\ufeff\u1680\u0100\u0100\u0100 \b".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff\ufeff \u0100\u0100\u0100\u0100\u0100\u0100 \0\0\0", "\ufeff\ufeff\u1680\u0100\u0100\u0100\u0100\u0100\u0100\t\n\0\0\0".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff\u0100\u0100\u0100\u0100\u0100\u0100 \0", "\ufeff\u0100\u0100\u0100\u0100\u0100\u0100\t\t\t\n\0".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual(" \u0100\u0100\u0100\u0100\ufeff\ufeff\ufeff\ufeff\ufeff\ufeff", "\u0019 \u0100\u0100\u0100\u0100\0\ufeff\ufeff\ufeff\ufeff\ufeff\ufeff".Clean(NormalizationOptions.All));
            Assert.AreEqual("\ufeff\0", "\ufeff\0".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff", "\ufeff\u2000".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff\0\ufeff", "\ufeff\0\ufeff".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff\u0100", "\ufeff\u0100".Clean(NormalizationOptions.All));
            Assert.AreEqual(" \0", "\0 \0".Clean(NormalizationOptions.ControlCharacters));
            Assert.AreEqual("\ufeff \0", "\ufeff\u2000\0".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("!\0", "!\0".Clean(NormalizationOptions.All));
            Assert.AreEqual("\0\ufeff\ufeff", "\0\ufeff\ufeff".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff \u0100\u0100\u0100 \0", "\ufeff\u2000\u0100\u0100\u0100\n\0".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff \u0100\0\0", "\ufeff\u2000\u1680\u0100\0\0".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("! !", "!\t\u0019\0!".Clean(NormalizationOptions.All));
            Assert.AreEqual("\ufeff \u0100\u0100", "\ufeff\u2000\u1680\u1680\u0100\u0100".Clean(NormalizationOptions.Whitespace));
            Assert.AreEqual("! \0", "!\t\t\u0019\0\0".Clean(NormalizationOptions.All));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).Clean());
            ExceptionAssert.Throws<ArgumentException>(() => testValue.Clean((NormalizationOptions)30));
            ExceptionAssert.Throws<ArgumentException>(() => testValue.Clean((NormalizationOptions)(-1)));
        }

        [TestMethod]
        public void OccurrencesOf()
        {
            string testValue = "abcdefgabcdefgh";

            Assert.AreEqual(2, testValue.OccurrencesOf('c'));
            Assert.AreEqual(2, testValue.OccurrencesOf("c"));
            Assert.AreEqual(2, testValue.OccurrencesOf("c", StringComparison.InvariantCulture));
            Assert.AreEqual(0, testValue.OccurrencesOf("C", StringComparison.InvariantCulture));
            Assert.AreEqual(1, testValue.OccurrencesOf('h'));
            Assert.AreEqual(1, testValue.OccurrencesOf("h"));
            Assert.AreEqual(0, testValue.OccurrencesOf('q'));
            Assert.AreEqual(0, testValue.OccurrencesOf("q"));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).OccurrencesOf('c'));
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).OccurrencesOf("c"));
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.OccurrencesOf(null)).WithParameter("value");
        }

        [TestMethod]
        public void Replace()
        {
            string testValue = "abcdefgabcdefg";

            Assert.AreEqual("abQdefgabcdefg", testValue.Replace('c', 'Q', 1));
            Assert.AreEqual("abcdefgabcdefg", testValue.Replace('h', 'Q', 2));
            Assert.AreEqual("abQdefgabQdefg", testValue.Replace('c', 'Q', 3));
            Assert.AreEqual("abQdefgabQdefg", testValue.Replace('c', 'Q', 2));
            Assert.AreEqual(testValue, testValue.Replace('c', 'Q', 0));
            Assert.AreEqual("abCDEfgabcdefg", testValue.Replace("cde", "CDE", 1));
            Assert.AreEqual("abCDEfgabCDEfg", testValue.Replace("cde", "CDE", 2));
            Assert.AreEqual(testValue, testValue.Replace("cde", "CDE", 0));
            Assert.AreEqual("abCDEfgabCDEfg", testValue.Replace("cde", "CDE", 3));
            Assert.AreEqual("abCDEfgabcdefg", testValue.Replace("cde", "CDE", 1, StringComparison.InvariantCulture));
            Assert.AreEqual("abCDEfgabCDEfg", testValue.Replace("cde", "CDE", 2, StringComparison.InvariantCulture));
            Assert.AreEqual(testValue, testValue.Replace("cde", "CDE", 0, StringComparison.InvariantCulture));
            Assert.AreEqual(testValue, testValue.Replace("CDE", "CDE", 1, StringComparison.InvariantCulture));
            Assert.AreEqual(testValue, testValue.Replace("CDE", "CDE", 2, StringComparison.InvariantCulture));
            Assert.AreEqual(testValue, testValue.Replace("CDE", "CDE", 0, StringComparison.InvariantCulture));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).Replace('c', 'Q', 0));
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).Replace("CDE", "CDE", 0, StringComparison.InvariantCulture)).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.Replace(null, "CDE", 0, StringComparison.InvariantCulture)).WithParameter("oldValue");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.Replace("CDE", null, 0, StringComparison.InvariantCulture)).WithParameter("newValue");
        }

        [TestMethod]
        public void ReplaceBetween()
        {
            string testValue = "abcdefgabcdefg";

            Assert.AreEqual("abcXYZefgabcdefg", testValue.ReplaceBetween('c', 'e', "XYZ"));
            Assert.AreEqual("abcdefgabcdefg", testValue.ReplaceBetween('h', 'Q', "XYZ"));
            Assert.AreEqual("abcXYZefgabcdefg", testValue.ReplaceBetween(2, 4, "XYZ"));
            Assert.AreEqual("abcXYZefgabcdefg", testValue.ReplaceBetween("c", "e", "XYZ"));
            Assert.AreEqual("abXYZfgabcdefg", testValue.ReplaceBetween('c', 'e', "XYZ", true));
            Assert.AreEqual("abXYZfgabcdefg", testValue.ReplaceBetween(2, 4, "XYZ", true));
            Assert.AreEqual("abXYZfgabcdefg", testValue.ReplaceBetween("c", "e", "XYZ", true));
            Assert.AreEqual("abXYZfgabcdefg", testValue.ReplaceBetween("c", "e", "XYZ", true, StringComparison.InvariantCulture));
            Assert.AreEqual(testValue, testValue.ReplaceBetween("C", "E", "XYZ", true, StringComparison.InvariantCulture));
            Assert.AreEqual("abcXYZefgabcdefg", testValue.ReplaceBetween("c", "e", "XYZ", false, StringComparison.InvariantCulture));
            Assert.AreEqual(testValue, testValue.ReplaceBetween("C", "E", "XYZ", false, StringComparison.InvariantCulture));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).ReplaceBetween('C', 'E', "XYZ")).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.ReplaceBetween('C', 'E', null)).WithParameter("newValue");
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).ReplaceBetween("C", "E", "XYZ", true, StringComparison.InvariantCulture)).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.ReplaceBetween(null, "E", "XYZ", true, StringComparison.InvariantCulture)).WithParameter("start");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.ReplaceBetween("C", null, "XYZ", true, StringComparison.InvariantCulture)).WithParameter("end");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.ReplaceBetween("C", "E", null, true, StringComparison.InvariantCulture)).WithParameter("newValue");
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).ReplaceBetween(2, 4, "XYZ", true)).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.ReplaceBetween(2, 4, null, true)).WithParameter("newValue");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.ReplaceBetween(-1, 4, "XYZ", true)).WithParameter("start");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.ReplaceBetween(2, -1, "XYZ", true)).WithParameter("end");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.ReplaceBetween(2, 2, "XYZ", true));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.ReplaceBetween(10, 2, "XYZ", true));
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.ReplaceBetween(20, 4, "XYZ", true)).WithParameter("start");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.ReplaceBetween(2, 20, "XYZ", true)).WithParameter("end");
        }

        [TestMethod]
        public void RightSubstring()
        {
            string testValue = "abcdefgabcdefg";

            Assert.AreEqual("defgabcdefg", testValue.RightSubstring('c'));
            Assert.AreEqual("cdefgabcdefg", testValue.RightSubstring(3));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring("q"));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring("q", 1));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring("q", 2));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring('q'));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring('q', 1));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring('q', 2));
            Assert.AreEqual("defgabcdefg", testValue.RightSubstring("c"));
            Assert.AreEqual("defgabcdefg", testValue.RightSubstring('c', 1));
            Assert.AreEqual("defg", testValue.RightSubstring('c', 2));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring('c', 3));
            Assert.AreEqual("cdefgabcdefg", testValue.RightSubstring(3, true));
            Assert.AreEqual("defgabcdefg", testValue.RightSubstring(3, false));
            Assert.AreEqual("defgabcdefg", testValue.RightSubstring("c", 1));
            Assert.AreEqual("defg", testValue.RightSubstring("c", 2));
            Assert.AreEqual("abcdefgabcdefg", testValue.RightSubstring("c", 3));
            Assert.AreEqual("defgabcdefg", testValue.RightSubstring("c", 1, StringComparison.InvariantCultureIgnoreCase));
            Assert.AreEqual("defg", testValue.RightSubstring("c", 2, StringComparison.InvariantCultureIgnoreCase));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).RightSubstring("c")).WithParameter("source");
            ExceptionAssert.Throws<ArgumentNullException>(() => testValue.RightSubstring(null)).WithParameter("value");
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).RightSubstring(3)).WithParameter("source");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.RightSubstring(-1)).WithParameter("endingIndex");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.RightSubstring(0)).WithParameter("endingIndex");
            ExceptionAssert.Throws<ArgumentOutOfRangeException>(() => testValue.RightSubstring(20)).WithParameter("endingIndex");
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).RightSubstring('c', 1)).WithParameter("source");
        }

        [TestMethod]
        public void ResizeString()
        {
            string testValue = "abcdefg";

            Assert.AreEqual("abc", testValue.ResizeString(3));
            Assert.AreEqual("abcdefg     ", testValue.ResizeString(12));
            Assert.AreEqual("            ", String.Empty.ResizeString(12));
            Assert.AreEqual("            ", ((string)null).ResizeString(12));
        }

        [TestMethod]
        public void LengthLessThan()
        {
            Assert.IsTrue("ABCD".LengthLessThan(5));
            Assert.IsFalse("ABCDE".LengthLessThan(5));
            Assert.IsFalse("ABCDEF".LengthLessThan(5));
            Assert.IsTrue(String.Empty.LengthLessThan(5));
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LengthLessThan(5));
        }

        [TestMethod]
        public void LengthLessThanOrEqual()
        {
            Assert.IsTrue("ABCD".LengthLessThanOrEqualTo(5));
            Assert.IsTrue("ABCDE".LengthLessThanOrEqualTo(5));
            Assert.IsFalse("ABCDEF".LengthLessThanOrEqualTo(5));
            Assert.IsTrue(String.Empty.LengthLessThanOrEqualTo(5));
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LengthLessThanOrEqualTo(5));
        }

        [TestMethod]
        public void LengthGreaterThan()
        {
            Assert.IsTrue("ABCDEFGHIJ".LengthGreaterThan(5));
            Assert.IsFalse("ABCDE".LengthGreaterThan(5));
            Assert.IsFalse("ABCD".LengthGreaterThan(5));
            Assert.IsFalse(String.Empty.LengthGreaterThan(5));
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LengthGreaterThan(5));
        }

        [TestMethod]
        public void LengthGreaterThanOrEqualTo()
        {
            Assert.IsTrue("ABCDEFGHIJ".LengthGreaterThanOrEqualTo(5));
            Assert.IsTrue("ABCDE".LengthGreaterThanOrEqualTo(5));
            Assert.IsFalse("ABCD".LengthGreaterThanOrEqualTo(5));
            Assert.IsFalse(String.Empty.LengthGreaterThanOrEqualTo(5));
            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LengthGreaterThanOrEqualTo(5));
        }

        [TestMethod]
        public void LengthBetween()
        {
            Assert.IsTrue("ABCDE".LengthBetween(5, 10));
            Assert.IsTrue("ABCDEFGHIJ".LengthBetween(5, 10));
            Assert.IsTrue("ABCDEF".LengthBetween(5, 10));
            Assert.IsFalse("ABCD".LengthBetween(5, 10));
            Assert.IsFalse("ABCDEFGHIJK".LengthBetween(5, 10));

            Assert.IsFalse("ABCDE".LengthBetween(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue("ABCDEFGHIJ".LengthBetween(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue("ABCDEF".LengthBetween(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse("ABCD".LengthBetween(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse("ABCDEFGHIJK".LengthBetween(5, 10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue("ABCDE".LengthBetween(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse("ABCDEFGHIJ".LengthBetween(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue("ABCDEF".LengthBetween(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse("ABCD".LengthBetween(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse("ABCDEFGHIJK".LengthBetween(5, 10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse("ABCDE".LengthBetween(5, 10, NumericComparisonOptions.None));
            Assert.IsFalse("ABCDEFGHIJ".LengthBetween(5, 10, NumericComparisonOptions.None));
            Assert.IsTrue("ABCDEF".LengthBetween(5, 10, NumericComparisonOptions.None));
            Assert.IsFalse("ABCD".LengthBetween(5, 10, NumericComparisonOptions.None));
            Assert.IsFalse("ABCDEFGHIJK".LengthBetween(5, 10, NumericComparisonOptions.None));

            Assert.IsFalse(String.Empty.LengthBetween(5, 10));
            Assert.IsFalse(String.Empty.LengthBetween(5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(String.Empty.LengthBetween(5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(String.Empty.LengthBetween(5, 10, NumericComparisonOptions.None));

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).LengthBetween(5, 10));
        }
    }
}