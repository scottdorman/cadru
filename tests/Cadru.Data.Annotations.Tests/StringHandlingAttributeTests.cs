
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Annotations.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class StringHandlingAttributeTests
    {
        [TestMethod]
        public void BasicTests()
        {
            var attribute = new StringHandlingAttribute(StringHandlingOption.Trim);
            Assert.AreEqual(StringHandlingOption.Trim, attribute.StringHandlingOption);
        }
    }
}
