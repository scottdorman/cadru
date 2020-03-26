
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Annotations.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExtendedPropertyAttributeTests
    {
        [TestMethod]
        public void BasicTests()
        {
            var attribute = new ExtendedPropertyAttribute("test", 5);
            Assert.AreEqual("test", attribute.Name);
            Assert.AreEqual(5, attribute.Value);
        }
    }
}
