using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;
using Cadru.UnitTest.Framework;
using System.Text;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class StringBuilderExtensionsTests
    {
        [TestMethod]
        public void AppendIf()
        {
            Assert.AreEqual("True", new StringBuilder().AppendIf(true, true).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, true).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (byte)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (byte)4).ToString());

            Assert.AreEqual("a", new StringBuilder().AppendIf(true, 'a').ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, 'a').ToString());

            Assert.AreEqual("ab", new StringBuilder().AppendIf(true, new char[] { 'a', 'b' }).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, new char[] { 'a', 'b' }).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (double)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (double)4).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (float)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (float)4).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (int)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (int)4).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (long)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (long)4).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (sbyte)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (sbyte)4).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (short)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (short)4).ToString());
            
            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (uint)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (uint)4).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (ulong)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (ulong)4).ToString());

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, (ushort)4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (ushort)4).ToString());

            Assert.AreEqual("test", new StringBuilder().AppendIf(true, (object)"test").ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, (object)"test").ToString());

            Assert.AreEqual("test", new StringBuilder().AppendIf(true, "test").ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, "test").ToString());

            Assert.AreEqual("test", new StringBuilder().AppendIf(true, "test", 0, 4) .ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, "test", 0, 4).ToString());
            Assert.AreEqual("est", new StringBuilder().AppendIf(true, "test", 1, 3).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, "test", 1, 3).ToString());

            Assert.AreEqual("aaa", new StringBuilder().AppendIf(true, 'a', 3).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, 'a', 3).ToString());

            Assert.AreEqual("a", new StringBuilder().AppendIf(true, new char[] { 'a', 'b' }, 0, 1).ToString());
            Assert.AreEqual("ab", new StringBuilder().AppendIf(true, new char[] { 'a', 'b' }, 0, 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, new char[] { 'a', 'b' }, 0, 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, new char[] { 'a', 'b' }, 0, 1).ToString());
            Assert.AreEqual("b", new StringBuilder().AppendIf(true, new char[] { 'a', 'b' }, 1, 1).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, new char[] { 'a', 'b' }, 1, 1).ToString());
        }

        [TestMethod]
        public void AppendFormatIf()
        {
            Assert.AreEqual("test 2", new StringBuilder().AppendFormatIf(true, "test {0}", 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendFormatIf(false, "test {0}", 2).ToString());

            Assert.AreEqual("test 2", new StringBuilder().AppendFormatIf(true, CultureInfo.CurrentCulture, "test {0}", 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendFormatIf(false, CultureInfo.CurrentCulture, "test {0}", 2).ToString());
        }

        [TestMethod]
        public void AppendFormatLine()
        {
            Assert.AreEqual(@"test 2
", new StringBuilder().AppendFormatLine("test {0}", 2).ToString());
            Assert.AreEqual(@"test 2
", new StringBuilder().AppendFormatLine(CultureInfo.CurrentCulture, "test {0}", 2).ToString());
        }

        [TestMethod]
        public void AppendFormatLineIf()
        {
            Assert.AreEqual(@"test 2
", new StringBuilder().AppendFormatLineIf(true, "test {0}", 2).ToString());
            Assert.AreEqual(@"test 2
", new StringBuilder().AppendFormatLineIf(true, CultureInfo.CurrentCulture, "test {0}", 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendFormatLineIf(false, "test {0}", 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendFormatLineIf(false, CultureInfo.CurrentCulture, "test {0}", 2).ToString());
        }

        [TestMethod]
        public void AppendLineIf()
        {
            Assert.AreEqual(@"test
", new StringBuilder().AppendLineIf(true, "test").ToString());
            Assert.AreEqual("", new StringBuilder().AppendLineIf(false, "test").ToString());

            Assert.AreEqual(@"
", new StringBuilder().AppendLineIf(true).ToString());
            Assert.AreEqual("", new StringBuilder().AppendLineIf(false).ToString());
        }
    }
}
