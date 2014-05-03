using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cadru.Extensions;
using Cadru.UnitTest.Framework;
using System.Diagnostics.CodeAnalysis;

namespace Cadru.UnitTest.Framework.UnitTests.Extensions
{
    [TestClass, ExcludeFromCodeCoverage]
    public class BooleanExtensionsTests
    {
        [TestMethod]
        public void ToBinary()
        {
            Assert.IsTrue(true.ToBit() == 1);
            Assert.IsFalse(false.ToBit() == 1);
            Assert.IsFalse(true.ToBit() == 0);
            Assert.IsTrue(false.ToBit() == 0);
        }

        [TestMethod]
        public void ToChar()
        {
            Assert.IsTrue(true.ToChar() == 'T');
            Assert.IsFalse(false.ToChar() == 'T');
            Assert.IsFalse(true.ToChar() == 'F');
            Assert.IsTrue(false.ToChar() == 'F');
        }

        [TestMethod]
        public void TryParse()
        {            
           ConditionAssert.IsTrue(TryParse("true"));
           ConditionAssert.IsTrue(TryParse("True"));
           ConditionAssert.IsTrue(TryParse("TRUE"));
           ConditionAssert.IsTrue(TryParse("T"));
           ConditionAssert.IsTrue(TryParse("t"));

           ConditionAssert.IsFalse(TryParse("false"));
           ConditionAssert.IsFalse(TryParse("False"));
           ConditionAssert.IsFalse(TryParse("FALSE"));
           ConditionAssert.IsFalse(TryParse("F"));
           ConditionAssert.IsFalse(TryParse("f"));

           ConditionAssert.IsTrue(TryParse("true "));
           ConditionAssert.IsTrue(TryParse(" True"));
           ConditionAssert.IsTrue(TryParse("TRUE "));
           ConditionAssert.IsTrue(TryParse("T "));
           ConditionAssert.IsTrue(TryParse(" t"));

           ConditionAssert.IsFalse(TryParse(" false"));
           ConditionAssert.IsFalse(TryParse("False "));
           ConditionAssert.IsFalse(TryParse("FALSE "));
           ConditionAssert.IsFalse(TryParse(" F"));
           ConditionAssert.IsFalse(TryParse("f "));

           ConditionAssert.IsTrue(TryParse(1));
           ConditionAssert.IsFalse(TryParse(0));

           Assert.IsNull(TryParse("foo"));
           Assert.IsNull(TryParse(" foo"));
           Assert.IsNull(TryParse(3));

           Assert.IsNull(TryParse(""));
           Assert.IsNull(TryParse("\0"));
           Assert.IsNull(TryParse("\u0089"));
           Assert.IsNull(TryParse("\t"));
           Assert.IsNull(TryParse("\u0100"));
           Assert.IsNull(TryParse("\u0089\t"));
           Assert.IsNull(TryParse("\u0089\0"));
           Assert.IsNull(TryParse("\t\0"));
           Assert.IsNull(TryParse("\t\0\0"));
           Assert.IsNull(TryParse("\u0100\0\0"));
           Assert.IsNull(TryParse("\u0100\t\t"));
           Assert.IsNull(TryParse("\t\t"));        
        }

        private bool? TryParse(string value)
        {
            bool result;

            if (value.TryParseAsBoolean(out result))
            {
                return result;
            }

            return null;
        }

        private bool? TryParse(int value)
        {
            bool result;

            if (value.TryParseAsBoolean(out result))
            {
                return result;
            }

            return null;
        }
    }
}
