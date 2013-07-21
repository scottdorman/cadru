//------------------------------------------------------------------------------
// <copyright file="DataValidation.cs" 
//  company="Scott Dorman" 
//  library="Cadru">
//    Copyright (C) 2001-2013 Scott Dorman.
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
namespace Cadru.Text
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Provides basic routines for common data validation.
    /// </summary>
    public static class DataValidation
    {
        #region Validate
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> matches the
        /// regular expression pattern <paramref name="pattern"/>.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <param name="pattern">The regular expression pattern to match against.</param>
        /// <returns><see cref="Validate"/> returns <see langword="true" /> if <paramref name="expression"/> matches the pattern; 
        /// otherwise it returns <see langword="false" />.</returns>
        public static bool Validate(this string expression, string pattern) 
        {
            return Regex.IsMatch(expression, pattern, RegexOptions.CultureInvariant);
        }

        #endregion

        #region IsAlpha

        #region IsAlpha(char expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains alphabetic characters.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsAlpha(char)"/> returns <see langword="true" /> if <paramref name="expression"/> contains alphabetic
        /// characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters A-Z or a-z, the
        /// punctuation characters and the space character.</remarks>
        public static bool IsAlpha(this char expression)
        {
            return Char.IsLetter(expression) || Char.IsPunctuation(expression) || CharUnicodeInfo.GetUnicodeCategory(expression) == UnicodeCategory.SpaceSeparator;
        }
        #endregion

        #region IsAlpha(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains alphabetic characters.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsAlpha(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains alphabetic
        /// characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters A-Z or a-z, the
        /// punctuation characters and the space character.</remarks>
        public static bool IsAlpha(this string expression) 
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            bool success = true;

            for (int i = 0; i < expression.Length; i++)
            {
                if (!(Char.IsLetter(expression, i) || Char.IsPunctuation(expression, i) || CharUnicodeInfo.GetUnicodeCategory(expression, i) == UnicodeCategory.SpaceSeparator))
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
        #endregion
        
        #endregion

        #region IsAlphanumeric

        #region IsAlphanumeric(char expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains alphabetic and numeric characters. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsAlphanumeric(char)"/> returns <see langword="true" /> if <paramref name="expression"/> contains alphabetic
        /// characters or numeric characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters
        /// A-Z or a-z, the punctuation characters, and the space character.
        /// Numeric characters are 0-9.</remarks>
        public static bool IsAlphanumeric(this char expression) 
        {
            return Char.IsLetter(expression) || Char.IsNumber(expression) || Char.IsPunctuation(expression) || CharUnicodeInfo.GetUnicodeCategory(expression) == UnicodeCategory.SpaceSeparator;
        }
        #endregion

        #region IsAlphanumeric(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains alphabetic and numeric characters. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsAlphanumeric(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains alphabetic
        /// characters or numeric characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters
        /// A-Z or a-z, the punctuation characters, and the space character.
        /// Numeric characters are 0-9.</remarks>
        public static bool IsAlphanumeric(this string expression) 
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            bool success = true;

            for (int i = 0; i < expression.Length; i++)
            {
                if (!(Char.IsLetter(expression, i) || Char.IsNumber(expression, i) || Char.IsPunctuation(expression, i) || CharUnicodeInfo.GetUnicodeCategory(expression, i) == UnicodeCategory.SpaceSeparator))
                {
                    success = false;
                    break;
                }
            }
    
            return success;
        }
        #endregion
        
        #endregion

        #region IsCurrency

        #region IsCurrency(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains a valid currency string. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsCurrency(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains a valid
        /// currency string; otherwise it returns <see langword="false" />.</returns>
        public static bool IsCurrency(this string expression)
        {
            return IsCurrency(expression, NumberFormatInfo.CurrentInfo);
        } 
        #endregion

        #region IsCurrency(string expression, IFormatProvider provider)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains a valid currency string. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> that 
        /// supplies culture-specific formatting information about <paramref name="expression"/>. </param>
        /// <returns><see cref="IsCurrency(string, IFormatProvider)"/> returns <see langword="true" /> if <paramref name="expression"/> contains a valid
        /// currency string; otherwise it returns <see langword="false" />.</returns>
        public static bool IsCurrency(this string expression, IFormatProvider provider)
        {
            double tmp;

            return Double.TryParse(expression, NumberStyles.Currency, provider, out tmp);
        } 
        #endregion

        #endregion

        #region IsDate

        #region IsDate(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> can be 
        /// converted to a date.
        /// </summary>
        /// <param name="expression">Any string expression recognizable as a date or time.</param>
        /// <returns><see cref="IsDate(string)"/> returns <see langword="true" /> if <paramref name="expression"/> is a string convertible
        /// to type <see cref="DateTime"/>; otherwise, it returns <see langword="false" />.</returns>
        /// <remarks><para>In Microsoft Windows, the range of valid dates is January 1, 100 A.D. through 
        /// December 31, 9999 A.D.; the ranges vary among operating systems.</para>
        /// <para><see cref="IsDate(string)"/> uses the <see cref="DateTimeFormatInfo.InvariantInfo"/> object.</para>
        /// </remarks>
        /// <example>The following example uses the <see cref="IsDate(string)"/> function to determine whether an expression 
        /// can be converted to a date:
        /// <code>
        /// string dtString = "9/27/1973";
        /// string dtBadString = "hello";
        ///
        /// Console.WriteLine(DataValidation.IsDate(dtString));
        /// Console.WriteLine(DataValidation.IsDate(dtBadString));
        /// </code>
        /// Produces the following output:
        /// <code>
        /// true
        /// false
        /// </code></example>
        public static bool IsDate(this string expression)
        {
            return IsDate(expression, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None);
        } 
        #endregion

        #region IsDate(string expression, IFormatProvider provider)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> can be 
        /// converted to a date.
        /// </summary>
        /// <param name="expression">Any string expression recognizable as a date or time.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> that 
        /// supplies culture-specific formatting information about <paramref name="expression"/>. </param>
        /// <returns><see cref="IsDate(string, IFormatProvider)"/> returns <see langword="true" /> if <paramref name="expression"/> is a string convertible
        /// to type <see cref="DateTime"/>; otherwise, it returns <see langword="false" />.</returns>
        /// <remarks><para>In Microsoft Windows, the range of valid dates is January 1, 100 A.D. through 
        /// December 31, 9999 A.D.; the ranges vary among operating systems.</para>
        /// </remarks>
        /// <example>The following example uses the IsDate function to determine whether an expression 
        /// can be converted to a date:
        /// <code>
        /// string dtString = "9/27/1973";
        /// string dtBadString = "hello";
        ///
        /// Console.WriteLine(DataValidation.IsDate(dtString));
        /// Console.WriteLine(DataValidation.IsDate(dtBadString));
        /// </code>
        /// Produces the following output:
        /// <code>
        /// true
        /// false
        /// </code></example>
        public static bool IsDate(this string expression, IFormatProvider provider)
        {
            return IsDate(expression, provider, DateTimeStyles.None);
        } 
        #endregion

        #region IsDate(string expression, IFormatProvider provider, DateTimeStyles styles)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> can be 
        /// converted to a date.
        /// </summary>
        /// <param name="expression">Any string expression recognizable as a date or time.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> that 
        /// supplies culture-specific formatting information about <paramref name="expression"/>. </param>
        /// <param name="styles">A bitwise combination of enumeration values 
        /// that defines how to interpret the parsed date in relation to the 
        /// current time zone or the current date. A typical value to specify
        /// is <see cref="DateTimeStyles">None</see>.</param>
        /// <returns><see cref="IsDate(string, IFormatProvider)"/> returns <see langword="true" /> if <paramref name="expression"/> is a string convertible
        /// to type <see cref="DateTime"/>; otherwise, it returns <see langword="false" />.</returns>
        /// <remarks><para>In Microsoft Windows, the range of valid dates is January 1, 100 A.D. through 
        /// December 31, 9999 A.D.; the ranges vary among operating systems.</para>
        /// </remarks>
        /// <example>The following example uses the IsDate function to determine whether an expression 
        /// can be converted to a date:
        /// <code>
        /// string dtString = "9/27/1973";
        /// string dtBadString = "hello";
        ///
        /// Console.WriteLine(DataValidation.IsDate(dtString));
        /// Console.WriteLine(DataValidation.IsDate(dtBadString));
        /// </code>
        /// Produces the following output:
        /// <code>
        /// true
        /// false
        /// </code></example>
        public static bool IsDate(this string expression, IFormatProvider provider, DateTimeStyles styles)
        {
            DateTime tmp;

            return DateTime.TryParse(expression, provider, styles, out tmp);
        } 
        #endregion

        #endregion

        #region IsHexadecimal

        #region IsHexadecimal(char expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains hexadecimal characters. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsHexadecimal(char)"/> returns <see langword="true" /> if <paramref name="expression"/> contains hexadecimal
        /// characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Hexadecimal characters are any letters
        /// A-F, a-f, or 0-9.</remarks>
        public static bool IsHexadecimal(this char expression) 
        {
            string pattern = @"^[0-9A-Fa-f]+$";
            return Validate(Convert.ToString(expression, CultureInfo.InvariantCulture), pattern);
        }
        #endregion

        #region IsHexadecimal(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains hexadecimal characters. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsHexadecimal(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains hexadecimal
        /// characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Hexadecimal characters are any letters
        /// A-F, a-f, or 0-9.</remarks>
        public static bool IsHexadecimal(this string expression) 
        {
            string pattern = @"^[0-9A-Fa-f]+$";
            return Validate(expression, pattern);
        }
        #endregion
        
        #endregion

        #region IsNumeric

        #region IsNumeric(char expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// contains only numbers.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsNumeric(char)"/> returns <see langword="true" /> if <paramref name="expression"/> contains numeric
        /// characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Numeric characters are 0-9.</remarks>
        public static bool IsNumeric(this char expression) 
        {
            return Char.IsNumber(expression);
        }
        #endregion

        #region IsNumeric(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// contains only numbers.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsNumeric(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains numeric
        /// characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Numeric characters are 0-9.</remarks>
        public static bool IsNumeric(this string expression) 
        {
            return IsNumeric(expression, NumberFormatInfo.CurrentInfo);
        }
        #endregion

        #region IsNumeric(string expression, IFormatProvider provider)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// contains only numbers.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <param name="provider">An <see cref="IFormatProvider"/> that 
        /// supplies culture-specific formatting information about <paramref name="expression"/>. </param>
        /// <returns><see cref="IsNumeric(string, IFormatProvider)"/> returns <see langword="true" /> if <paramref name="expression"/> contains numeric
        /// characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Numeric characters are 0-9.</remarks>
        public static bool IsNumeric(this string expression, IFormatProvider provider)
        {
            long tmp;

            return Int64.TryParse(expression, NumberStyles.Number, provider, out tmp);
        } 
        #endregion

        #endregion

        #region IsStrictlyAlpha

        #region IsStrictlyAlpha(char expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// contains only alphabetic characters.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsStrictlyAlpha(char)"/> returns <see langword="true" /> if <paramref name="expression"/> contains 
        /// alphabetic characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters A-Z or a-z.</remarks>
        public static bool IsStrictlyAlpha(this char expression) 
        {
            return Char.IsLetter(expression);
        }
        #endregion

        #region IsStrictlyAlpha(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// contains only alphabetic characters.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsStrictlyAlpha(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains 
        /// alphabetic characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters A-Z or a-z.</remarks>
        public static bool IsStrictlyAlpha(this string expression) 
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            bool success = true;

            for (int i = 0; i < expression.Length; i++)
            {
                if (!Char.IsLetter(expression, i))
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
        #endregion
        
        #endregion

        #region IsStrictlyAlphanumeric

        #region IsStrictlyAlphanumeric(char expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// contains only alphabetic and numeric characters.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsStrictlyAlphanumeric(char)"/> returns <see langword="true" /> if <paramref name="expression"/> contains 
        /// alphabetic characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters A-Z or a-z. 
        /// Numeric characters are 0-9.</remarks>
        public static bool IsStrictlyAlphanumeric(this char expression) 
        {
            return Char.IsLetter(expression) || Char.IsNumber(expression);
        }
        #endregion

        #region IsStrictlyAlphanumeric(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/>
        /// contains only alphabetic and numeric characters.
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsStrictlyAlphanumeric(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains 
        /// alphabetic characters; otherwise it returns <see langword="false" />.</returns>
        /// <remarks>Alphabetic characters are any letters A-Z or a-z. 
        /// Numeric characters are 0-9.</remarks>
        public static bool IsStrictlyAlphanumeric(this string expression) 
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            bool success = true;

            for (int i = 0; i < expression.Length; i++)
            {
                if (!(Char.IsLetter(expression, i) || Char.IsNumber(expression, i)))
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
        #endregion
        
        #endregion

        #region IsValidFileName

        #region IsValidFileName(string expression)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains a valid file name. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <returns><see cref="IsValidFileName(string)"/> returns <see langword="true" /> if <paramref name="expression"/> contains a
        /// valid file name; otherwise it returns <see langword="false" />.</returns>
        public static bool IsValidFileName(this string expression)
        {
            return IsValidFileName(expression, false);
        }
        #endregion

        #region IsValidFileName(string expression, bool platformIndependant)
        /// <summary>
        /// Returns a <see cref="Boolean"/> expression indicating whether <paramref name="expression"/> 
        /// contains a valid file name. 
        /// </summary>
        /// <param name="expression">Any string expression.</param>
        /// <param name="platformIndependent"><see langword="true"/> to test whether
        /// the expression contains a valid platform independent file name;
        /// otherwise, <see langword="false"/>.</param>
        /// <returns><see cref="IsValidFileName(string, bool)"/> returns <see langword="true" /> if <paramref name="expression"/> contains a
        /// valid file name; otherwise it returns <see langword="false" />.</returns>
        public static bool IsValidFileName(this string expression, bool platformIndependent)
        {
            string pattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                pattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }

            return Validate(Convert.ToString(expression, CultureInfo.InvariantCulture), pattern);
        }
        #endregion

        #endregion
    }
}
