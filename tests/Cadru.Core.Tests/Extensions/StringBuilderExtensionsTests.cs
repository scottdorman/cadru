//------------------------------------------------------------------------------
// <copyright file="StringBuilderExtensionsTests.cs"
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
using System.Globalization;
using System.Text;

using Cadru.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class StringBuilderExtensionsTests
    {
        [TestMethod]
        public void AppendAsHexadecimal()
        {
            Assert.AreEqual("04", new StringBuilder().AppendAsHexadecimal((byte)4).ToString());
            Assert.AreEqual("4b", new StringBuilder().AppendAsHexadecimal((byte)75).ToString());

            Assert.AreEqual("00000004", new StringBuilder().AppendAsHexadecimal(4).ToString());
            Assert.AreEqual("0000004b", new StringBuilder().AppendAsHexadecimal(75).ToString());

            Assert.AreEqual("0004", new StringBuilder().AppendAsHexadecimal((short)4).ToString());
            Assert.AreEqual("004b", new StringBuilder().AppendAsHexadecimal((short)75).ToString());

            Assert.AreEqual("044b", new StringBuilder().AppendAsHexadecimal(new byte[] { 4, 75 }).ToString());
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
            Assert.AreEqual(@"test 2" + Environment.NewLine, new StringBuilder().AppendFormatLine("test {0}", 2).ToString());
            Assert.AreEqual(@"test 2" + Environment.NewLine, new StringBuilder().AppendFormatLine(CultureInfo.CurrentCulture, "test {0}", 2).ToString());
        }

        [TestMethod]
        public void AppendFormatLineIf()
        {
            Assert.AreEqual(@"test 2" + Environment.NewLine, new StringBuilder().AppendFormatLineIf(true, "test {0}", 2).ToString());
            Assert.AreEqual(@"test 2" + Environment.NewLine, new StringBuilder().AppendFormatLineIf(true, CultureInfo.CurrentCulture, "test {0}", 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendFormatLineIf(false, "test {0}", 2).ToString());
            Assert.AreEqual("", new StringBuilder().AppendFormatLineIf(false, CultureInfo.CurrentCulture, "test {0}", 2).ToString());
        }

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

            Assert.AreEqual("4", new StringBuilder().AppendIf(true, 4).ToString());
            Assert.AreEqual("", new StringBuilder().AppendIf(false, 4).ToString());

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

            Assert.AreEqual("test", new StringBuilder().AppendIf(true, "test", 0, 4).ToString());
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
        public void AppendLineIf()
        {
            Assert.AreEqual(@"test" + Environment.NewLine, new StringBuilder().AppendLineIf(true, "test").ToString());
            Assert.AreEqual("", new StringBuilder().AppendLineIf(false, "test").ToString());

            Assert.AreEqual("" + Environment.NewLine, new StringBuilder().AppendLineIf(true).ToString());
            Assert.AreEqual("", new StringBuilder().AppendLineIf(false).ToString());
        }
    }
}