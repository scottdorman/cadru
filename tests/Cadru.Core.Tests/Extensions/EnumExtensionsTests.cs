//------------------------------------------------------------------------------
// <copyright file="EnumExtensionsTests.cs"
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
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Cadru.Extensions;
using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
{
    public enum Test
    {
        [EnumDescription(".NET Framework 1.0")]
        Value1,

        [System.ComponentModel.Description(".NET Framework 1.0 (From Description)")]
        Value2,

        [Display(Name = ".NET Framework 1.0 (From Display)")]
        Value3,

        [EnumDescription(".NET Framework 1.0")]
        [System.ComponentModel.Description(".NET Framework 1.0 (From Description)")]
        [Display(Name = ".NET Framework 1.0 (From Display)")]
        Value4
    }

    [TestClass, ExcludeFromCodeCoverage]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void EnumPassthrough()
        {
            Assert.AreEqual("Value1", Enum<Test>.GetName(Test.Value1));
            CollectionAssert.AreEqual(new[] { "Value1", "Value2", "Value3", "Value4" }, Enum<Test>.GetNames().ToArray());
            Assert.AreEqual(typeof(int).Name, Enum<Test>.GetUnderlyingType().Name);
            CollectionAssert.AreEqual(new[] { Test.Value1, Test.Value2, Test.Value3, Test.Value4 }, Enum<Test>.GetValues().ToArray());
            Assert.IsTrue(Enum<Test>.IsDefined("Value1"));
            Assert.IsFalse(Enum<Test>.IsDefined("Value5"));
            Assert.AreEqual(Test.Value1, Enum<Test>.ToEnum(0));
        }

        [TestMethod]
        public void EnumTGetDescription()
        {
            Enum value = null;

            string actual;
            ExceptionAssert.Throws<ArgumentNullException>(() => actual = value.GetDescription()).WithParameter("value");

            // This enum has the Flags attribute,
            // but not a Description attribute, so the description should
            // be the enum value.
            Assert.AreEqual("None", Enum<System.Net.DecompressionMethods>.GetDescription(System.Net.DecompressionMethods.None));
            Assert.AreEqual(null, Enum<System.Net.DecompressionMethods>.GetDescription(System.Net.DecompressionMethods.None, false));

            // This enum has no attributes, so the description should be the enum value.
            Assert.AreEqual("SafeUnescaped", Enum<UriFormat>.GetDescription(UriFormat.SafeUnescaped));
            Assert.AreEqual(null, Enum<UriFormat>.GetDescription(UriFormat.SafeUnescaped, false));

            // This enum value has two attributes that match the prototype.
            Assert.AreEqual(".NET Framework 1.0", Enum<Test>.GetDescription(Test.Value1));
            Assert.AreEqual(".NET Framework 1.0", Enum<Test>.GetDescription(Test.Value1, false));

            Assert.AreEqual(".NET Framework 1.0 (From Description)", Enum<Test>.GetDescription(Test.Value2));
            Assert.AreEqual(".NET Framework 1.0 (From Description)", Enum<Test>.GetDescription(Test.Value2, false));

            Assert.AreEqual(".NET Framework 1.0 (From Display)", Enum<Test>.GetDescription(Test.Value3));
            Assert.AreEqual(".NET Framework 1.0 (From Display)", Enum<Test>.GetDescription(Test.Value3, false));

            Assert.AreEqual(".NET Framework 1.0", Enum<Test>.GetDescription(Test.Value4));
            Assert.AreEqual(".NET Framework 1.0", Enum<Test>.GetDescription(Test.Value4, false));
        }

        [TestMethod]
        public void EnumTGetDescriptions()
        {
            Enum value = null;

            string actual;
            ExceptionAssert.Throws<ArgumentNullException>(() => actual = value.GetDescription()).WithParameter("value");

            // This enum has the Flags and ComVisible(true) attributes,
            // but not a Description attribute, so the description should
            // be the enum value.
            var expectedValues = this.GetExpectedValues<System.Net.DecompressionMethods>();
            CollectionAssert.AreEqual(expectedValues.Item1, Enum<System.Net.DecompressionMethods>.GetDescriptions().ToArray());
            CollectionAssert.AreEqual(expectedValues.Item2, Enum<System.Net.DecompressionMethods>.GetDescriptions(false).ToArray());

            // This enum has no attributes, so the description should be the enum value.
            expectedValues = this.GetExpectedValues<UriFormat>();

            // The OrderBy calls seem to be necessary for this to pass when running tests normally.
            // Without them, the next test passes when run under the debugger, but fails otherwise
            // because the collection apparently comes back in a different order.
            CollectionAssert.AreEqual(expectedValues.Item1.OrderBy(v => v).ToArray(), Enum<UriFormat>.GetDescriptions().OrderBy(v => v).ToArray());
            CollectionAssert.AreEqual(expectedValues.Item2, Enum<UriFormat>.GetDescriptions(false).ToArray());

            // This enum value has two attributes that match the prototype.
            var expectedDescriptions = new[] {
                ".NET Framework 1.0",
                ".NET Framework 1.0 (From Description)",
                ".NET Framework 1.0 (From Display)",
                ".NET Framework 1.0"
            };

            CollectionAssert.AreEqual(expectedDescriptions, Enum<Test>.GetDescriptions().ToArray());
            CollectionAssert.AreEqual(expectedDescriptions, Enum<Test>.GetDescriptions(false).ToArray());
        }

        [TestMethod]
        public void GetDescription()
        {
            Enum value = null;

            string actual;
            ExceptionAssert.Throws<ArgumentNullException>(() => actual = value.GetDescription()).WithParameter("value");

            // This enum has the Flags attribute,
            // but not a Description attribute, so the description should
            // be the enum value.
            Assert.AreEqual("None", System.Net.DecompressionMethods.None.GetDescription());
            Assert.AreEqual(null, System.Net.DecompressionMethods.None.GetDescription(false));

            // This enum has no attributes, so the description should be the enum value.
            Assert.AreEqual("SafeUnescaped", UriFormat.SafeUnescaped.GetDescription());
            Assert.AreEqual(null, UriFormat.SafeUnescaped.GetDescription(false));

            // This enum value has two attributes that match the prototype.
            Assert.AreEqual(".NET Framework 1.0", Test.Value1.GetDescription());
            Assert.AreEqual(".NET Framework 1.0", Test.Value1.GetDescription(false));

            Assert.AreEqual(".NET Framework 1.0", Test.Value1.GetDescription());
            Assert.AreEqual(".NET Framework 1.0", Test.Value1.GetDescription(false));
        }

        [TestMethod]
        public void Parse()
        {
            Assert.AreEqual(Test.Value1, Enum<Test>.Parse("Value1"));
            Assert.AreEqual(Test.Value1, Enum<Test>.Parse("value1", true));
        }

        [TestMethod]
        public void TryParse()
        {
            Assert.IsTrue(Enum<Test>.TryParse("Value1", out var _));
            Assert.IsTrue(Enum<Test>.TryParse("value1", true, out _));
            Assert.IsFalse(Enum<Test>.TryParse("value1", false, out _));
            Assert.IsFalse(Enum<Test>.TryParse("test", true, out _));
            Assert.IsFalse(Enum<Test>.TryParse("test", false, out _));
        }

        private (string[], string[]) GetExpectedValues<T>() where T : struct
        {
            var expected = Enum.GetNames(typeof(T));
            var emptyExpected = new string[expected.Count()];
            emptyExpected.AsSpan().Fill(null);
            return (expected, emptyExpected);
        }
    }
}