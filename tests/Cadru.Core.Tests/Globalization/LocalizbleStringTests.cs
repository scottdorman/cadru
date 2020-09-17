//------------------------------------------------------------------------------
// <copyright file="LocalizbleStringTests.cs"
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

using Cadru.Core.Tests.Resources;
using Cadru.Globalization;
using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Globalization.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class LocalizbleStringTests
    {
        [TestMethod]
        public void Constructor()
        {
            var localized = new LocalizableString("Width");
            Assert.IsNotNull(localized);
            Assert.IsInstanceOfType(localized, typeof(LocalizableString));
            Assert.IsNull(localized.ResourceType);

            Assert.ThrowsException<ArgumentException>(() => new LocalizableString(""));
            Assert.ThrowsException<ArgumentNullException>(() => new LocalizableString(null));
            Assert.ThrowsException<ArgumentException>(() => new LocalizableString("    "));
        }

        [TestMethod]
        public void InvalidResource()
        {
            var localized = new LocalizableString("RelativeDateFormatStringTomorrow")
            {
                ResourceType = typeof(Strings),
                Value = "RelativeDateFormatStringYesterday"
            };
            Assert.AreEqual("RelativeDateFormatStringYesterday", localized.Value);
            Assert.IsNotNull(localized.ResourceType);

            Assert.ThrowsException<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.ResourceType = typeof(LocalizbleStringTests.ResourceTester);
            localized.Value = "Invalid";
            Assert.ThrowsException<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "InvalidStatic";
            Assert.ThrowsException<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid2";
            Assert.ThrowsException<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid3";
            Assert.ThrowsException<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid4";
            Assert.ThrowsException<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid5";
            Assert.ThrowsException<InvalidOperationException>(() => localized.GetLocalizableValue());
        }

        [TestMethod]
        public void LocalizedValue()
        {
            var localized = new LocalizableString("RelativeDateFormatStringTomorrow")
            {
                Value = "RelativeDateFormatStringTomorrow"
            };
            Assert.IsNull(localized.ResourceType);
            Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.Value);
            Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.GetLocalizableValue());

            localized.ResourceType = typeof(Strings);
            Assert.IsNotNull(localized.ResourceType);
            Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.Value);
            Assert.AreEqual("Tomorrow", localized.GetLocalizableValue());

            var currentCulture = CultureInfo.CurrentCulture;
            var currentUICulture = CultureInfo.CurrentUICulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("es-ES");
                CultureInfo.CurrentUICulture = new CultureInfo("es-ES");
                localized.ResourceType = typeof(Strings);
                Assert.IsNotNull(localized.ResourceType);
                Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.Value);
                Assert.AreEqual("Mañana", localized.GetLocalizableValue());
            }
            finally
            {
                CultureInfo.CurrentCulture = currentCulture;
                CultureInfo.CurrentUICulture = currentUICulture;
            }
        }

        [TestMethod]
        public void LocalValue()
        {
            var localized = new LocalizableString("Width")
            {
                Value = "100px"
            };
            Assert.AreEqual("100px", localized.Value);
            Assert.AreEqual("100px", localized.GetLocalizableValue());
        }

        [TestMethod]
        public void MissingValue()
        {
            var localized = new LocalizableString("RelativeDateFormatStringTomorrow")
            {
                ResourceType = typeof(Strings)
            };
            Assert.IsNotNull(localized.ResourceType);
            Assert.IsNull(localized.GetLocalizableValue());
        }

        public class ResourceTester
        {
            public static string Invalid3 { set { } }
            public static string Invalid4 { private get => ""; set { } }
            public static string Valid => "Valid";

            public string Invalid2 { set { } }
            public string Invalid5 { private get => ""; set { } }
            internal static string InvalidStatic => "Invalid";
            internal string Invalid => "Invalid";
        }
    }
}