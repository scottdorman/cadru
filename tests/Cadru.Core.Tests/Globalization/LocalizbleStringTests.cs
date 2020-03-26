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
        public class ResourceTester
        {
            public static string Valid => "Valid";

            internal string Invalid => "Invalid";

            public string Invalid2 { set { } }

            public static string Invalid3 { set { } }

            public static string Invalid4 { private get => ""; set { } }

            public string Invalid5 { private get => ""; set { } }

            internal static string InvalidStatic => "Invalid";
        }

        [TestMethod]
        public void Constructor()
        {
            var localized = new LocalizableString("Width");
            Assert.IsNotNull(localized);
            Assert.IsInstanceOfType(localized, typeof(LocalizableString));
            Assert.IsNull(localized.ResourceType);

            ExceptionAssert.Throws<ArgumentException>(() => new LocalizableString(""));
            ExceptionAssert.Throws<ArgumentNullException>(() => new LocalizableString(null));
            ExceptionAssert.Throws<ArgumentException>(() => new LocalizableString("    "));
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
        public void InvalidResource()
        {
            var localized = new LocalizableString("RelativeDateFormatStringTomorrow")
            {
                ResourceType = typeof(Strings),
                Value = "RelativeDateFormatStringYesterday"
            };
            Assert.AreEqual("RelativeDateFormatStringYesterday", localized.Value);
            Assert.IsNotNull(localized.ResourceType);

            ExceptionAssert.Throws<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.ResourceType = typeof(LocalizbleStringTests.ResourceTester);
            localized.Value = "Invalid";
            ExceptionAssert.Throws<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "InvalidStatic";
            ExceptionAssert.Throws<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid2";
            ExceptionAssert.Throws<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid3";
            ExceptionAssert.Throws<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid4";
            ExceptionAssert.Throws<InvalidOperationException>(() => localized.GetLocalizableValue());

            localized.Value = "Invalid5";
            ExceptionAssert.Throws<InvalidOperationException>(() => localized.GetLocalizableValue());
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

    }
}
