using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

        [TestMethod]
        public void Parse()
        {
            Assert.AreEqual(TestEnum.TestValue1, Enum<TestEnum>.Parse("TestValue1"));
            Assert.AreEqual(TestEnum.TestValue1, Enum<TestEnum>.Parse("testvalue1", true));
        }

        [TestMethod]
        public void TryParse()
        {
            TestEnum result;
            Assert.IsTrue(Enum<TestEnum>.TryParse("TestValue1", out result));
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
