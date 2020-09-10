//------------------------------------------------------------------------------
// <copyright file="DataValidationTests.cs"
//  company="Scott Dorman"
//  library="Cadru">
//    Copyright (C) 2001-2020 Scott Dorman.
// </copyright>
//
// <license>
//    Licensed under the Microsoft Public License (Ms-PL) (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//    http://opensource.org/licenses/Ms-PL.html
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </license>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Cadru.Text;
using Cadru.UnitTest.Framework;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cadru.Core.Text.Tests
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
            Assert.IsTrue('a'.IsAlpha());
            Assert.IsTrue('A'.IsAlpha());
            Assert.IsTrue('.'.IsAlpha());
            Assert.IsTrue('/'.IsAlpha());
            Assert.IsTrue(';'.IsAlpha());

            Assert.IsFalse('1'.IsAlpha());
            Assert.IsTrue("ADZerdfd".IsAlpha());
            Assert.IsTrue("ADZ./;".IsAlpha());
            Assert.IsFalse("ADZe321".IsAlpha());

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).IsAlpha());
        }

        /// <summary>
        ///A test for IsAlphanumeric (char)
        ///</summary>
        [TestMethod]
        public void IsAlphanumeric()
        {
            Assert.IsTrue('a'.IsAlphanumeric());
            Assert.IsTrue('A'.IsAlphanumeric());
            Assert.IsTrue('.'.IsAlphanumeric());
            Assert.IsTrue('/'.IsAlphanumeric());
            Assert.IsTrue(';'.IsAlphanumeric());
            Assert.IsTrue('1'.IsAlphanumeric());

            // This is a high ASCII character.
            Assert.IsFalse('×'.IsAlphanumeric());

            Assert.IsTrue("ADZerdfd".IsAlphanumeric());
            Assert.IsTrue("ADZ./;".IsAlphanumeric());
            Assert.IsTrue("ADZe321".IsAlphanumeric());
            Assert.IsTrue("321./;".IsAlphanumeric());

            // This is a high ASCII character.
            Assert.IsFalse("×".IsAlphanumeric());

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).IsAlphanumeric());
        }

        /// <summary>
        ///A test for IsCurrency (string)
        ///</summary>
        [TestMethod]
        public void IsCurrency()
        {
            Assert.IsTrue("45454.15".IsCurrency());
            Assert.IsTrue("454545.1".IsCurrency());
            Assert.IsTrue("454545".IsCurrency());
            Assert.IsTrue("454545.45".IsCurrency());

            Assert.IsFalse("123.34r".IsCurrency());
            Assert.IsFalse("ADZe321".IsCurrency());
            Assert.IsFalse("ADZe".IsCurrency());
            Assert.IsTrue("456.248".IsCurrency());
        }

        /// <summary>
        ///A test for IsDate (string)
        ///</summary>
        [TestMethod]
        public void IsDate()
        {
            Assert.IsTrue("2004-12-25".IsDate());
            Assert.IsTrue("2004-12-12".IsDate());
            Assert.IsTrue("12/12/2004".IsDate());
            Assert.IsTrue("2001-05-02T00:00:00".IsDate());

            Assert.IsFalse("123".IsDate());
            Assert.IsFalse("ADZe321".IsDate());
            Assert.IsFalse("ADZe".IsDate());

            Assert.IsTrue("2004-12-25".IsDate(DateTimeFormatInfo.CurrentInfo));
            Assert.IsTrue("2004-12-12".IsDate(DateTimeFormatInfo.CurrentInfo));
            Assert.IsTrue("12/12/2004".IsDate(DateTimeFormatInfo.CurrentInfo));
            Assert.IsTrue("2001-05-02T00:00:00".IsDate(DateTimeFormatInfo.CurrentInfo));

            Assert.IsFalse("123".IsDate(DateTimeFormatInfo.CurrentInfo));
            Assert.IsFalse("ADZe321".IsDate(DateTimeFormatInfo.CurrentInfo));
            Assert.IsFalse("ADZe".IsDate(DateTimeFormatInfo.CurrentInfo));
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
        //    Assert.IsTrue("joecool@xyz.com".IsEmailAddress());
        //    Assert.IsTrue("joe.cool@xyz.com".IsEmailAddress());
        //    Assert.IsTrue("joe_cool@xyz.com".IsEmailAddress());
        //    Assert.IsTrue("joe-cool@xyz.com".IsEmailAddress());

        // Assert.IsTrue("joecool@[123.123.123.134]".IsEmailAddress());
        // Assert.IsTrue("joe.cool@[123.123.123.134]".IsEmailAddress());
        // Assert.IsTrue("joe_cool@[123.123.123.134]".IsEmailAddress()); Assert.IsTrue("joe-cool@[123.123.123.134]".IsEmailAddress());

        // Assert.IsTrue("joecool@xyz.com.us".IsEmailAddress());
        // Assert.IsTrue("joe.cool@xyz.com.us".IsEmailAddress());
        // Assert.IsTrue("joe_cool@xyz.com.us".IsEmailAddress()); Assert.IsTrue("joe-cool@xyz.com.us".IsEmailAddress());

        // Assert.IsTrue("joecool@xyz.def.com.us".IsEmailAddress());
        // Assert.IsTrue("joe.cool@xyz.def.com.us".IsEmailAddress());
        // Assert.IsTrue("joe_cool@xyz.def.com.us".IsEmailAddress()); Assert.IsTrue("joe-cool@xyz.def.com.us".IsEmailAddress());

        // Assert.IsFalse("joecool@xyz".IsEmailAddress());
        // Assert.IsFalse("joe.cool@xyz".IsEmailAddress());
        // Assert.IsFalse("joe_cool@xyz".IsEmailAddress()); Assert.IsFalse("joe-cool@xyz".IsEmailAddress());

        // Assert.IsFalse("joecool@".IsEmailAddress());
        // Assert.IsFalse("joe.cool@".IsEmailAddress());
        // Assert.IsFalse("joe_cool@".IsEmailAddress()); Assert.IsFalse("joe-cool@".IsEmailAddress());

        // Assert.IsFalse("joecool".IsEmailAddress());
        // Assert.IsFalse("joe.cool".IsEmailAddress());
        // Assert.IsFalse("joe_cool".IsEmailAddress()); Assert.IsFalse("joe-cool".IsEmailAddress());

        // Assert.IsFalse("joecool@123.123.123.123.123.123".IsEmailAddress());
        // Assert.IsFalse("joe.cool@123.123.123.123.123.123".IsEmailAddress());
        // Assert.IsFalse("joe_cool@123.123.123.123.123.123".IsEmailAddress()); Assert.IsFalse("joe-cool@123.123.123.123.123.123".IsEmailAddress());

        // Assert.IsTrue("joecool@123.123".IsEmailAddress());
        // Assert.IsTrue("joe.cool@123.123".IsEmailAddress());
        // Assert.IsTrue("joe_cool@123.123".IsEmailAddress()); Assert.IsTrue("joe-cool@123.123".IsEmailAddress());

        // Assert.IsFalse("joecool@[123.123.123.134].com".IsEmailAddress());
        // Assert.IsFalse("joe.cool@[123.123.123.134].com".IsEmailAddress());
        // Assert.IsFalse("joe_cool@[123.123.123.134].com".IsEmailAddress()); Assert.IsFalse("joe-cool@[123.123.123.134].com".IsEmailAddress());

        // Assert.IsFalse("joecool@[123.123.123.123.123.123]".IsEmailAddress());
        // Assert.IsFalse("joe.cool@[123.123.123.123.123.123]".IsEmailAddress());
        // Assert.IsFalse("joe_cool@[123.123.123.123.123.123]".IsEmailAddress()); Assert.IsFalse("joe-cool@[123.123.123.123.123.123]".IsEmailAddress());

        // Assert.IsTrue("joecool@[123.123]".IsEmailAddress());
        // Assert.IsTrue("joe.cool@[123.123]".IsEmailAddress());
        // Assert.IsTrue("joe_cool@[123.123]".IsEmailAddress()); Assert.IsTrue("joe-cool@[123.123]".IsEmailAddress());

        //    Assert.IsFalse("joecool@[123.123".IsEmailAddress());
        //    Assert.IsFalse("joe.cool@[123.123".IsEmailAddress());
        //    Assert.IsFalse("joe_cool@123.123]".IsEmailAddress());
        //    Assert.IsFalse("joe-cool@123.123]".IsEmailAddress());
        //}

        //[TestMethod]
        //public void IsEmailAddress3()
        //{
        //    Assert.IsTrue("jdoe@example.com".IsEmailAddress());
        //    Assert.IsTrue("John Doe <jdoe@example.com>".IsEmailAddress());
        //    //Assert.IsFalse("John Doe <jdoe@example.com".IsEmailAddress());
        //    Assert.IsTrue("John Doe [john@example.com]".IsEmailAddress());
        //    //Assert.IsFalse("John Doe [john@example.com".IsEmailAddress());
        //    Assert.IsTrue("John Doe john@example.com".IsEmailAddress());
        //    Assert.IsTrue("John (M) Doe john@example.com".IsEmailAddress());
        //    //Assert.IsFalse("John (M Doe john@example.com".IsEmailAddress());
        //    //Assert.IsTrue("John Doe john@example.com (R&D)".IsEmailAddress());
        //    Assert.IsTrue("John [M] Doe john@example.com".IsEmailAddress());
        //    Assert.IsTrue("\"John Doe\" john@example.com".IsEmailAddress());
        //    Assert.IsTrue("John (J @ Home) Doe john@example.com".IsEmailAddress());
        //    //Assert.IsTrue("John Mc'Connell Doe john.m.doe@example.com".IsEmailAddress());
        //    Assert.IsTrue("\"John Doe: Personal\" john@example.com".IsEmailAddress());
        //    Assert.IsFalse("\"John Doe: Personal john@example.com".IsEmailAddress());
        //    Assert.IsTrue("John (Dr.) Doe <john(personal account)@example.com(personal domain)>".IsEmailAddress());
        //}

        //[TestMethod]
        //public void IsEmailAddress4()
        //{
        //    Assert.IsFalse("John Doe john@example.com (R\\D)".IsEmailAddress());
        //    Assert.IsFalse("jdoe@example..com".IsEmailAddress());
        //    //Assert.IsFalse(".jdoe@example.com".IsEmailAddress());
        //    Assert.IsFalse(".@example.com".IsEmailAddress());
        //    Assert.IsFalse("jdoe@example,com".IsEmailAddress());
        //    //Assert.IsTrue("John Doe john@example.com (R/D)".IsEmailAddress());
        //    //Assert.IsTrue("John Doe john@example.com (dept = R&D)".IsEmailAddress());
        //    //Assert.IsTrue("John Doe john@example.com (R?D)".IsEmailAddress());
        //    Assert.IsFalse("joecool@example.[123.123]".IsEmailAddress());
        //    //Assert.IsTrue("John Mc^Connell Doe john.m.doe@example.com".IsEmailAddress());
        //    Assert.IsFalse("jdoe@example_domain.com".IsEmailAddress());
        //    //Assert.IsTrue("John Doe {john@example.com}".IsEmailAddress());
        //    Assert.IsFalse("John Doe {john@example.com".IsEmailAddress());
        //    //Assert.IsTrue("John~Doe <john@example.com>".IsEmailAddress());
        //}

        /// <summary>
        ///A test for IsHexadecimal (char)
        ///</summary>
        [TestMethod]
        public void IsHexadecimal()
        {
            Assert.IsTrue('0'.IsHexadecimal());
            Assert.IsTrue('a'.IsHexadecimal());
            Assert.IsTrue('A'.IsHexadecimal());
            Assert.IsTrue('f'.IsHexadecimal());
            Assert.IsTrue('F'.IsHexadecimal());

            Assert.IsFalse('h'.IsHexadecimal());
            Assert.IsFalse('H'.IsHexadecimal());

            Assert.IsTrue("0".IsHexadecimal());
            Assert.IsTrue("a".IsHexadecimal());
            Assert.IsTrue("A".IsHexadecimal());
            Assert.IsTrue("f".IsHexadecimal());
            Assert.IsTrue("F".IsHexadecimal());
            Assert.IsTrue("0AF".IsHexadecimal());
            Assert.IsTrue("0af".IsHexadecimal());

            Assert.IsFalse("h".IsHexadecimal());
            Assert.IsFalse("H".IsHexadecimal());
            Assert.IsFalse("0Ah".IsHexadecimal());
            Assert.IsFalse("0AH".IsHexadecimal());
        }

        /// <summary>
        ///A test for IsNumeric (char)
        ///</summary>
        [TestMethod]
        public void IsNumeric()
        {
            Assert.IsTrue('1'.IsNumeric());

            Assert.IsFalse('A'.IsNumeric());
            Assert.IsFalse('e'.IsNumeric());
            Assert.IsTrue("123".IsNumeric());

            Assert.IsFalse("ADZe321".IsNumeric());
            Assert.IsFalse("ADZe".IsNumeric());
        }

        /// <summary>
        ///A test for IsStrictlyAlpha (char)
        ///</summary>
        [TestMethod]
        public void IsStrictlyAlpha()
        {
            Assert.IsTrue('a'.IsStrictlyAlpha());
            Assert.IsTrue('A'.IsStrictlyAlpha());

            Assert.IsFalse('.'.IsStrictlyAlpha());
            Assert.IsFalse('/'.IsStrictlyAlpha());
            Assert.IsFalse(';'.IsStrictlyAlpha());
            Assert.IsFalse('1'.IsStrictlyAlpha());

            Assert.IsTrue("ADZerdfd".IsStrictlyAlpha());

            Assert.IsFalse("ADZe321".IsStrictlyAlpha());
            Assert.IsFalse("ADZ./;".IsStrictlyAlpha());
            Assert.IsFalse("321./;".IsStrictlyAlpha());

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).IsStrictlyAlpha());
        }

        /// <summary>
        ///A test for IsStrictlyAlphanumeric (char)
        ///</summary>
        [TestMethod]
        public void IsStrictlyAlphanumeric()
        {
            Assert.IsTrue('a'.IsStrictlyAlphanumeric());
            Assert.IsTrue('A'.IsStrictlyAlphanumeric());
            Assert.IsTrue('1'.IsStrictlyAlphanumeric());

            Assert.IsFalse('.'.IsStrictlyAlphanumeric());
            Assert.IsFalse('/'.IsStrictlyAlphanumeric());
            Assert.IsFalse(';'.IsStrictlyAlphanumeric());

            Assert.IsTrue("ADZerdfd".IsStrictlyAlphanumeric());
            Assert.IsTrue("ADZe321".IsStrictlyAlphanumeric());

            Assert.IsFalse("ADZ./;".IsStrictlyAlphanumeric());
            Assert.IsFalse("321./;".IsStrictlyAlphanumeric());

            ExceptionAssert.Throws<ArgumentNullException>(() => ((string)null).IsStrictlyAlphanumeric());
        }

        /// <summary>
        ///A test for IsValidFileName (string)
        ///</summary>
        [TestMethod]
        public void IsValidFileName()
        {
            var expression = "testfile.txt";

            Assert.IsTrue(expression.IsValidFileName());

            expression = "\testfile.txt";

            Assert.IsTrue(expression.IsValidFileName(true));
            Assert.IsFalse(expression.IsValidFileName(false));
        }
    }
}