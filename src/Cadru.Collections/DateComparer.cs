//------------------------------------------------------------------------------
// <copyright file="DateComparer.cs"
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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using Cadru.Collections.Resources;
using Cadru.Extensions;
using Cadru.Internal;

namespace Cadru.Collections
{
    /// <summary>
    /// Compares two dates or string date representations for equivalence,
    /// ignoring case, in date order.
    /// </summary>
    public sealed class DateComparer : IComparer, IEqualityComparer, IComparer<DateTime>, IEqualityComparer<DateTime>, IComparer<string>, IEqualityComparer<string>
    {
        private readonly CultureInfo cultureInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateComparer"/> class
        /// using the <see cref="CultureInfo.CurrentCulture"/> of the current thread.
        /// </summary>
        /// <remarks>
        /// When the <see cref="DateComparer"/> instance is created using this
        /// constructor, the <see cref="CultureInfo.CurrentCulture"/> of the
        /// current thread is saved. Comparison procedures use the saved culture
        /// to determine the sort order and casing rules; therefore, string
        /// comparisons might have different results depending on the culture.
        /// For more information on culture-specific comparisons, see the
        /// <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </remarks>
        public DateComparer()
            : this(CultureInfo.CurrentCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateComparer"/> class
        /// using the specified <see cref="System.Globalization.CultureInfo"/>.
        /// </summary>
        /// <param name="culture">
        /// The <see cref="CultureInfo"/> to use for the new <see cref="DateComparer"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="culture"/> is a <see langword="null"/>.
        /// </exception>
        /// <rermarks>
        /// Comparison procedures use the specified <see cref="CultureInfo"/> to
        /// determine the sort order and casing rules. String comparisons might
        /// have different results depending on the culture. For more
        /// information on culture-specific comparisons, see the
        /// <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </rermarks>
        public DateComparer(CultureInfo culture)
        {
            Contracts.Requires.NotNull(culture, nameof(culture));

            this.cultureInfo = culture;
        }

        /// <summary>
        /// Represents an instance of <see cref="DateComparer"/> that is
        /// associated with the <see cref="CultureInfo.CurrentCulture"/>.
        /// </summary>
        /// <value>The default <see cref="DateComparer"/></value>
        /// <remarks>
        /// Comparison procedures use the
        /// <see cref="CultureInfo.CurrentCulture"/> of the current thread to
        /// determine the sort order and casing rules. String comparisons might
        /// have different results depending on the culture. For more
        /// information on culture-specific comparisons, see the
        /// <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </remarks>
        [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "Cadru.Collections.DateComparer.#ctor", Justification = "This constructor call implicitly passes a culture.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed.")]
        public static IComparer Default => new DateComparer();

        /// <summary>
        /// Represents an instance of <see cref="DateComparer"/> that is
        /// associated with the <see cref="CultureInfo.InvariantCulture"/> and
        /// that is always available.
        /// </summary>
        /// <value>
        /// An instance of <see cref="DateComparer"/> that is associated with <see cref="CultureInfo.InvariantCulture"/>.
        /// </value>
        /// <remarks>
        /// Comparison procedures use the
        /// <see cref="CultureInfo.InvariantCulture"/> to determine the sort
        /// order and casing rules. String comparisons might have different
        /// results depending on the culture. For more information on
        /// culture-specific comparisons, see the
        /// <see cref="System.Globalization"/> namespace and
        /// <see href="http://msdn.microsoft.com/en-us/library/vstudio/h6270d0z(v=vs.100).aspx">Encoding
        /// and Localization</see>.
        /// </remarks>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Reviewed.")]
        public static IComparer DefaultInvariant => new DateComparer(CultureInfo.InvariantCulture);

        /// <inheritdoc/>
        public int Compare(DateTime x, DateTime y)
        {
            return DateTime.Compare(x, y);
        }

        /// <inheritdoc/>
        public int Compare(object x, object y)
        {
            int result;

            if (x is string left && y is string right)
            {
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
            }
            else
            {
                result = x switch
                {
                    null when y is null => 0,
                    null => -1,
                    _ => 1
                };
            }

            return result;
        }

        /// <inheritdoc/>
        /// <remarks>The strings should be a valid date time format.</remarks>
        public int Compare(string x, string y)
        {
            if (!DateTime.TryParse(x, this.cultureInfo, DateTimeStyles.None, out var t1))
            {
                throw ExceptionBuilder.CreateFormatException(Strings.Format_BadDateTime);
            }

            if (!DateTime.TryParse(y, this.cultureInfo, DateTimeStyles.None, out var t2))
            {
                throw ExceptionBuilder.CreateFormatException(Strings.Format_BadDateTime);
            }

            return this.Compare(t1, t2);
        }

        /// <inheritdoc/>
        public bool Equals(DateTime x, DateTime y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public bool Equals(string x, string y)
        {
            if (x.IsNull() && y.IsNull())
            {
                return true;
            }
            else if (x.IsNull() || y.IsNull())
            {
                return false;
            }
            else
            {
                var stringComparison = String.Equals(x, y, StringComparison.OrdinalIgnoreCase);

                // First, test to see if the strings are equal.
                if (stringComparison)
                {
                    // The strings are equal, so now we need to test to see if
                    // they are valid DateTime objects and if they are equal.
                    if (!DateTime.TryParse(x, this.cultureInfo, DateTimeStyles.None, out var t1) || !DateTime.TryParse(y, this.cultureInfo, DateTimeStyles.None, out var t2))
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

        /// <inheritdoc/>
        bool IEqualityComparer.Equals(object x, object y)
        {
            return Equals(x, y);
        }

        /// <inheritdoc/>
        public int GetHashCode(DateTime obj)
        {
            return obj.GetHashCode();
        }

        /// <inheritdoc/>
        public int GetHashCode(object obj)
        {
            Contracts.Requires.NotNull(obj, nameof(obj));

            int hashCode;

            if (obj is DateTime time)
            {
                return time.GetHashCode();
            }
            else
            {
                var s1 = obj as string;

                if (s1.IsNull())
                {
                    hashCode = obj.GetHashCode();
                }
                else
                {
                    if (DateTime.TryParse(s1, this.cultureInfo, DateTimeStyles.None, out var t1))
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

        /// <inheritdoc/>
        public int GetHashCode(string obj)
        {
            Contracts.Requires.NotNull(obj, nameof(obj));

            int hashCode;

            if (obj.Length == 0)
            {
                hashCode = obj.GetHashCode();
            }
            else
            {
                if (DateTime.TryParse(obj, this.cultureInfo, DateTimeStyles.None, out var t1))
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
    }
}