using System;
using System.Diagnostics.CodeAnalysis;

using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Annotations.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ViewAttributeTests
    {
        [TestMethod]
        public void BasicTests()
        {
            var attribute = new ViewAttribute("test");
            Assert.AreEqual("test", attribute.Name);
            Assert.IsTrue(String.IsNullOrWhiteSpace(attribute.Schema));

            attribute = new ViewAttribute("test") { Schema = "dbo" };
            Assert.AreEqual("test", attribute.Name);
            Assert.AreEqual("dbo", attribute.Schema);

            ExceptionAssert.Throws<ArgumentNullException>(() => new ViewAttribute(null));
            ExceptionAssert.Throws<ArgumentException>(() => new ViewAttribute(String.Empty));

            ExceptionAssert.Throws<ArgumentNullException>(() => new ViewAttribute("test") { Schema = null });
            ExceptionAssert.Throws<ArgumentException>(() => new ViewAttribute("test") { Schema = String.Empty });
        }
    }
}
