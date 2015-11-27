using Cadru.Globalization;
using Cadru.UnitTest.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cadru.Core.UnitTests.Globalization
{
    [TestClass, ExcludeFromCodeCoverage]
    public class LocalizbleStringTests
    {
        public class ResourceTester
        {
            public static string Valid { get { return "Valid"; } }

            internal string Invalid { get { return "Invalid"; } }

            public string Invalid2 { set {  } }

            public static string Invalid3 { set { } }

            public static string Invalid4 { private get { return ""; } set { } }

            public string Invalid5 { private get { return ""; } set { } }

            internal static string InvalidStatic { get { return "Invalid"; } }
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
            var localized = new LocalizableString("Width");
            localized.Value = "100px";
            Assert.AreEqual("100px", localized.Value);
            Assert.AreEqual("100px", localized.GetLocalizableValue());
        }

        [TestMethod]
        public void InvalidResource()
        {
            var localized = new LocalizableString("RelativeDateFormatStringTomorrow");
            localized.ResourceType = typeof(Properties.Resources);
            localized.Value = "RelativeDateFormatStringYesterday";
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
            var localized = new LocalizableString("RelativeDateFormatStringTomorrow");
            localized.ResourceType = typeof(Properties.Resources);
            Assert.IsNotNull(localized.ResourceType);
            Assert.IsNull(localized.GetLocalizableValue());
        }

        [TestMethod]
        public void LocalizedValue()
        {
            var localized = new LocalizableString("RelativeDateFormatStringTomorrow");
            localized.Value = "RelativeDateFormatStringTomorrow";
            Assert.IsNull(localized.ResourceType);
            Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.Value);
            Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.GetLocalizableValue());

            localized.ResourceType = typeof(Properties.Resources);
            Assert.IsNotNull(localized.ResourceType);
            Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.Value);
            Assert.AreEqual("Tomorrow", localized.GetLocalizableValue());

            var currentCulture = Thread.CurrentThread.CurrentUICulture;
            try
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");
                localized.ResourceType = typeof(Properties.Resources);
                Assert.IsNotNull(localized.ResourceType);
                Assert.AreEqual("RelativeDateFormatStringTomorrow", localized.Value);
                Assert.AreEqual("Mañana", localized.GetLocalizableValue());
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = currentCulture;
            }
        }

    }
}
