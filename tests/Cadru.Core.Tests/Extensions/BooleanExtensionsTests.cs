//------------------------------------------------------------------------------
// <copyright file="BooleanExtensionsTests.cs"
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

using System.Diagnostics.CodeAnalysis;

using Cadru.Extensions;
using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BooleanExtensionsTests
    {
        [TestMethod]
        public void ToBinary()
        {
            Assert.IsTrue(true.ToBit() == 1);
            Assert.IsFalse(false.ToBit() == 1);
            Assert.IsFalse(true.ToBit() == 0);
            Assert.IsTrue(false.ToBit() == 0);
        }

        [TestMethod]
        public void ToChar()
        {
            Assert.IsTrue(true.ToChar() == 'T');
            Assert.IsFalse(false.ToChar() == 'T');
            Assert.IsFalse(true.ToChar() == 'F');
            Assert.IsTrue(false.ToChar() == 'F');
        }

        [TestMethod]
        public void ToLower()
        {
            Assert.IsTrue(true.ToLower() == System.Boolean.TrueString.ToLower());
            Assert.IsTrue(false.ToLower() == System.Boolean.FalseString.ToLower());
        }

        [TestMethod]
        public void ToUpper()
        {
            Assert.IsTrue(true.ToUpper() == System.Boolean.TrueString.ToUpper());
            Assert.IsTrue(false.ToUpper() == System.Boolean.FalseString.ToUpper());
        }

        [TestMethod]
        public void TryParse()
        {
            ConditionAssert.IsTrue(this.TryParse("true"));
            ConditionAssert.IsTrue(this.TryParse("True"));
            ConditionAssert.IsTrue(this.TryParse("TRUE"));
            ConditionAssert.IsTrue(this.TryParse("T"));
            ConditionAssert.IsTrue(this.TryParse("t"));
            ConditionAssert.IsTrue(this.TryParse("Y"));
            ConditionAssert.IsTrue(this.TryParse("y"));
            ConditionAssert.IsTrue(this.TryParse("YES"));
            ConditionAssert.IsTrue(this.TryParse("Yes"));
            ConditionAssert.IsTrue(this.TryParse("yes"));

            ConditionAssert.IsFalse(this.TryParse("false"));
            ConditionAssert.IsFalse(this.TryParse("False"));
            ConditionAssert.IsFalse(this.TryParse("FALSE"));
            ConditionAssert.IsFalse(this.TryParse("F"));
            ConditionAssert.IsFalse(this.TryParse("f"));
            ConditionAssert.IsFalse(this.TryParse("N"));
            ConditionAssert.IsFalse(this.TryParse("n"));
            ConditionAssert.IsFalse(this.TryParse("NO"));
            ConditionAssert.IsFalse(this.TryParse("No"));
            ConditionAssert.IsFalse(this.TryParse("no"));
            ConditionAssert.IsFalse(this.TryParse("NA"));
            ConditionAssert.IsFalse(this.TryParse("Na"));
            ConditionAssert.IsFalse(this.TryParse("na"));
            ConditionAssert.IsFalse(this.TryParse("N/A"));
            ConditionAssert.IsFalse(this.TryParse("N/a"));
            ConditionAssert.IsFalse(this.TryParse("n/a"));

            ConditionAssert.IsTrue(this.TryParse("true "));
            ConditionAssert.IsTrue(this.TryParse(" True"));
            ConditionAssert.IsTrue(this.TryParse("TRUE "));
            ConditionAssert.IsTrue(this.TryParse("T "));
            ConditionAssert.IsTrue(this.TryParse(" t"));
            ConditionAssert.IsTrue(this.TryParse("Y "));
            ConditionAssert.IsTrue(this.TryParse(" y"));
            ConditionAssert.IsTrue(this.TryParse("YES "));
            ConditionAssert.IsTrue(this.TryParse(" Yes"));
            ConditionAssert.IsTrue(this.TryParse(" yes "));

            ConditionAssert.IsFalse(this.TryParse(" false"));
            ConditionAssert.IsFalse(this.TryParse("False "));
            ConditionAssert.IsFalse(this.TryParse("FALSE "));
            ConditionAssert.IsFalse(this.TryParse(" F"));
            ConditionAssert.IsFalse(this.TryParse("f "));
            ConditionAssert.IsFalse(this.TryParse("N "));
            ConditionAssert.IsFalse(this.TryParse(" n"));
            ConditionAssert.IsFalse(this.TryParse("NO "));
            ConditionAssert.IsFalse(this.TryParse(" No"));
            ConditionAssert.IsFalse(this.TryParse("no "));
            ConditionAssert.IsFalse(this.TryParse("NA "));
            ConditionAssert.IsFalse(this.TryParse(" Na"));
            ConditionAssert.IsFalse(this.TryParse("na "));
            ConditionAssert.IsFalse(this.TryParse(" N/A "));
            ConditionAssert.IsFalse(this.TryParse(" N/a"));
            ConditionAssert.IsFalse(this.TryParse("n/a "));

            ConditionAssert.IsTrue(this.TryParse(1));
            ConditionAssert.IsFalse(this.TryParse(0));

            Assert.IsNull(this.TryParse("foo"));
            Assert.IsNull(this.TryParse(" foo"));
            Assert.IsNull(this.TryParse(3));

            Assert.IsNull(this.TryParse(""));
            Assert.IsNull(this.TryParse("\0"));
            Assert.IsNull(this.TryParse("\u0089"));
            Assert.IsNull(this.TryParse("\t"));
            Assert.IsNull(this.TryParse("\u0100"));
            Assert.IsNull(this.TryParse("\u0089\t"));
            Assert.IsNull(this.TryParse("\u0089\0"));
            Assert.IsNull(this.TryParse("\t\0"));
            Assert.IsNull(this.TryParse("\t\0\0"));
            Assert.IsNull(this.TryParse("\u0100\0\0"));
            Assert.IsNull(this.TryParse("\u0100\t\t"));
            Assert.IsNull(this.TryParse("\t\t"));
        }

        private bool? TryParse(string value)
        {
            if (value.TryParseAsBoolean(out var result))
            {
                return result;
            }

            return null;
        }

        private bool? TryParse(int value)
        {
            if (value.TryParseAsBoolean(out var result))
            {
                return result;
            }

            return null;
        }
    }
}