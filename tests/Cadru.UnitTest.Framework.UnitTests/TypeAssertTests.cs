using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests
{
    [TestClass, ExcludeFromCodeCoverage]
    public class TypeAssertTests
    {
        [TestMethod]
        public void IsAssignableFrom()
        {
            int[] array10 = new int[10];
            int[] array2 = new int[2];

            TypeAssert.IsAssignableFrom(array10, array2.GetType());
            TypeAssert.IsAssignableFrom(array10, array2.GetType(), "Type Failure Message");
            TypeAssert.IsAssignableFrom(array10, array2.GetType(), "Type Failure Message", null);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsAssignableFromFails()
        {
            int[] array10 = new int[10];
            int[,] array2 = new int[2, 2];

            TypeAssert.IsAssignableFrom(array10, array2.GetType());
        }

        [TestMethod]
        public void IsNotAssignableFrom()
        {
            int[] array10 = new int[10];
            int[,] array2 = new int[2, 2];

            TypeAssert.IsNotAssignableFrom(array10, array2.GetType());
            TypeAssert.IsNotAssignableFrom(array10, array2.GetType(), "Type Failure Message");
            TypeAssert.IsNotAssignableFrom(array10, array2.GetType(), "Type Failure Message", null);
        }

        [TestMethod, ExpectedException(typeof(AssertFailedException))]
        public void IsNotAssignableFromFails()
        {
            int[] array10 = new int[10];
            int[] array2 = new int[2];

            TypeAssert.IsNotAssignableFrom(array10, array2.GetType());
        }
    }
}
