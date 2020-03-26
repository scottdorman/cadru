
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Data.Annotations.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExportableAttributeTests
    {
        [TestMethod]
        public void BasicTests()
        {
            Assert.IsTrue(new ExportableAttribute(true).AllowExport);
            Assert.IsFalse(new ExportableAttribute(false).AllowExport);
            Assert.AreEqual(100, new ExportableAttribute(true) { Order = 100 }.Order);
            Assert.AreEqual(ExportableAttribute.DefaultOrder, new ExportableAttribute(true).Order);
        }
    }
}
