using System.Diagnostics.CodeAnalysis;

using Cadru.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void ToLower()
        {
            Assert.IsTrue(true.ToLower() == System.Boolean.TrueString.ToLower());
            Assert.IsTrue(false.ToLower() == System.Boolean.FalseString.ToLower());
        }

        [TestMethod]
        public void ToUpper()
        {
            Assert.IsTrue(true.ToUpper() == System.Boolean.TrueString.ToUpper());
            Assert.IsTrue(false.ToUpper() == System.Boolean.FalseString.ToUpper());
        }

        [TestMethod]
        public void TryParse()
        {
            ConditionAssert.IsTrue(TryParse("true"));
            ConditionAssert.IsTrue(TryParse("True"));
            ConditionAssert.IsTrue(TryParse("TRUE"));
            ConditionAssert.IsTrue(TryParse("T"));
            ConditionAssert.IsTrue(TryParse("t"));
            ConditionAssert.IsTrue(TryParse("Y"));
            ConditionAssert.IsTrue(TryParse("y"));
            ConditionAssert.IsTrue(TryParse("YES"));
            ConditionAssert.IsTrue(TryParse("Yes"));
            ConditionAssert.IsTrue(TryParse("yes"));

            ConditionAssert.IsFalse(TryParse("false"));
            ConditionAssert.IsFalse(TryParse("False"));
            ConditionAssert.IsFalse(TryParse("FALSE"));
            ConditionAssert.IsFalse(TryParse("F"));
            ConditionAssert.IsFalse(TryParse("f"));
            ConditionAssert.IsFalse(TryParse("N"));
            ConditionAssert.IsFalse(TryParse("n"));
            ConditionAssert.IsFalse(TryParse("NO"));
            ConditionAssert.IsFalse(TryParse("No"));
            ConditionAssert.IsFalse(TryParse("no"));
            ConditionAssert.IsFalse(TryParse("NA"));
            ConditionAssert.IsFalse(TryParse("Na"));
            ConditionAssert.IsFalse(TryParse("na"));
            ConditionAssert.IsFalse(TryParse("N/A"));
            ConditionAssert.IsFalse(TryParse("N/a"));
            ConditionAssert.IsFalse(TryParse("n/a"));

            ConditionAssert.IsTrue(TryParse("true "));
            ConditionAssert.IsTrue(TryParse(" True"));
            ConditionAssert.IsTrue(TryParse("TRUE "));
            ConditionAssert.IsTrue(TryParse("T "));
            ConditionAssert.IsTrue(TryParse(" t"));
            ConditionAssert.IsTrue(TryParse("Y "));
            ConditionAssert.IsTrue(TryParse(" y"));
            ConditionAssert.IsTrue(TryParse("YES "));
            ConditionAssert.IsTrue(TryParse(" Yes"));
            ConditionAssert.IsTrue(TryParse(" yes "));

            ConditionAssert.IsFalse(TryParse(" false"));
            ConditionAssert.IsFalse(TryParse("False "));
            ConditionAssert.IsFalse(TryParse("FALSE "));
            ConditionAssert.IsFalse(TryParse(" F"));
            ConditionAssert.IsFalse(TryParse("f "));
            ConditionAssert.IsFalse(TryParse("N "));
            ConditionAssert.IsFalse(TryParse(" n"));
            ConditionAssert.IsFalse(TryParse("NO "));
            ConditionAssert.IsFalse(TryParse(" No"));
            ConditionAssert.IsFalse(TryParse("no "));
            ConditionAssert.IsFalse(TryParse("NA "));
            ConditionAssert.IsFalse(TryParse(" Na"));
            ConditionAssert.IsFalse(TryParse("na "));
            ConditionAssert.IsFalse(TryParse(" N/A "));
            ConditionAssert.IsFalse(TryParse(" N/a"));
            ConditionAssert.IsFalse(TryParse("n/a "));

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
