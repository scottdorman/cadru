using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    public enum TestEnum
    {

        [EnumDescription(".NET Framework 1.0")]
        TestValue1,
    }

    [TestClass, ExcludeFromCodeCoverage]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void GetDescription()
        {
            Enum value = null;

            string expected = null;
            string actual;
            ExceptionAssert.Throws<ArgumentNullException>(() => actual = value.GetDescription()).WithParameter("value");

            // This enum has the Flags and ComVisible(true) attributes, 
            // but not a Description attribute, so the description should
            // be the enum value.
            value = System.AppDomainManagerInitializationOptions.None;
            actual = value.GetDescription();
            expected = "None";

            Assert.AreEqual(expected, actual);

            // This enum has no attributes, so the description should be the enum value.
            value = System.UriFormat.SafeUnescaped;
            actual = value.GetDescription();
            expected = "SafeUnescaped";

            Assert.AreEqual(expected, actual);

            // This enum value has two attributes that match the prototype.
            value = TestEnum.TestValue1;

            expected = ".NET Framework 1.0";
            actual = value.GetDescription();

            Assert.AreEqual(expected, actual);
        }
    }
}
