using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru;
using Cadru.Text;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void IsEmpty()
        {
            Assert.IsTrue(StringExtensions.IsNullOrWhiteSpace(null));
            Assert.IsTrue(StringExtensions.IsNullOrWhiteSpace(""));
            Assert.IsTrue(StringExtensions.IsNullOrWhiteSpace("   "));
            Assert.IsTrue(StringExtensions.IsNullOrWhiteSpace("\n"));
            Assert.IsTrue(StringExtensions.IsNullOrWhiteSpace("\t"));
            Assert.IsFalse(StringExtensions.IsNullOrWhiteSpace("abc"));
            Assert.IsFalse(StringExtensions.IsNullOrWhiteSpace("\0"));
            Assert.IsFalse(StringExtensions.IsNullOrWhiteSpace("\u1680d\u2004c\u205fb\u2028a\u00a0"));
            Assert.IsTrue(StringExtensions.IsNullOrWhiteSpace("\u1680 \u2004 \u205f \u2028 \u00a0"));

            Assert.IsFalse(StringExtensions.IsNotNullOrWhiteSpace(null));
            Assert.IsFalse(StringExtensions.IsNotNullOrWhiteSpace(""));
            Assert.IsFalse(StringExtensions.IsNotNullOrWhiteSpace("   "));
            Assert.IsFalse(StringExtensions.IsNotNullOrWhiteSpace("\n"));
            Assert.IsFalse(StringExtensions.IsNotNullOrWhiteSpace("\t"));
            Assert.IsTrue(StringExtensions.IsNotNullOrWhiteSpace("abc"));
            Assert.IsTrue(StringExtensions.IsNotNullOrWhiteSpace("\0"));
            Assert.IsTrue(StringExtensions.IsNotNullOrWhiteSpace("\u1680d\u2004c\u205fb\u2028a\u00a0"));
            Assert.IsFalse(StringExtensions.IsNotNullOrWhiteSpace("\u1680 \u2004 \u205f \u2028 \u00a0"));
        }

        [TestMethod]
        public void SubstringBetween()
        {
            string testValue = "abcdefg";

            string expected = "d";
            string actual = StringExtensions.SubstringBetween(testValue, 'c', 'e');
            Assert.AreEqual(expected, actual);

            expected = "d";
            actual = StringExtensions.SubstringBetween(testValue, "c", "e");
            Assert.AreEqual(expected, actual);

            expected = "cde";
            actual = StringExtensions.SubstringBetween(testValue, 'c', 'e', true);
            Assert.AreEqual(expected, actual);

            expected = "cde";
            actual = StringExtensions.SubstringBetween(testValue, "c", "e", true);
            Assert.AreEqual(expected, actual);

            expected = "cde";
            actual = StringExtensions.SubstringBetween(testValue, "c", "e", true, StringComparison.InvariantCultureIgnoreCase);
            Assert.AreEqual(expected, actual);

            expected = "d";
            actual = StringExtensions.SubstringBetween(testValue, 'c', 'e', false);
            Assert.AreEqual(expected, actual);

            expected = "d";
            actual = StringExtensions.SubstringBetween(testValue, "c", "e", false);
            Assert.AreEqual(expected, actual);

            expected = "d";
            actual = StringExtensions.SubstringBetween(testValue, "c", "e", false, StringComparison.InvariantCultureIgnoreCase);
            Assert.AreEqual(expected, actual);

            expected = "abcde";
            actual = StringExtensions.SubstringBetween(testValue, String.Empty, "e", true);
            Assert.AreEqual(expected, actual);

            expected = "abcd";
            actual = StringExtensions.SubstringBetween(testValue, String.Empty, "e", false);
            Assert.AreEqual(expected, actual);

            expected = "c";
            actual = StringExtensions.SubstringBetween(testValue, "c", String.Empty, true);
            Assert.AreEqual(expected, actual);

            expected = "";
            actual = StringExtensions.SubstringBetween(testValue, "c", String.Empty, false);
            Assert.AreEqual(expected, actual);

            expected = "a";
            actual = StringExtensions.SubstringBetween(testValue, String.Empty, String.Empty, true);
            Assert.AreEqual(expected, actual);

            expected = "";
            actual = StringExtensions.SubstringBetween(testValue, "h", "j", true);
            Assert.AreEqual(expected, actual);

            expected = "";
            actual = StringExtensions.SubstringBetween(testValue, "h", "j", false);
            Assert.AreEqual(expected, actual);

            expected = "";
            actual = StringExtensions.SubstringBetween(testValue, "c", "j", true);
            Assert.AreEqual(expected, actual);

            expected = "";
            actual = StringExtensions.SubstringBetween(testValue, "c", "j", false);
            Assert.AreEqual(expected, actual);

            try
            {
                actual = StringExtensions.SubstringBetween(null, 'c', 'e', false);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.SubstringBetween(null, "c", "e", false);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.SubstringBetween(testValue, null, "e", false);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "start")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.SubstringBetween(testValue, "c", null, false);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "end")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LastCharacter()
        {
            string testValue = "abcdefg";

            char expected = 'g';
            char actual = StringExtensions.LastCharacter(testValue);
            Assert.AreEqual(expected, actual);

            try
            {
                actual = StringExtensions.LastCharacter(null);
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LeftSubstring()
        {
            string testValue = "abcdefgabcdefg";

            string expected = "abc";
            string actual = StringExtensions.LeftSubstring(testValue, 'c');
            Assert.AreEqual(expected, actual);

            expected = "abcd";
            actual = StringExtensions.LeftSubstring(testValue, 3);
            Assert.AreEqual(expected, actual);

            expected = "abc";
            actual = StringExtensions.LeftSubstring(testValue, 3, false);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "q");
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "q", 1);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "q", 2);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'q');
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'q', 1);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'q', 2);
            Assert.AreEqual(expected, actual);

            expected = "abc";
            actual = StringExtensions.LeftSubstring(testValue, "c");
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.LeftSubstring(testValue, 'c', 3);
            Assert.AreEqual(expected, actual);

            expected = "abc";
            actual = StringExtensions.LeftSubstring(testValue, 'c', 1);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabc";
            actual = StringExtensions.LeftSubstring(testValue, 'c', 2);
            Assert.AreEqual(expected, actual);

            expected = "abc";
            actual = StringExtensions.LeftSubstring(testValue, "c", 1);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabc";
            actual = StringExtensions.LeftSubstring(testValue, "c", 2);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.LeftSubstring(testValue, "c", 3);
            Assert.AreEqual(expected, actual);

            expected = "abc";
            actual = StringExtensions.LeftSubstring(testValue, "c", 1, StringComparison.InvariantCultureIgnoreCase);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void LeftSubstringExceptions()
        {
            string testValue = "abcdefgabcdefg";
            string actual;

            try
            {
                actual = StringExtensions.LeftSubstring(null, "c");
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.LeftSubstring(testValue, null);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "value")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.LeftSubstring(null, 3);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.LeftSubstring(testValue, -1);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "endingIndex")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.LeftSubstring(testValue, 0);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "endingIndex")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.LeftSubstring(testValue, 20);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "endingIndex")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.LeftSubstring(null, 'c');
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Normalize()
        {
            string testValue = " abc   defg abcdefg ";

            string expected = "abc defg abcdefg";
            string actual = StringExtensions.Normalize(testValue);
            Assert.AreEqual(expected, actual);

            expected = "abc defg abcdefg";
            actual = StringExtensions.Normalize(testValue, NormalizationOptions.Whitespace);
            Assert.AreEqual(expected, actual);

            testValue = " abc\tdefg\n\u001Fabcdefg ";
            expected = " abcdefgabcdefg ";
            actual = StringExtensions.Normalize(testValue, NormalizationOptions.ControlCharacters);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.Normalize(testValue, NormalizationOptions.None);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.Normalize("abcdefgabcdefg ", NormalizationOptions.Whitespace);
            Assert.AreEqual(expected, actual);

            expected = "abc defgabcdefg";
            actual = StringExtensions.Normalize("abc  defgabcdefg ", NormalizationOptions.Whitespace);
            Assert.AreEqual(expected, actual);

            expected = "abc defgabcdefg";
            actual = StringExtensions.Normalize("  abc  defgabcdefg ", NormalizationOptions.Whitespace);
            Assert.AreEqual(expected, actual);

            expected = "abc defga bcdefg";
            actual = StringExtensions.Normalize("  abc  defga\nbcdefg ", NormalizationOptions.Whitespace);
            Assert.AreEqual(expected, actual);

            Assert.AreEqual("\ufeff", StringExtensions.Normalize("\ufeff", NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff", StringExtensions.Normalize("\ufeff", NormalizationOptions.All));
            Assert.AreEqual("", StringExtensions.Normalize("", NormalizationOptions.None));
            Assert.AreEqual("\0", StringExtensions.Normalize("\0", NormalizationOptions.None));
            Assert.AreEqual("\0", StringExtensions.Normalize("\0", NormalizationOptions.ControlCharacters));
            Assert.AreEqual("\0", StringExtensions.Normalize("\0\0", NormalizationOptions.ControlCharacters));
            Assert.AreEqual("\0", StringExtensions.Normalize("\0", NormalizationOptions.Whitespace));
            Assert.AreEqual("\0\ufeff", StringExtensions.Normalize("\0\ufeff", NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff", StringExtensions.Normalize("\ufeff\u2000", NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff\0\ufeff", StringExtensions.Normalize("\ufeff\0\ufeff", NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff\u0100", StringExtensions.Normalize("\ufeff\u0100", NormalizationOptions.All));
            Assert.AreEqual(" \0", StringExtensions.Normalize("\0 \0", NormalizationOptions.ControlCharacters));
            Assert.AreEqual("\ufeff \0", StringExtensions.Normalize("\ufeff\u2000\0", NormalizationOptions.Whitespace));
            Assert.AreEqual("!\0", StringExtensions.Normalize("!\0", NormalizationOptions.All));
            Assert.AreEqual("\0\ufeff\ufeff", StringExtensions.Normalize("\0\ufeff\ufeff", NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff \u0100\u0100\u0100 \0", StringExtensions.Normalize("\ufeff\u2000\u0100\u0100\u0100\n\0", NormalizationOptions.Whitespace));
            Assert.AreEqual("\ufeff \u0100\0\0", StringExtensions.Normalize("\ufeff\u2000\u1680\u0100\0\0", NormalizationOptions.Whitespace));
            Assert.AreEqual("! !", StringExtensions.Normalize("!\t\u0019\0!", NormalizationOptions.All));
            Assert.AreEqual("\ufeff \u0100\u0100", StringExtensions.Normalize("\ufeff\u2000\u1680\u1680\u0100\u0100", NormalizationOptions.Whitespace));
            Assert.AreEqual("! \0", StringExtensions.Normalize("!\t\t\u0019\0\0", NormalizationOptions.All));

            try
            {
                actual = StringExtensions.Normalize(null);
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.Normalize(testValue, (NormalizationOptions)30);
            }
            catch (System.ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.Normalize(testValue, (NormalizationOptions)(-1));
            }
            catch (System.ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

        }

        [TestMethod]
        public void OccurrencesOf()
        {
            string testValue = "abcdefgabcdefgh";

            int expected = 2;
            int actual = StringExtensions.OccurrencesOf(testValue, 'c');
            Assert.AreEqual(expected, actual);

            expected = 2;
            actual = StringExtensions.OccurrencesOf(testValue, "c");
            Assert.AreEqual(expected, actual);

            expected = 2;
            actual = StringExtensions.OccurrencesOf(testValue, "c", StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = 0;
            actual = StringExtensions.OccurrencesOf(testValue, "C", StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = 1;
            actual = StringExtensions.OccurrencesOf(testValue, 'h');
            Assert.AreEqual(expected, actual);

            expected = 1;
            actual = StringExtensions.OccurrencesOf(testValue, "h");
            Assert.AreEqual(expected, actual);

            expected = 0;
            actual = StringExtensions.OccurrencesOf(testValue, 'q');
            Assert.AreEqual(expected, actual);

            expected = 0;
            actual = StringExtensions.OccurrencesOf(testValue, "q");
            Assert.AreEqual(expected, actual);

            try
            {
                actual = StringExtensions.OccurrencesOf(null, 'c');
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.OccurrencesOf(null, "c");
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.OccurrencesOf(testValue, null);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "value")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void Replace()
        {
            string testValue = "abcdefgabcdefg";

            string expected = "abQdefgabcdefg";
            string actual = StringExtensions.Replace(testValue, 'c', 'Q', 1);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.Replace(testValue, 'h', 'Q', 2);
            Assert.AreEqual(expected, actual);

            expected = "abQdefgabQdefg";
            actual = StringExtensions.Replace(testValue, 'c', 'Q', 3);
            Assert.AreEqual(expected, actual);

            expected = "abQdefgabQdefg";
            actual = StringExtensions.Replace(testValue, 'c', 'Q', 2);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.Replace(testValue, 'c', 'Q', 0);
            Assert.AreEqual(expected, actual);

            expected = "abCDEfgabcdefg";
            actual = StringExtensions.Replace(testValue, "cde", "CDE", 1);
            Assert.AreEqual(expected, actual);

            expected = "abCDEfgabCDEfg";
            actual = StringExtensions.Replace(testValue, "cde", "CDE", 2);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.Replace(testValue, "cde", "CDE", 0);
            Assert.AreEqual(expected, actual);

            expected = "abCDEfgabCDEfg";
            actual = StringExtensions.Replace(testValue, "cde", "CDE", 3);
            Assert.AreEqual(expected, actual);

            expected = "abCDEfgabcdefg";
            actual = StringExtensions.Replace(testValue, "cde", "CDE", 1, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = "abCDEfgabCDEfg";
            actual = StringExtensions.Replace(testValue, "cde", "CDE", 2, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.Replace(testValue, "cde", "CDE", 0, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.Replace(testValue, "CDE", "CDE", 1, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.Replace(testValue, "CDE", "CDE", 2, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.Replace(testValue, "CDE", "CDE", 0, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            try
            {
                actual = StringExtensions.Replace(null, 'c', 'Q', 0);
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.Replace(null, "CDE", "CDE", 0, StringComparison.InvariantCulture);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.Replace(testValue, null, "CDE", 0, StringComparison.InvariantCulture);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "oldValue")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.Replace(testValue, "CDE", null, 0, StringComparison.InvariantCulture);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "newValue")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ReplaceBetween()
        {
            string testValue = "abcdefgabcdefg";
            //string actual;

            string expected = "abcXYZefgabcdefg";
            string actual = StringExtensions.ReplaceBetween(testValue, 'c', 'e', "XYZ");
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, 'h', 'Q', "XYZ");
            Assert.AreEqual(expected, actual);

            expected = "abcXYZefgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, 2, 4, "XYZ");
            Assert.AreEqual(expected, actual);

            expected = "abcXYZefgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, "c", "e", "XYZ");
            Assert.AreEqual(expected, actual);

            expected = "abXYZfgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, 'c', 'e', "XYZ", true);
            Assert.AreEqual(expected, actual);

            expected = "abXYZfgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, 2, 4, "XYZ", true);
            Assert.AreEqual(expected, actual);

            expected = "abXYZfgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, "c", "e", "XYZ", true);
            Assert.AreEqual(expected, actual);

            expected = "abXYZfgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, "c", "e", "XYZ", true, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.ReplaceBetween(testValue, "C", "E", "XYZ", true, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = "abcXYZefgabcdefg";
            actual = StringExtensions.ReplaceBetween(testValue, "c", "e", "XYZ", false, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);

            expected = testValue;
            actual = StringExtensions.ReplaceBetween(testValue, "C", "E", "XYZ", false, StringComparison.InvariantCulture);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReplaceBetween1()
        {
            string testValue = "abcdefgabcdefg";
            string actual;

            try
            {
                actual = StringExtensions.ReplaceBetween(null, 'C', 'E', "XYZ");
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, 'C', 'E', null);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "newValue")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ReplaceBetween2()
        {
            string testValue = "abcdefgabcdefg";
            string actual;

            try
            {
                actual = StringExtensions.ReplaceBetween(null, "C", "E", "XYZ", true, StringComparison.InvariantCulture);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, null, "E", "XYZ", true, StringComparison.InvariantCulture);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "start")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, "C", null, "XYZ", true, StringComparison.InvariantCulture);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "end")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, "C", "E", null, true, StringComparison.InvariantCulture);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "newValue")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ReplaceBetween3()
        {
            string testValue = "abcdefgabcdefg";
            string actual;

            try
            {
                actual = StringExtensions.ReplaceBetween(null, 2, 4, "XYZ", true);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, 2, 4, null, true);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "newValue")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, -1, 4, "XYZ", true);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "start")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, 2, -1, "XYZ", true);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "end")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, 2, 2, "XYZ", true);
            }
            catch (System.ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, 10, 2, "XYZ", true);
            }
            catch (System.ArgumentException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, 20, 4, "XYZ", true);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "start")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.ReplaceBetween(testValue, 2, 20, "XYZ", true);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "end")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void RightSubstring()
        {
            string testValue = "abcdefgabcdefg";

            string expected = "defgabcdefg";
            string actual = StringExtensions.RightSubstring(testValue, 'c');
            Assert.AreEqual(expected, actual);

            expected = "cdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 3);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "q");
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "q", 1);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "q", 2);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'q');
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'q', 1);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'q', 2);
            Assert.AreEqual(expected, actual);

            expected = "defgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "c");
            Assert.AreEqual(expected, actual);

            expected = "defgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'c', 1);
            Assert.AreEqual(expected, actual);

            expected = "defg";
            actual = StringExtensions.RightSubstring(testValue, 'c', 2);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 'c', 3);
            Assert.AreEqual(expected, actual);

            expected = "cdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 3, true);
            Assert.AreEqual(expected, actual);

            expected = "defgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, 3, false);
            Assert.AreEqual(expected, actual);

            expected = "defgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "c", 1);
            Assert.AreEqual(expected, actual);

            expected = "defg";
            actual = StringExtensions.RightSubstring(testValue, "c", 2);
            Assert.AreEqual(expected, actual);

            expected = "abcdefgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "c", 3);
            Assert.AreEqual(expected, actual);

            expected = "defgabcdefg";
            actual = StringExtensions.RightSubstring(testValue, "c", 1, StringComparison.InvariantCultureIgnoreCase);
            Assert.AreEqual(expected, actual);

            expected = "defg";
            actual = StringExtensions.RightSubstring(testValue, "c", 2, StringComparison.InvariantCultureIgnoreCase);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void RightSubstringExceptions()
        {
            string testValue = "abcdefgabcdefg";
            string actual;

            try
            {
                actual = StringExtensions.RightSubstring(null, "c");
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.RightSubstring(testValue, null);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "value")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.RightSubstring(null, 3);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.RightSubstring(testValue, -1);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "endingIndex")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.RightSubstring(testValue, 0);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "endingIndex")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.RightSubstring(testValue, 20);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                if (e.ParamName == "endingIndex")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }

            try
            {
                actual = StringExtensions.RightSubstring(null, 'c', 1);
            }
            catch (System.ArgumentNullException e)
            {
                if (e.ParamName == "source")
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail(e.Message);
                }
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ResizeString()
        {
            string testValue = "abcdefg";

            string expected = "abc";
            string actual = StringExtensions.ResizeString(testValue, 3);
            Assert.AreEqual(expected, actual);

            expected = "abcdefg     ";
            actual = StringExtensions.ResizeString(testValue, 12);
            Assert.AreEqual(expected, actual);

            expected = "            ";
            actual = StringExtensions.ResizeString(String.Empty, 12);
            Assert.AreEqual(expected, actual);

            expected = "            ";
            actual = StringExtensions.ResizeString(null, 12);
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void LengthLessThan()
        {
            Assert.IsTrue(StringExtensions.LengthLessThan("ABCD", 5));
            Assert.IsFalse(StringExtensions.LengthLessThan("ABCDE", 5));
            Assert.IsFalse(StringExtensions.LengthLessThan("ABCDEF", 5));
            Assert.IsTrue(StringExtensions.LengthLessThan(String.Empty, 5));

            try
            {
                Assert.IsFalse(StringExtensions.LengthLessThan(null, 5));
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LengthLessThanOrEqual()
        {
            Assert.IsTrue(StringExtensions.LengthLessThanOrEqualTo("ABCD", 5));
            Assert.IsTrue(StringExtensions.LengthLessThanOrEqualTo("ABCDE", 5));
            Assert.IsFalse(StringExtensions.LengthLessThanOrEqualTo("ABCDEF", 5));
            Assert.IsTrue(StringExtensions.LengthLessThanOrEqualTo(String.Empty, 5));

            try
            {
                Assert.IsFalse(StringExtensions.LengthLessThanOrEqualTo(null, 5));
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LengthGreaterThan()
        {
            Assert.IsTrue(StringExtensions.LengthGreaterThan("ABCDEFGHIJ", 5));
            Assert.IsFalse(StringExtensions.LengthGreaterThan("ABCDE", 5));
            Assert.IsFalse(StringExtensions.LengthGreaterThan("ABCD", 5));
            Assert.IsFalse(StringExtensions.LengthGreaterThan(String.Empty, 5));

            try
            {
                Assert.IsFalse(StringExtensions.LengthGreaterThan(null, 5));
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LengthGreaterThanOrEqualTo()
        {
            Assert.IsTrue(StringExtensions.LengthGreaterThanOrEqualTo("ABCDEFGHIJ", 5));
            Assert.IsTrue(StringExtensions.LengthGreaterThanOrEqualTo("ABCDE", 5));
            Assert.IsFalse(StringExtensions.LengthGreaterThanOrEqualTo("ABCD", 5));
            Assert.IsFalse(StringExtensions.LengthGreaterThanOrEqualTo(String.Empty, 5));
            
            try
            {
                Assert.IsFalse(StringExtensions.LengthGreaterThanOrEqualTo(null, 5));
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void LengthBetween()
        {
            Assert.IsTrue(StringExtensions.LengthBetween("ABCDE", 5, 10));
            Assert.IsTrue(StringExtensions.LengthBetween("ABCDEFGHIJ", 5, 10));
            Assert.IsTrue(StringExtensions.LengthBetween("ABCDEF", 5, 10));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCD", 5, 10));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCDEFGHIJK", 5, 10));

            Assert.IsFalse(StringExtensions.LengthBetween("ABCDE", 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(StringExtensions.LengthBetween("ABCDEFGHIJ", 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsTrue(StringExtensions.LengthBetween("ABCDEF", 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCD", 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCDEFGHIJK", 5, 10, NumericComparisonOptions.IncludeMaximum));

            Assert.IsTrue(StringExtensions.LengthBetween("ABCDE", 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCDEFGHIJ", 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsTrue(StringExtensions.LengthBetween("ABCDEF", 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCD", 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCDEFGHIJK", 5, 10, NumericComparisonOptions.IncludeMinimum));

            Assert.IsFalse(StringExtensions.LengthBetween("ABCDE", 5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCDEFGHIJ", 5, 10, NumericComparisonOptions.None));
            Assert.IsTrue(StringExtensions.LengthBetween("ABCDEF", 5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCD", 5, 10, NumericComparisonOptions.None));
            Assert.IsFalse(StringExtensions.LengthBetween("ABCDEFGHIJK", 5, 10, NumericComparisonOptions.None));

            Assert.IsFalse(StringExtensions.LengthBetween(String.Empty, 5, 10));
            Assert.IsFalse(StringExtensions.LengthBetween(String.Empty, 5, 10, NumericComparisonOptions.IncludeMaximum));
            Assert.IsFalse(StringExtensions.LengthBetween(String.Empty, 5, 10, NumericComparisonOptions.IncludeMinimum));
            Assert.IsFalse(StringExtensions.LengthBetween(String.Empty, 5, 10, NumericComparisonOptions.None));

            try
            {
                Assert.IsFalse(StringExtensions.LengthBetween(null, 5, 10));
            }
            catch (System.ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
