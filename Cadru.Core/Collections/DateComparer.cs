//------------------------------------------------------------------------------
// <copyright file="DateComparer.cs" 
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

namespace Cadru.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using Cadru.Properties;

    /// <summary>
    /// Compares two dates or string date representations for equivalence, 
    /// ignoring case, in date order.
    /// </summary>
    public sealed class DateComparer : IComparer, IEqualityComparer, IComparer<DateTime>, IEqualityComparer<DateTime>, IComparer<string>, IEqualityComparer<string>
    {
        #region events

        #endregion

        #region class-wide fields
        private static DateComparer defaultInvariant;
        private CultureInfo cultureInfo;

        #endregion

        #region constructors

        #region DateComparer()
        /// <summary>
        /// Initializes a new instance of the <see cref="DateComparer"/> class using the
        /// <see cref="CultureInfo.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <remarks>When the <see cref="DateComparer"/> instance is created using
        /// this constructor, the <see cref="CultureInfo.CurrentCulture"/> of the 
        /// current thread is saved. Comparison procedures use the saved 
        /// culture to determine the sort order and casing rules; therefore, 
        /// string comparisons might have different results depending on the 
        /// culture. For more information on culture-specific comparisons, see 
        /// the <see cref="System.Globalization"/> namespace and 
        /// <see cref="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding and Localization</see>.
        /// </remarks>
        public DateComparer()
        {
            this.cultureInfo = CultureInfo.CurrentCulture;
        }
        #endregion

        #region DateComparer(CultureInfo culture)
        /// <summary>
        /// Initializes a new instance of the <see cref="DateComparer"/> class using
        /// the specified <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/>
        /// to use for the new <see cref="DateComparer"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="culture"/> is a <see langword="null" />.</exception>
        /// <rermarks>Comparison procedures use the specified <see cref="CultureInfo"/> to determine
        /// the sort order and casing rules. String comparisons might have different results
        /// depending on the culture. For more information on culture-specific comparisons, see
        /// the <see cref="System.Globalization"/> namespace and 
        /// <see cref="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding and Localization</see>.
        /// </rermarks>
        public DateComparer(CultureInfo culture)
        {
            if (culture == null)
            {
                throw new ArgumentNullException("culture");
            }

            this.cultureInfo = culture;
        }
        #endregion

        #endregion

        #region properties

        #region Default
        /// <summary>
        /// Represents an instance of <see cref="DateComparer"/> that is 
        /// associated with the <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        /// <value>The default <see cref="DateComparer"/></value>
        /// <remarks>Comparison procedures use the 
        /// <see cref="Thread.CurrentCulture"/> of the current thread to 
        /// determine the sort order and casing rules. String comparisons 
        /// might have different results depending on the culture. For more 
        /// information on culture-specific comparisons, see the 
        /// <see cref="System.Globalization"/> namespace and 
        /// <see cref="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding and Localization</see>.
        /// </remarks>
        public static IComparer Default
        {
            get
            {
                return new DateComparer(CultureInfo.CurrentCulture);
            }
        }
        #endregion

        #region DefaultInvariant
        /// <summary>
        /// Represents an instance of <see cref="DateComparer"/> that is 
        /// associated with the 
        /// <see cref="CultureInfo.InvariantCulture"/> and that is always 
        /// available.
        /// </summary>
        /// <value>An instance of <see cref="DateComparer"/> that is 
        /// associated with <see cref="CultureInfo.InvariantCulture"/>.
        /// </value>
        /// <remarks>Comparison procedures use the 
        /// <see cref="CultureInfo.InvariantCulture"/> to determine the sort 
        /// order and casing rules. String comparisons might have different
        /// results depending on the culture. For more information on 
        /// culture-specific comparisons, see the 
        /// <see cref="System.Globalization"/> namespace and 
        /// <see cref="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding and Localization</see>.
        /// </remarks>
        public static IComparer DefaultInvariant
        {
            get
            {
                if (defaultInvariant == null)
                {
                    defaultInvariant = new DateComparer(CultureInfo.InvariantCulture);
                }

                return defaultInvariant;
            }
        }
        #endregion

        #endregion

        #region methods

        #region Compare

        #region Compare(DateTime x, DateTime y)
        /// <summary>
        /// Performs a comparison of two <see cref="DateTime"/> objects and returns a value
        /// indicating whether one is less than, equal to or greater than
        /// the other.
        /// </summary>
        /// <param name="x">The first <see cref="DateTime"/> to compare.</param>
        /// <param name="y">The second <see cref="DateTime"/> to compare.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </description>
        /// </item>
        /// </list></returns>
        public int Compare(DateTime x, DateTime y)
        {
            return DateTime.Compare(x, y);
        }
        #endregion

        #region Compare(object x, object y)
        /// <summary>
        /// Performs a comparison of two objects and returns a value
        /// indicating whether one is less than, equal to or greater than
        /// the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </description>
        /// </item>
        /// </list></returns>
        public int Compare(object x, object y)
        {
            int result = 0;

            string left = x as string;
            string right = y as string;

            if (String.IsNullOrEmpty(left) && String.IsNullOrEmpty(right))
            {
                result = 0;
            }
            else if (String.IsNullOrEmpty(left))
            {
                result = -1;
            }
            else if (String.IsNullOrEmpty(right))
            {
                result = 1;
            }
            else
            {
                result = this.Compare(left, right);
            }

            return result;
        }
        #endregion

        #region Compare(string x, string y)
        /// <summary>
        /// Performs a comparison of two <see cref="String"/> objects and returns a value
        /// indicating whether one is less than, equal to or greater than
        /// the other.
        /// </summary>
        /// <param name="x">The first <see cref="String"/> to compare.</param>
        /// <param name="y">The second <see cref="String"/> to compare.</param>
        /// <returns>
        /// <list type="table">
        /// <listheader>
        /// <term>Value</term>
        /// <term>Condition</term>
        /// </listheader>
        /// <item>
        /// <term>Less than zero</term>
        /// <description>
        /// <paramref name="x"/> is less than <paramref name="y"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Zero</term>
        /// <description>
        /// <paramref name="x"/> equals <paramref name="y"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>Greater than zero</term>
        /// <description>
        /// <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </description>
        /// </item>
        /// </list></returns>
        /// <remarks>The strings should be a valid date time format.</remarks>
        public int Compare(string x, string y)
        {
            if (x == null)
            {
                throw new ArgumentNullException("x");
            }

            if (y == null)
            {
                throw new ArgumentNullException("y");
            }

            DateTime t1;
            DateTime t2;

            if (!DateTime.TryParse(x, this.cultureInfo, DateTimeStyles.None, out t1))
            {
                throw new FormatException(Resources.Format_BadDateTime);
            }

            if (!DateTime.TryParse(y, this.cultureInfo, DateTimeStyles.None, out t2))
            {
                throw new FormatException(Resources.Format_BadDateTime);
            }

            return this.Compare(t1, t2);
        }
        #endregion

        #endregion

        #region Equals

        #region Equals(DateTime x, DateTime y)
        /// <summary>
        /// Returns a value indicating whether two instances of <see cref="DateTime"/> are equal.
        /// </summary>
        /// <param name="x">The first <see cref="DateTime"/> to compare.</param>
        /// <param name="y">The second <see cref="DateTime"/> to compare.</param>
        /// <returns><see langword="true"/> if the two <see cref="DateTime"/> values are equal; 
        /// otherwise, <see langword="false"/>. </returns>
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Equals(y);
        }
        #endregion

        #region Equals(string x, string y)
        /// <summary>
        /// Returns a value indicating whether two instances of <see cref="DateTime"/> are equal.
        /// </summary>
        /// <param name="x">The first <see cref="DateTime"/> to compare.</param>
        /// <param name="y">The second <see cref="DateTime"/> to compare.</param>
        /// <returns><see langword="true"/> if the two <see cref="DateTime"/> values are equal; 
        /// otherwise, <see langword="false"/>. </returns>
        public bool Equals(string x, string y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            else if (x == null || y == null)
            {
                return false;
            }
            else
            {
                bool stringComparison = String.Equals(x, y, StringComparison.OrdinalIgnoreCase);

                // First, test to see if the strings are equal.
                if (stringComparison == true)
                {
                    // The strings are equal, so now we need to test to see if
                    // they are valid DateTime objects and if they are equal.
                    DateTime t1;
                    DateTime t2;

                    if (!DateTime.TryParse(x, this.cultureInfo, DateTimeStyles.None, out t1) || !DateTime.TryParse(y, this.cultureInfo, DateTimeStyles.None, out t2))
                    {
                        // At least of the strings was not a valid DateTime, so
                        // return the string comparison result.
                        return stringComparison;
                    }
                    else
                    {
                        // Both of the strings were a valid DateTime.
                        return t1.Equals(t2);
                    }
                }
                else
                {
                    // The string values aren't equal, so the DateTime
                    // representations can't be equal.
                    return false;
                }
            }
        }
        #endregion

        #endregion

        #region GetHashCode

        #region GetHashCode(DateTime obj)
        /// <summary>
        /// Returns a hash code for the specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="obj">The <see cref="DateTime"/> for which a hash code is to be 
        /// returned.</param>
        /// <returns>A hash code for the specified <see cref="DateTime"/>.</returns>
        /// <exception cref="ArgumentNullException">The type of <paramref name="obj"/> is a 
        /// reference type and <paramref name="obj"/> is a <see langword="null"/>.
        /// </exception>
        public int GetHashCode(DateTime obj)
        {
            return obj.GetHashCode();
        }
        #endregion

        #region GetHashCode(object obj)
        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> for which a hash code is to be 
        /// returned.</param>
        /// <returns>A hash code for the specified <see cref="Objecct"/>.</returns>
        /// <exception cref="ArgumentNullException">The type of <paramref name="obj"/> is a 
        /// reference type and <paramref name="obj"/> is a <see langword="null"/>.
        /// </exception>
        public int GetHashCode(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            int hashCode;

            if (obj is DateTime)
            {
                return ((DateTime)obj).GetHashCode();
            }
            else
            {
                string s1 = obj as string;

                if (s1 == null)
                {
                    hashCode = obj.GetHashCode();
                }
                else
                {
                    DateTime t1;

                    if (DateTime.TryParse(s1, this.cultureInfo, DateTimeStyles.None, out t1))
                    {
                        hashCode = t1.GetHashCode();
                    }
                    else
                    {
                        hashCode = obj.GetHashCode();
                    }
                }
            }

            return hashCode;
        }
        #endregion

        #region GetHashCode(string obj)
        /// <summary>
        /// Returns a hash code for the specified string.
        /// </summary>
        /// <param name="obj">The string for which a hash code is to be 
        /// returned.</param>
        /// <returns>A hash code for the specified string.</returns>
        /// <exception cref="ArgumentNullException">The type of <paramref name="obj"/> is a 
        /// reference type and <paramref name="obj"/> is a <see langword="null"/>.
        /// </exception>
        public int GetHashCode(string obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            int hashCode;

            if (obj.Length == 0)
            {
                hashCode = obj.GetHashCode();
            }
            else
            {
                DateTime t1;

                if (DateTime.TryParse(obj, this.cultureInfo, DateTimeStyles.None, out t1))
                {
                    hashCode = t1.GetHashCode();
                }
                else
                {
                    hashCode = obj.GetHashCode();
                }
            }

            return hashCode;
        }
        #endregion

        #endregion

        #region IEqualityComparer.Equals(object x, object y)
        /// <summary>
        /// Returns a value indicating whether two instances of objects are equal.
        /// </summary>
        /// <param name="x">The first <see cref="Obect"/> to compare.</param>
        /// <param name="y">The second <see cref="Object"/> to compare.</param>
        /// <returns><see langword="true"/> if the two <see cref="Object"/> values are equal; 
        /// otherwise, <see langword="false"/>. </returns>
        bool IEqualityComparer.Equals(object x, object y)
        {
            return DateTime.Equals(x, y);
        }
        #endregion

        #endregion
    }
}
