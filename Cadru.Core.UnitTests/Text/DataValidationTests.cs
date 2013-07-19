using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Cadru.Text;
using System.Globalization;
using System.Diagnostics.CodeAnalysis;

namespace CadruUnitTests.Text
{
    /// <summary>
    ///This is a test class for CadruText.DataValidation and is 
    ///intended to contain all CadruText.DataValidation Unit Tests
    ///</summary>
    [TestClass, ExcludeFromCodeCoverage]
    public class DataValidationTests
    {
        /// <summary>
        ///A test for IsAlpha (char)
        ///</summary>
        [TestMethod]
        public void IsAlpha()
        {
            Assert.IsTrue(DataValidation.IsAlpha('a'));
            Assert.IsTrue(DataValidation.IsAlpha('A'));
            Assert.IsTrue(DataValidation.IsAlpha('.'));
            Assert.IsTrue(DataValidation.IsAlpha('/'));
            Assert.IsTrue(DataValidation.IsAlpha(';'));

            Assert.IsFalse(DataValidation.IsAlpha('1'));
        }

        /// <summary>
        ///A test for IsAlpha (string)
        ///</summary>
        [TestMethod]
        public void IsAlpha1()
        {
            Assert.IsTrue(DataValidation.IsAlpha("ADZerdfd"));
            Assert.IsTrue(DataValidation.IsAlpha("ADZ./;"));

            Assert.IsFalse(DataValidation.IsAlpha("ADZe321"));
        }

        /// <summary>
        /// A test for IsAlpha (string), where string is null
        /// </summary>
        [TestMethod]
        public void IsAlpha2()
        {
            try
            {
                DataValidation.IsAlpha(null);
            }
            catch (ArgumentNullException)
            {
                Assert.IsTrue(true);
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for IsAlphanumeric (char)
        ///</summary>
        [TestMethod]
        public void IsAlphanumeric()
        {
            Assert.IsTrue(DataValidation.IsAlphanumeric('a'));
            Assert.IsTrue(DataValidation.IsAlphanumeric('A'));
            Assert.IsTrue(DataValidation.IsAlphanumeric('.'));
            Assert.IsTrue(DataValidation.IsAlphanumeric('/'));
            Assert.IsTrue(DataValidation.IsAlphanumeric(';'));
            Assert.IsTrue(DataValidation.IsAlphanumeric('1'));

            // This is a high ASCII character.
            Assert.IsFalse(DataValidation.IsAlphanumeric('×'));
        }

        /// <summary>
        ///A test for IsAlphanumeric (string)
        ///</summary>
        [TestMethod]
        public void IsAlphanumeric1()
        {
            Assert.IsTrue(DataValidation.IsAlphanumeric("ADZerdfd"));
            Assert.IsTrue(DataValidation.IsAlphanumeric("ADZ./;"));
            Assert.IsTrue(DataValidation.IsAlphanumeric("ADZe321"));
            Assert.IsTrue(DataValidation.IsAlphanumeric("321./;"));

            // This is a high ASCII character.
            Assert.IsFalse(DataValidation.IsAlphanumeric("×"));
        }

        /// <summary>
        /// A test for IsAlphanumeric (string), where string is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsAlphanumeric2()
        {
            DataValidation.IsAlphanumeric(null);
        }

        /// <summary>
        ///A test for IsCurrency (string)
        ///</summary>
        [TestMethod]
        public void IsCurrency()
        {
            Assert.IsTrue(DataValidation.IsCurrency("45454.15"));
            Assert.IsTrue(DataValidation.IsCurrency("454545.1"));
            Assert.IsTrue(DataValidation.IsCurrency("454545"));
            Assert.IsTrue(DataValidation.IsCurrency("454545.45"));

            Assert.IsFalse(DataValidation.IsCurrency("123.34r"));
            Assert.IsFalse(DataValidation.IsCurrency("ADZe321"));
            Assert.IsFalse(DataValidation.IsCurrency("ADZe"));
            Assert.IsTrue(DataValidation.IsCurrency("456.248"));
        }

        /// <summary>
        ///A test for IsDate (string)
        ///</summary>
        [TestMethod]
        public void IsDate()
        {
            Assert.IsTrue(DataValidation.IsDate("2004-12-25"));
            Assert.IsTrue(DataValidation.IsDate("2004-12-12"));
            Assert.IsTrue(DataValidation.IsDate("12/12/2004"));
            Assert.IsTrue(DataValidation.IsDate("2001-05-02T00:00:00"));

            Assert.IsFalse(DataValidation.IsDate("123"));
            Assert.IsFalse(DataValidation.IsDate("ADZe321"));
            Assert.IsFalse(DataValidation.IsDate("ADZe"));
        }

        /// <summary>
        ///A test for IsDate (string, IFormatProvider)
        ///</summary>
        [TestMethod]
        public void IsDate2()
        {
            Assert.IsTrue(DataValidation.IsDate("2004-12-25", DateTimeFormatInfo.CurrentInfo));
            Assert.IsTrue(DataValidation.IsDate("2004-12-12", DateTimeFormatInfo.CurrentInfo));
            Assert.IsTrue(DataValidation.IsDate("12/12/2004", DateTimeFormatInfo.CurrentInfo));
            Assert.IsTrue(DataValidation.IsDate("2001-05-02T00:00:00", DateTimeFormatInfo.CurrentInfo));

            Assert.IsFalse(DataValidation.IsDate("123", DateTimeFormatInfo.CurrentInfo));
            Assert.IsFalse(DataValidation.IsDate("ADZe321", DateTimeFormatInfo.CurrentInfo));
            Assert.IsFalse(DataValidation.IsDate("ADZe", DateTimeFormatInfo.CurrentInfo));
        }

        ///// <summary>
        /////A test for IsEmailAddress (string)
        /////</summary>
        //[TestMethod]
        //public void IsEmailAddress()
        //{
        //    try
        //    {
        //        bool actual = DataValidation.IsEmailAddress(null);
        //    }
        //    catch (System.ArgumentNullException)
        //    {
        //        Assert.IsTrue(true);
        //    }
        //    catch (System.Exception e)
        //    {
        //        Assert.Fail(e.Message);
        //    }

        //    try
        //    {
        //        bool actual = DataValidation.IsEmailAddress(String.Empty);
        //    }
        //    catch (System.ArgumentException)
        //    {
        //        Assert.IsTrue(true);
        //    }
        //    catch (System.Exception e)
        //    {
        //        Assert.Fail(e.Message);
        //    }
        //}

        //[TestMethod]
        //public void IsEmailAddress2()
        //{
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joecool@xyz.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe.cool@xyz.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe_cool@xyz.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe-cool@xyz.com"));

        //    Assert.IsTrue(DataValidation.IsEmailAddress("joecool@[123.123.123.134]"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe.cool@[123.123.123.134]"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe_cool@[123.123.123.134]"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe-cool@[123.123.123.134]"));

        //    Assert.IsTrue(DataValidation.IsEmailAddress("joecool@xyz.com.us"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe.cool@xyz.com.us"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe_cool@xyz.com.us"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe-cool@xyz.com.us"));

        //    Assert.IsTrue(DataValidation.IsEmailAddress("joecool@xyz.def.com.us"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe.cool@xyz.def.com.us"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe_cool@xyz.def.com.us"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe-cool@xyz.def.com.us"));
            
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool@xyz"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe.cool@xyz"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe_cool@xyz"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe-cool@xyz"));

        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool@"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe.cool@"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe_cool@"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe-cool@"));

        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe.cool"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe_cool"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe-cool"));

        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool@123.123.123.123.123.123"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe.cool@123.123.123.123.123.123"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe_cool@123.123.123.123.123.123"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe-cool@123.123.123.123.123.123"));

        //    Assert.IsTrue(DataValidation.IsEmailAddress("joecool@123.123"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe.cool@123.123"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe_cool@123.123"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe-cool@123.123"));

        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool@[123.123.123.134].com"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe.cool@[123.123.123.134].com"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe_cool@[123.123.123.134].com"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe-cool@[123.123.123.134].com"));

        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool@[123.123.123.123.123.123]"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe.cool@[123.123.123.123.123.123]"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe_cool@[123.123.123.123.123.123]"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe-cool@[123.123.123.123.123.123]"));

        //    Assert.IsTrue(DataValidation.IsEmailAddress("joecool@[123.123]"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe.cool@[123.123]"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe_cool@[123.123]"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("joe-cool@[123.123]"));

        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool@[123.123"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe.cool@[123.123"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe_cool@123.123]"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joe-cool@123.123]"));
        //}

        //[TestMethod]
        //public void IsEmailAddress3()
        //{
        //    Assert.IsTrue(DataValidation.IsEmailAddress("jdoe@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("John Doe <jdoe@example.com>"));
        //    //Assert.IsFalse(DataValidation.IsEmailAddress("John Doe <jdoe@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("John Doe [john@example.com]"));
        //    //Assert.IsFalse(DataValidation.IsEmailAddress("John Doe [john@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("John Doe john@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("John (M) Doe john@example.com"));
        //    //Assert.IsFalse(DataValidation.IsEmailAddress("John (M Doe john@example.com"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John Doe john@example.com (R&D)"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("John [M] Doe john@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("\"John Doe\" john@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("John (J @ Home) Doe john@example.com"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John Mc'Connell Doe john.m.doe@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("\"John Doe: Personal\" john@example.com"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("\"John Doe: Personal john@example.com"));
        //    Assert.IsTrue(DataValidation.IsEmailAddress("John (Dr.) Doe <john(personal account)@example.com(personal domain)>"));
        //}

        //[TestMethod]
        //public void IsEmailAddress4()
        //{
        //    Assert.IsFalse(DataValidation.IsEmailAddress("John Doe john@example.com (R\\D)"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("jdoe@example..com"));
        //    //Assert.IsFalse(DataValidation.IsEmailAddress(".jdoe@example.com"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress(".@example.com"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("jdoe@example,com"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John Doe john@example.com (R/D)"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John Doe john@example.com (dept = R&D)"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John Doe john@example.com (R?D)"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("joecool@example.[123.123]"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John Mc^Connell Doe john.m.doe@example.com"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("jdoe@example_domain.com"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John Doe {john@example.com}"));
        //    Assert.IsFalse(DataValidation.IsEmailAddress("John Doe {john@example.com"));
        //    //Assert.IsTrue(DataValidation.IsEmailAddress("John~Doe <john@example.com>"));
        //}

        /// <summary>
        ///A test for IsHexadecimal (char)
        ///</summary>
        [TestMethod]
        public void IsHexadecimal()
        {
            Assert.IsTrue(DataValidation.IsHexadecimal('0'));
            Assert.IsTrue(DataValidation.IsHexadecimal('a'));
            Assert.IsTrue(DataValidation.IsHexadecimal('A'));
            Assert.IsTrue(DataValidation.IsHexadecimal('f'));
            Assert.IsTrue(DataValidation.IsHexadecimal('F'));

            Assert.IsFalse(DataValidation.IsHexadecimal('h'));
            Assert.IsFalse(DataValidation.IsHexadecimal('H'));

        }

        /// <summary>
        ///A test for IsHexadecimal (string)
        ///</summary>
        [TestMethod]
        public void IsHexadecimal1()
        {
            Assert.IsTrue(DataValidation.IsHexadecimal("0"));
            Assert.IsTrue(DataValidation.IsHexadecimal("a"));
            Assert.IsTrue(DataValidation.IsHexadecimal("A"));
            Assert.IsTrue(DataValidation.IsHexadecimal("f"));
            Assert.IsTrue(DataValidation.IsHexadecimal("F"));
            Assert.IsTrue(DataValidation.IsHexadecimal("0AF"));
            Assert.IsTrue(DataValidation.IsHexadecimal("0af"));

            Assert.IsFalse(DataValidation.IsHexadecimal("h"));
            Assert.IsFalse(DataValidation.IsHexadecimal("H"));
            Assert.IsFalse(DataValidation.IsHexadecimal("0Ah"));
            Assert.IsFalse(DataValidation.IsHexadecimal("0AH"));
        }

        /// <summary>
        ///A test for IsNumeric (char)
        ///</summary>
        [TestMethod]
        public void IsNumeric()
        {
            Assert.IsTrue(DataValidation.IsNumeric('1'));

            Assert.IsFalse(DataValidation.IsNumeric('A'));
            Assert.IsFalse(DataValidation.IsNumeric('e'));
        }

        /// <summary>
        ///A test for IsNumeric (string)
        ///</summary>
        [TestMethod]
        public void IsNumeric1()
        {
            Assert.IsTrue(DataValidation.IsNumeric("123"));

            Assert.IsFalse(DataValidation.IsNumeric("ADZe321"));
            Assert.IsFalse(DataValidation.IsNumeric("ADZe"));
        }

        /// <summary>
        ///A test for IsStrictlyAlpha (char)
        ///</summary>
        [TestMethod]
        public void IsStrictlyAlpha()
        {
            Assert.IsTrue(DataValidation.IsStrictlyAlpha('a'));
            Assert.IsTrue(DataValidation.IsStrictlyAlpha('A'));

            Assert.IsFalse(DataValidation.IsStrictlyAlpha('.'));
            Assert.IsFalse(DataValidation.IsStrictlyAlpha('/'));
            Assert.IsFalse(DataValidation.IsStrictlyAlpha(';'));
            Assert.IsFalse(DataValidation.IsStrictlyAlpha('1'));
        }

        /// <summary>
        ///A test for IsStrictlyAlpha (string)
        ///</summary>
        [TestMethod]
        public void IsStrictlyAlpha1()
        {
            Assert.IsTrue(DataValidation.IsStrictlyAlpha("ADZerdfd"));

            Assert.IsFalse(DataValidation.IsStrictlyAlpha("ADZe321"));
            Assert.IsFalse(DataValidation.IsStrictlyAlpha("ADZ./;"));
            Assert.IsFalse(DataValidation.IsStrictlyAlpha("321./;"));
        }

        /// <summary>
        /// A test for IsStrictlyAlpha (string), where string is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsStrictlyAlpha2()
        {
            DataValidation.IsStrictlyAlpha(null);
        }

        /// <summary>
        ///A test for IsStrictlyAlphanumeric (char)
        ///</summary>
        [TestMethod]
        public void IsStrictlyAlphanumeric()
        {
            Assert.IsTrue(DataValidation.IsStrictlyAlphanumeric('a'));
            Assert.IsTrue(DataValidation.IsStrictlyAlphanumeric('A'));
            Assert.IsTrue(DataValidation.IsStrictlyAlphanumeric('1'));

            Assert.IsFalse(DataValidation.IsStrictlyAlphanumeric('.'));
            Assert.IsFalse(DataValidation.IsStrictlyAlphanumeric('/'));
            Assert.IsFalse(DataValidation.IsStrictlyAlphanumeric(';'));
        }

        /// <summary>
        ///A test for IsStrictlyAlphanumeric (string)
        ///</summary>
        [TestMethod]
        public void IsStrictlyAlphanumeric1()
        {
            Assert.IsTrue(DataValidation.IsStrictlyAlphanumeric("ADZerdfd"));
            Assert.IsTrue(DataValidation.IsStrictlyAlphanumeric("ADZe321"));

            Assert.IsFalse(DataValidation.IsStrictlyAlphanumeric("ADZ./;"));
            Assert.IsFalse(DataValidation.IsStrictlyAlphanumeric("321./;"));
        }

        /// <summary>
        /// A test for IsStrictlyAlphanumeric (string), where string is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsStrictlyAlphanumeric2()
        {
            DataValidation.IsStrictlyAlphanumeric(null);
        }

        /// <summary>
        ///A test for IsValidFileName (string)
        ///</summary>
        [TestMethod]
        public void IsValidFileName()
        {
            string expression = "testfile.txt"; // TODO: Initialize to an appropriate value

            Assert.IsTrue(DataValidation.IsValidFileName(expression));
        }

        /// <summary>
        ///A test for IsValidFileName (string, bool)
        ///</summary>
        [TestMethod]
        public void IsValidFileName1()
        {
            string expression = "\testfile.txt";

            Assert.IsTrue(DataValidation.IsValidFileName(expression, true));
            Assert.IsFalse(DataValidation.IsValidFileName(expression, false));
        }
    }
}
