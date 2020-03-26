using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.Tests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TypeAssertTests
    {
        [TestMethod]
        public void IsAssignableFrom()
        {
            var array10 = new int[10];
            var array2 = new int[2];

            TypeAssert.IsAssignableFrom(array10, array2.GetType());
            TypeAssert.IsAssignableFrom(array10, array2.GetType(), "Type Failure Message");
            TypeAssert.IsAssignableFrom(array10, array2.GetType(), "Type Failure Message", null);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsAssignableFromFails()
        {
            var array10 = new int[10];
            var array2 = new int[2, 2];

            TypeAssert.IsAssignableFrom(array10, array2.GetType());
        }

        [TestMethod]
        public void IsNotAssignableFrom()
        {
            var array10 = new int[10];
            var array2 = new int[2, 2];

            TypeAssert.IsNotAssignableFrom(array10, array2.GetType());
            TypeAssert.IsNotAssignableFrom(array10, array2.GetType(), "Type Failure Message");
            TypeAssert.IsNotAssignableFrom(array10, array2.GetType(), "Type Failure Message", null);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsNotAssignableFromFails()
        {
            var array10 = new int[10];
            var array2 = new int[2];

            TypeAssert.IsNotAssignableFrom(array10, array2.GetType());
        }
    }
}
