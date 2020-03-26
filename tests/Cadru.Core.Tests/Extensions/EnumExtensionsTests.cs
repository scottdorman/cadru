using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using Cadru.Extensions;
using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Extensions.Tests
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

            string actual;
            ExceptionAssert.Throws<ArgumentNullException>(() => actual = value.GetDescription()).WithParameter("value");

            // This enum has the Flags attribute,
            // but not a Description attribute, so the description should
            // be the enum value.
            Assert.AreEqual("None", System.Net.DecompressionMethods.None.GetDescription());
            Assert.AreEqual(null, System.Net.DecompressionMethods.None.GetDescription(false));

            // This enum has no attributes, so the description should be the enum value.
            Assert.AreEqual("SafeUnescaped", System.UriFormat.SafeUnescaped.GetDescription());
            Assert.AreEqual(null, System.UriFormat.SafeUnescaped.GetDescription(false));

            // This enum value has two attributes that match the prototype.
            Assert.AreEqual(".NET Framework 1.0", TestEnum.TestValue1.GetDescription());
            Assert.AreEqual(".NET Framework 1.0", TestEnum.TestValue1.GetDescription(false));
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
            Assert.AreEqual(".NET Framework 1.0", Enum<TestEnum>.GetDescription(TestEnum.TestValue1));
            Assert.AreEqual(".NET Framework 1.0", Enum<TestEnum>.GetDescription(TestEnum.TestValue1, false));
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
            CollectionAssert.AreEqual(new[] { "None", "GZip", "Deflate" }, Enum<System.Net.DecompressionMethods>.GetDescriptions().ToArray());
            CollectionAssert.AreEqual(new string[] { null, null, null }, Enum<System.Net.DecompressionMethods>.GetDescriptions(false).ToArray());

            // This enum has no attributes, so the description should be the enum value.
            CollectionAssert.AreEqual(new[] { "UriEscaped", "Unescaped", "SafeUnescaped" }, Enum<UriFormat>.GetDescriptions().ToArray());
            CollectionAssert.AreEqual(new string[] { null, null, null }, Enum<UriFormat>.GetDescriptions(false).ToArray());

            // This enum value has two attributes that match the prototype.
            CollectionAssert.AreEqual(new[] { ".NET Framework 1.0" }, Enum<TestEnum>.GetDescriptions().ToArray());
            CollectionAssert.AreEqual(new[] { ".NET Framework 1.0" }, Enum<TestEnum>.GetDescriptions(false).ToArray());
        }

        [TestMethod]
        public void Parse()
        {
            Assert.AreEqual(TestEnum.TestValue1, Enum<TestEnum>.Parse("TestValue1"));
            Assert.AreEqual(TestEnum.TestValue1, Enum<TestEnum>.Parse("testvalue1", true));
        }

        [TestMethod]
        public void TryParse()
        {
            Assert.IsTrue(Enum<TestEnum>.TryParse("TestValue1", out var result));
            Assert.IsTrue(Enum<TestEnum>.TryParse("testvalue1", true, out result));
            Assert.IsFalse(Enum<TestEnum>.TryParse("testvalue1", false, out result));
            Assert.IsFalse(Enum<TestEnum>.TryParse("test", true, out result));
            Assert.IsFalse(Enum<TestEnum>.TryParse("test", false, out result));
        }

        [TestMethod]
        public void EnumPassthrough()
        {
            Assert.AreEqual("TestValue1", Enum<TestEnum>.GetName(TestEnum.TestValue1));
            CollectionAssert.AreEqual(new[] { "TestValue1" }, Enum<TestEnum>.GetNames().ToArray());
            Assert.AreEqual(typeof(int).Name, Enum<TestEnum>.GetUnderlyingType().Name);
            CollectionAssert.AreEqual(new[] { TestEnum.TestValue1 }, Enum<TestEnum>.GetValues().ToArray());
            Assert.IsTrue(Enum<TestEnum>.IsDefined("TestValue1"));
            Assert.IsFalse(Enum<TestEnum>.IsDefined("TestValue2"));
            Assert.AreEqual(TestEnum.TestValue1, Enum<TestEnum>.ToEnum(0));
        }
    }
}
